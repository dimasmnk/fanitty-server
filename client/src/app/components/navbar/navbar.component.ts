import { Component, OnDestroy, OnInit } from '@angular/core';
import { Title } from '../../services/constants';
import { AuthService } from 'src/app/services/auth.service';
import { Observable, EMPTY, Subscription } from 'rxjs';
import { UserApiService } from 'src/app/services/user-api.service';
import { User } from '@angular/fire/auth';
import { publishFacade } from '@angular/compiler';
import { UserData } from 'src/app/models/user-data.mode';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {
  title : string = Title;
  userData: UserData | null = null;
  currentUserDataUnsubscribe: Subscription | undefined;
  
  constructor(public userApiService: UserApiService,
    public authService: AuthService) {
      this.currentUserDataUnsubscribe = this.userApiService.currentUserData?.subscribe(userData => {
        this.userData = userData;
      });
    }

    ngOnInit() {

    }

    ngOnDestroy(): void {
      this.currentUserDataUnsubscribe?.unsubscribe();
    }
}
