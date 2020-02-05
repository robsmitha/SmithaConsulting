import { Component, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
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
    if (this.address && this.address.length > 0) {
      let params = new HttpParams().set("address", this.address);
      this._http.get(this._baseUrl + 'address', { params: params }).subscribe(result => {
        if (result.validAddress) {
          window.location.href = '/fetch-data?lat=' + result.latitude + '&lng=' + result.longitude + '&address=' + result.formattedAddress;
        }
        else {
          alert(result.enteredAddress + ' is not a valid address.');
        }
      }, error => console.error(error));
    }
    else {
      alert('Please enter an address or location');
    }  
  }
  public useCurrentLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        var lat = position.coords.latitude;
        var lng = position.coords.longitude;
        if (lat && lng) {
          let params = new HttpParams().set("lat", String(lat)).set("lng", String(lng));
          this._http.get(this._baseUrl + 'address', { params: params }).subscribe(result => {
            if (result.validAddress) {
              window.location.href = '/fetch-data?lat=' + result.latitude + '&lng=' + result.longitude + '&address=' + result.formattedAddress;
            }
            else {
              alert('Could not determine address.');
            }
          }, error => console.error(error));
        }
        else {
          alert('Could not determine your location.');
        }
      });
    }
    else {
      alert('Your browser does not support geolocation.');
    } 
  }
}


