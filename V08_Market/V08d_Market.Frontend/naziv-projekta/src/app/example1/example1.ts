import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-example1',
  standalone: false,
  templateUrl: './example1.html',
  styleUrl: './example1.css',
})
export class Example1Component implements OnInit {

  counterDisplay: HTMLInputElement | null = null;
  slider: HTMLInputElement | null = null;
  sliderValue: HTMLInputElement | null = null;

  private running = false;
  private counter = 0;

  ngOnInit(): void {
    this.counterDisplay = document.querySelector('#counterDisplay') as HTMLInputElement | null;
    this.slider = document.querySelector('#speedSlider') as HTMLInputElement | null;
    this.sliderValue = document.querySelector('#sliderValue') as HTMLInputElement | null;

    if (this.counterDisplay == null)
      throw 'counterDisplay == null';
    if (this.slider == null)
      throw 'slider == null';
    if (this.sliderValue == null)
      throw 'sliderValue == null';

    this.slider!.oninput = () => {
      console.log(` Nova vrijednost za varijablu slider.value = ${this.slider!.value}.`)
      this.sliderValue!.textContent = this.slider!.value;
    };
  }

  tick() {
    if (!this.running) return;

    this.counter++;
    if (this.counterDisplay) {
      this.counterDisplay.value = this.counter.toString();
    }

    if (!this.slider) return;

    // svaki put nanovo uzima interval iz slidera
    const interval = parseInt(this.slider.value, 10);
    console.log(`Novi setTimeout za interval = ${interval}ms`);
    setTimeout(() => this.tick(), interval);
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
