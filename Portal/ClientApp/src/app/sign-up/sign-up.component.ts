import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, ValidatorFn } from '@angular/forms';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { Customer } from '../classes/customer';
import { Router } from '@angular/router';
import { confirmPasswordMatch } from '../shared/confirmpassword.directive';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
})

export class SignUpComponent {
  customer: Customer
  signUpForm;

  constructor(private customerService: CustomerService, private formBuilder: FormBuilder, private router: Router, private authService: AuthService) {
    this.customer = new Customer();
  }

  ngOnInit() {
    this.signUpForm = new FormGroup({
      firstName: new FormControl('', [
        Validators.required
      ]),
      lastName: new FormControl('', [
        Validators.required
      ]),
      email: new FormControl('', [
        Validators.required
      ]),
      password: new FormControl('', [
        Validators.required
      ]),
      confirmPassword: new FormControl('', [
        Validators.required
      ])
    }, { validators: confirmPasswordMatch })
  }

  get firstName() {
    return this.signUpForm.get('firstName')
  }

  get lastName() {
    return this.signUpForm.get('lastName')
  }

  get email() {
    return this.signUpForm.get('email')
  }

  get password() {
    return this.signUpForm.get('password')
  }

  get confirmPassword() {
    return this.signUpForm.get('confirmPassword')
  }

  onSubmit(signUpData) {
    this.customerService.signUpCustomer(signUpData)
      .subscribe(data => {
        if (data.id > 0) {
          this.authService.setLoginSession(data);
          this.router.navigateByUrl('profile');
        } else {
          alert('Not found');
        }
      });
  }
}
