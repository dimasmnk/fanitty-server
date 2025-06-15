import { Injectable } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, firstValueFrom, lastValueFrom } from 'rxjs';
import { Auth, User, authState, GoogleAuthProvider, signInWithPopup, signOut, ParsedToken } from '@angular/fire/auth';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BaseUrl } from './service-constants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public readonly user: Observable<User | null> = EMPTY;
  public appUserId: string = '';
  public isLoggedIn: boolean = false;
  private isUserUpdatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public isUserUpdated: Observable<boolean> = this.isUserUpdatedSubject.asObservable();

  constructor(
    public auth: Auth,
    private router: Router,
    private httpClient: HttpClient
  ) {
    this.auth.onAuthStateChanged((aUser: User | null) => {
      if (aUser) {
        this.getAppUserId().then(appUserId => {
            this.setAppUserId(appUserId);
        })
      }
      else {
        this.setAppUserId('');
      }
    });

    this.user = authState(this.auth);
    this.isLoggedIn = Boolean(localStorage.getItem('isLoggedIn'));
    this.appUserId = String(localStorage.getItem('userId'));
  }

  async userSignInWithGoogle() {
    await signInWithPopup(this.auth, new GoogleAuthProvider());
    if(this.auth.currentUser) {
      if(!this.appUserId) {
        try {
          await this.createUserAndUpdateAppUserId();
        } 
        catch {
          await this.userSignOut();
        }
      }
      this.router.navigate(['/']);
    }
  }

  async createUserAndUpdateAppUserId() {
    var resultCode = await this.createUser();
    if(resultCode == 200) {
      await this.getToken(true);
      var userId = await this.getAppUserId();
      this.setAppUserId(userId);
    }
  }

  async userSignOut() {
    await signOut(this.auth);
    if (this.auth.currentUser == null) {
      this.router.navigate(['/']);
    }
  }

  async getToken(forceRefresh: boolean = false): Promise<string> {
    var user = await firstValueFrom(this.user);
    if(user) {
      return await user.getIdToken(forceRefresh);
    }
    else {
      return '';
    }
  }

  async getAppUserId(): Promise<string> {
    var token = await this.auth.currentUser?.getIdTokenResult();
    var userId = token ? token.claims["app_user_id"] : '';
    return userId ? userId : '';
  }

  setAppUserId(appUserId: string) {
    this.appUserId = appUserId;
    localStorage.setItem('userId', this.appUserId);
    if(appUserId) {
      localStorage.setItem('isLoggedIn', 'true');
      this.isLoggedIn = true;
      this.isUserUpdatedSubject.next(true);
    }
    else {
      localStorage.setItem('isLoggedIn', 'false');
      this.isLoggedIn = false;
    }
  }

  async createUser(): Promise<number> {
    var result = await lastValueFrom(this.httpClient.post(`${BaseUrl}/users`, null, { observe: 'response' }));
    return result.status;
  }
}
