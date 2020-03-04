import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { Customer } from '../classes/customer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
})
export class SignInComponent {
  customer: Customer
  signInForm;
  constructor(private customerService: CustomerService, private formBuilder: FormBuilder, private router: Router, private authService: AuthService) {
    this.customer = new Customer();
  }

  ngOnInit() {
    this.signInForm = new FormGroup({
      email: new FormControl(this.customer.email, [
        Validators.required,
        Validators.minLength(4)
      ]),
      password: new FormControl(this.customer.password, [
        Validators.required,
        Validators.minLength(4)
      ])
    });
  }

  get email() {
    return this.signInForm.get('email')
  }
  get password() {
    return this.signInForm.get('password')
  }

  onSubmit(signInData) {
    this.customerService.signInCustomer(signInData)
      .subscribe(data => {
        if (data && data.id && data.id > 0) {
          this.authService.setLoginSession(data);
          this.router.navigateByUrl('profile');
        } else {
          alert('Not found');
        }
      });
  }
}
