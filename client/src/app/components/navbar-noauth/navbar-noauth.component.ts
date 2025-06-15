import { Component } from '@angular/core';
import { Title } from '../../services/constants';
import { AuthService } from 'src/app/services/auth.service';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'app-navbar-noauth',
  templateUrl: './navbar-noauth.component.html',
  styleUrls: ['./navbar-noauth.component.scss']
})
export class NavbarNoauthComponent {
  faGoogle = faGoogle;
  constructor(public auth: AuthService){}

  title : string = Title;
}
