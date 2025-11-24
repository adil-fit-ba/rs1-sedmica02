import { Component } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-header-menu',
  standalone: false,
  templateUrl: './header-menu.html',
  styleUrl: './header-menu.scss',
})
export class HeaderMenu {
  constructor(private router:Router) {
  }
  /*
  goTo(number: number) {
    let newPath = '?komponenta=' + number;
    window.location.href = newPath;
    //window.history.replaceState({}, "", newPath);
  }*/
}
