import { Component, OnInit } from '@angular/core';
import { UserData } from 'src/app/models/user-data.mode';
import { UserApiService } from 'src/app/services/user-api.service';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { FormBuilder, Validators } from '@angular/forms';
import { usernameAvailabilityValidator } from 'src/app/validators/username.validator';
import { atLeastOneFieldNotNullValidator } from 'src/app/validators/atLeastOneNotNull.validator';
import { UpdateUserCommand } from 'src/app/services/commands/update-user-command';

@Component({
  selector: 'app-profile-editor',
  templateUrl: './profile-editor.component.html',
  styleUrls: ['./profile-editor.component.scss']
})
export class ProfileEditorComponent implements OnInit {
  faArrowLeft = faArrowLeft;
  userData: UserData | null = null;
  isLoading = false;
  isGoodResult = false;
  isSaved = false;

  editProfileForm = this.fb.nonNullable.group({
    username: ['', [Validators.minLength(5), Validators.maxLength(40), Validators.pattern('^[a-zA-Z0-9\-_\.]+$')], usernameAvailabilityValidator(this.userApiService)],
    displayName: ['', [Validators.maxLength(50)]],
    bio: ['', [Validators.maxLength(255)]]
  }, { validators: atLeastOneFieldNotNullValidator() });
  
  constructor(private userApiService: UserApiService, private fb: FormBuilder) { }
  ngOnInit(): void {
    this.userApiService.currentUserData.subscribe(userData => {
      this.userData = userData;
    })
  }

  async save(): Promise<number> {
    this.isLoading = true;
    var result = await this.userApiService.saveProfile(new UpdateUserCommand(this.username?.value ?? null, this.displayName?.value ?? null, this.bio?.value ?? null));
    this.isLoading = false;
    this.isGoodResult = result == 200;
    this.isSaved = true;
    return result;
  }

  get username() { return this.editProfileForm.get('username'); }
  get displayName() { return this.editProfileForm.get('displayName'); }
  get bio() { return this.editProfileForm.get('bio'); }
  get form() { return this.editProfileForm; }
}
