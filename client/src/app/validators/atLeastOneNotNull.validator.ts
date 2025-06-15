import { AbstractControl, FormGroup, ValidatorFn } from '@angular/forms';

export function atLeastOneFieldNotNullValidator(): ValidatorFn {
  return (control: AbstractControl) => {
    const controls = control as FormGroup;

    let isValid = false;

    // Iterate through the form controls in the FormGroup
    Object.keys(controls.controls).forEach((controlName) => {
      const currentControl = controls.get(controlName);

      // Check if the control has a value
      if (currentControl?.value !== null && currentControl?.value !== '') {
        isValid = true;
      }
    });

    // If no control has a value, return an error
    return isValid ? null : { atLeastOneFieldNotNull: true };
  };
}
