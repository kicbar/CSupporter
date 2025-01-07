import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  selectedTabIndex = 0;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.getForecasts();
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        switch (event.url) {
          case '/':
            this.selectedTabIndex = 0;
            break;
          case '/clients':
            this.selectedTabIndex = 1;
            break;
          case '/products':
            this.selectedTabIndex = 2;
            break;
        }
      }
    });    
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'rkanchor.client';

  onTabChange(index: number): void {
    switch (index) {
      case 0:
        this.router.navigate(['/']);
        break;
      case 1:
        this.router.navigate(['/clients']);
        break;
      case 2:
        this.router.navigate(['/products']);
        break;
    }
  }
}
