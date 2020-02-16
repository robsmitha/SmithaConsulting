import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
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
  private id: number

  constructor(private customerService: CustomerService,
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService) {

    this.id = authService.CustomerId
    this.customerService.loadCustomer(this.id).
      subscribe(data => {
        this.editProfileForm = this.formBuilder.group({
          id: data.id,
          firstName: data.firstName,
          lastName: data.lastName,
          middleName: data.middleName,
          email: data.email,
          password: data.password,
          active: data.active,
          createdAt: data.createdAt,
          modifiedTime: data.modifiedTime
        });
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
}
