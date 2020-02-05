import { Component, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  private lat;
  private lng;
  public address;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this.route.queryParams.subscribe(params => {
      this.lat = params['lat'];
      this.lng = params['lng'];
      this.address = params['address'];
    });
    let params = new HttpParams().set("lat", this.lat).set("lng", this.lng);
    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast', { params: params }).subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
