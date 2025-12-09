import {ChangeDetectorRef, Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-example2-manualchangedetection',
  standalone: false,
  templateUrl: './example2-manualchangedetection.component.html',
  styleUrl: './example2-manualchangedetection.component.scss',
})
export class Example2ManualChangeDetectionComponent implements OnInit {

  constructor(private cd: ChangeDetectorRef) {
  }

  ngOnInit(): void {
  }

  counter = 0;
  interval = 1000;
  running = false;

  tick() {
    if (!this.running) return;

    this.counter++;
    console.log(`Novi setTimeout za interval = ${this.interval} za counter = ${this.counter}.`)
    //this.cd.markForCheck(); // angular ne prepoznaje izmjene nastale na klasicnim js interavom (setTimeout)

    setTimeout(() => this.tick(), this.interval);
  }

  startTimer() {
    if (this.running) return;
    this.running = true;
    this.tick();
  }

  stopTimer() {
    this.running = false;
  }

}
