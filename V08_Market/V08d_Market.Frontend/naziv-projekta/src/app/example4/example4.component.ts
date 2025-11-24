import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-example4',
  standalone: false,
  templateUrl: './example4.component.html',
  styleUrl: './example4.component.scss',
})
export class Example4Component implements OnInit {
  constructor(private httpClient: HttpClient) {
  }

  ngOnInit() {
    this.httpClient.get('https://localhost:7260/Products').subscribe(x=>{
      console.log(x);
    });
  }
}
