import { Component, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public address;
  private _http;
  private _baseUrl;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._http = http;
    this._baseUrl = baseUrl;
  }

  public searchAddress() {
    let params = new HttpParams().set("address", this.address);
    this._http.get(this._baseUrl + 'address', { params: params }).subscribe(result => {
      if (result.validAddress) {
        window.location.href = '/fetch-data?lat=' + result.latitude + '&lng=' + result.longitude + '&address=' + result.formattedAddress;
      }
      else {
        alert(result.enteredAddress + ' is not a valid address');
      }
    }, error => console.error(error));
  }
}


