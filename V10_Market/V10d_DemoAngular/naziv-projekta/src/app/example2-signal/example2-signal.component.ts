import { Component, OnInit, signal, effect } from '@angular/core';

@Component({
  selector: 'app-example2-signal',
  standalone: false,
  templateUrl: './example2-signal.component.html',
  styleUrl: './example2-signal.component.scss',
})
export class Example2SignalComponent {

  ngOnInit(): void {
  }

  counter = signal(0);  // ← SAMO OVO PROMIJENI
  interval = 1000;
  running = false;

  tick() {
    if (!this.running) return;

    this.counter.update(c => c + 1);  // ← I OVO
    console.log(`Novi setTimeout za interval = ${this.interval} za counter = ${this.counter()}.`)
    // Obriši cd.markForCheck() - više nije potreban!

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
