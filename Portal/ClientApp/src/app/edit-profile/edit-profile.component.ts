import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';
import { Customer } from '../classes/customer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
})
export class EditProfileComponent {
  customer: Customer
  editProfileForm;

  constructor(private customerService: CustomerService,
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService) {

      this.customerService.loadCustomer(authService.CustomerId).
      subscribe(data => {
        this.customer = data;
        if (this.customer != null) {

          this.editProfileForm = new FormGroup({
            firstName: new FormControl(this.customer.firstName, [
              Validators.required
            ]),
            middleName: new FormControl(this.customer.middleName),
            lastName: new FormControl(this.customer.lastName, [
              Validators.required
            ]),
            id: new FormControl(this.customer.id),
            email: new FormControl(this.customer.email),
            password: new FormControl(this.customer.password),
            active: new FormControl(this.customer.active),
            createdAt: new FormControl(this.customer.createdAt),
            modifiedTime: new FormControl(this.customer.modifiedTime),
          });
        }
        else {
          this.router.navigateByUrl('/')
        }
      });
  }

  ngOnInit() {

  }

  onSubmit(editProfileData) {
    this.customerService.editCustomer(editProfileData)
      .subscribe(data => {
        if (data.id) {
          this.router.navigateByUrl('profile');
        } else {
          alert('Not found');
        }
      });
  }

  get firstName() {
    return this.editProfileForm.get('firstName')
  }

  get middleName() {
    return this.editProfileForm.get('middleName')
  }

  get lastName() {
    return this.editProfileForm.get('lastName')
  }
}
