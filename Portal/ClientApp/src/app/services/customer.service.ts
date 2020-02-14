
import { Injectable } from '@angular/core'
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Customer } from '../classes/customer';

@Injectable()
export class CustomerService {

  constructor(private client: HttpClient) {}

  signInCustomer(data: Customer): Observable<any> {
    return this.client.post('account/signin', data);
  }
  signUpCustomer(data: Customer): Observable<any> {
    return this.client.post('account/signup', data);
  }
  loadCustomer(id: number): Observable<any> {
    let params = new HttpParams().set("id", id.toString());
    return this.client.get('account/profile', { params: params })
  }
}
