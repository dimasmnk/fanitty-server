import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserByUsernameResponse } from 'src/app/services/responses/user-by-username-response';
import { UserApiService } from 'src/app/services/user-api.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit, OnDestroy {
  username: string = '';
  user: UserByUsernameResponse | null = null;
  private currentUserDataSubscription: Subscription | null = null;
  isCurrentUser = false;
  isButtonLoading = true;

  constructor(private activatedRoute: ActivatedRoute,
    private userApiService: UserApiService,
    private router: Router) { }

  ngOnInit(): void {
    this.username = this.activatedRoute.snapshot.paramMap.get('username')!;
    this.currentUserDataSubscription = this.userApiService.currentUserData.subscribe(userData => {
      if (userData) {
        this.isCurrentUser = userData?.username === this.username;
        this.isButtonLoading = false;
      }
    });
    this.router.events
      .subscribe(url => {
        const nextUsername = this.activatedRoute.snapshot.paramMap.get('username')!;
        if (this.username != nextUsername) {
          this.username = nextUsername;
          this.isButtonLoading = true;
          this.currentUserDataSubscription?.unsubscribe();
          this.currentUserDataSubscription = this.userApiService.currentUserData.subscribe(userData => {
            if (userData) {
              this.isCurrentUser = userData?.username === this.username;
              this.isButtonLoading = false;
            }
          });
          this.getUser();
        }
      });
      this.getUser();
  }

  ngOnDestroy(): void {
    this.currentUserDataSubscription?.unsubscribe();
  }

  async getUser() {
    this.user = await this.userApiService.getUserInfoByUsername(this.username);
  }

  public truncateString(str: string, length: number = 40): string {
    return str.length > length ? str.substring(0, length) : str;
  }
}
