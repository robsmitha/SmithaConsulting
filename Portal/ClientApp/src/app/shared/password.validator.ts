import { AsyncValidator, AbstractControl, ValidationErrors } from "@angular/forms";
import { Injectable } from "@angular/core";
import { CustomerService } from "../services/customer.service";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class PasswordValidator implements AsyncValidator {
  constructor(private customerService: CustomerService) { }
  validate(ctrl: AbstractControl):
    Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {

    return null;
  }
}
