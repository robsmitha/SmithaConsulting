import { ValidatorFn, FormGroup, ValidationErrors } from "@angular/forms";


export const confirmPasswordMatch: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
  const password = control.get('password');
  const confirmPassword = control.get('confirmPassword');

  return password && confirmPassword && password.value !== confirmPassword.value ? { 'confirmPasswordMismatch': true } : null;
};
