import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  selectedTabIndex = 0;

  constructor(private router: Router) {}

  ngOnInit() {
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
