import {Component, OnInit, signal} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.scss'
})
export class App implements OnInit {
  selectedComponent = 0;

  ngOnInit(): void {

  }
/*
  badUseQueryString() {
    const params = new URLSearchParams(window.location.search);
    const komp = params.get("komponenta") ?? 0;
    this.selectedComponent = +komp;
  }*/

  protected readonly title = signal('naziv-projekta');
}
