import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { Customer } from '../classes/customer';
import { Router } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { confirmPasswordMatch } from '../shared/confirmpassword.directive';

@Component({
  selector: 'change-password',
  templateUrl: './change-password.component.html',
})


export class ChangePasswordComponent {
  customer: Customer;
  changePasswordForm;

  constructor(private customerService: CustomerService,
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService) {
    this.customer = new Customer();
    
  }

  ngOnInit() {
    this.changePasswordForm = new FormGroup({
      currentPassword: new FormControl('', [
        Validators.required
      ]),
      password: new FormControl('', [
        Validators.required
      ]),
      confirmPassword: new FormControl('', [
        Validators.required
      ])
    }, { validators: confirmPasswordMatch });
  }

  onSubmit(d) {
    this.customerService.loadCustomer(this.authService.CustomerId).
      subscribe(data => {
        this.customer = data;
        if (this.customer != null) {

          this.customer.password = d.password;
          this.customerService.editCustomer(this.customer)
            .subscribe(data => {
              if (data.id) {
                this.router.navigateByUrl('profile');
              } else {
                alert('Not found');
              }
            });
        }
        else {
          alert('Not found');
          this.router.navigateByUrl('/')
        }
      });

  }

  get currentPassword() {
    return this.changePasswordForm.get("currentPassword")
  }

  get password() {
    return this.changePasswordForm.get("password")
  }

  get confirmPassword() {
    return this.changePasswordForm.get("confirmPassword")
  }


}
