import { AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable, of, timer } from 'rxjs';
import { map, switchMap, catchError } from 'rxjs/operators';
import { UserApiService } from '../services/user-api.service'; 
export function usernameAvailabilityValidator(userApiService: UserApiService) {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    if (!control.value) {
      return of(null);
    }
    return timer(3000)  // 300ms debounce time
      .pipe(
        switchMap(() => {
          return userApiService.isUsernameAvailable(control.value)
            .pipe(
              map(checkUsernameAvailabilityRepsonse => {
                return !checkUsernameAvailabilityRepsonse.isAvailable ? { 'usernameTaken': true } : null;
              }),
              catchError(() => {
                return of(null);  // In case of an error with the API, return as if there's no error.
              })
            );
        })
      );
  };
}