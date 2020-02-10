import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
declare var google;
declare var Skycons;

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent implements OnInit {
  
  public forecast: WeatherForecast;
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
    http.get<WeatherForecast>(baseUrl + 'weatherforecast', { params: params }).subscribe(result => {
      this.forecast = result;
      var skycons = new Skycons();
      skycons.add("icon_currently_summary", this.forecast.currently.icon);
      skycons.play();
    }, error => console.error(error));
  }

  ngOnInit(): void {
    var latLng = { lat: parseFloat(this.lat), lng: parseFloat(this.lng) };
    let map = new google.maps.Map(document.getElementById('map'), {
      center: latLng,
      zoom: 10
    });
    var marker = new google.maps.Marker({
      position: latLng,
      map: map,
      title: this.address
    });
  }
  
}

interface WeatherForecast {
  currently: Currently,
  minutely: Minutely,
  hourly: Hourly,
  daily: Daily
}

interface Currently {
  date: string;
  temperature: number;
  feelsLikeTemperature: number;
  summary: string;
  icon: string;
}

interface Minutely {
  summary: string;
  icon: string;
  data: Array<WeatherData>;
}

interface Hourly {
  summary: string;
  icon: string;
  data: Array<WeatherData>;
}

interface Daily {
  summary: string;
  icon: string;
  data: Array<WeatherData>;
}

interface WeatherData {
  time: string;
  date: string;
  summary: string;
  icon: string;
  temperature: number;
  feelsLikeTemperature: number;
  temperatureHigh: number;
  temperatureLow: number;
}
