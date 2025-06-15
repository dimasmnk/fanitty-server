import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseUrl } from './service-constants';
import { BehaviorSubject, EMPTY, Observable, lastValueFrom, shareReplay } from 'rxjs';
import { CurrentUserResponse } from './responses/current-user-response.model';
import { CheckUsernameAvailabilityRepsonse } from './responses/check-username-availability-response.model';
import { UserByUsernameResponse } from './responses/user-by-username-response';
import { AuthService } from './auth.service';
import { UserData } from '../models/user-data.mode';
import { UpdateUserCommand } from './commands/update-user-command';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  private currentUserDataSubject: BehaviorSubject<UserData | null> = new BehaviorSubject<UserData | null>(null);
  public currentUserData = this.currentUserDataSubject.asObservable();

  constructor(private httpClient: HttpClient, private authService: AuthService) {
    this.authService.isUserUpdated.subscribe(isUserUpdated => {
      if(isUserUpdated) {
        this.getCurrentUserInfo().subscribe(userData => {
          this.currentUserDataSubject.next(userData);
        });
      }
    });
   }

  private getCurrentUserInfo(): Observable<UserData> {
    return this.httpClient.get<CurrentUserResponse>(`${BaseUrl}/users/me`);
  }

  async getUserInfoByUsername(username: string): Promise<UserByUsernameResponse> {
    return await lastValueFrom(this.httpClient.get<UserByUsernameResponse>(`${BaseUrl}/users/${username}`));
  }

  isUsernameAvailable(username: string): Observable<CheckUsernameAvailabilityRepsonse> {
    return this.httpClient.get<CheckUsernameAvailabilityRepsonse>(`${BaseUrl}/usernames/check/${username}`);
  }

  async saveProfile(updateUserCommand: UpdateUserCommand): Promise<number> {
    var result = await lastValueFrom(this.httpClient.put(`${BaseUrl}/users/`, updateUserCommand, { observe: 'response' }));
    if(result.status == 200) {
      let newUserData = this.currentUserDataSubject.value!;
      newUserData.username = updateUserCommand.username ? updateUserCommand.username : newUserData.username;
      newUserData.displayName = updateUserCommand.displayName ? updateUserCommand.displayName : newUserData.displayName;
      newUserData.bio = updateUserCommand.bio ? updateUserCommand.bio : newUserData.bio;

      this.currentUserDataSubject.next(newUserData)
    }
    return result.status;
  }
}
