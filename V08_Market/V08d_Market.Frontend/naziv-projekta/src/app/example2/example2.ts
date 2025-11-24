import {Component, ElementRef, OnInit} from '@angular/core';
import {Example1Component} from '../example1/example1';

@Component({
  selector: 'app-example2',
  standalone: false,
  templateUrl: './example2.html',
  styleUrl: './example2.css',
})
export class Example2Component implements OnInit {

  counterDisplay: HTMLInputElement | null = null;
  slider: HTMLInputElement | null = null;
  sliderValue: HTMLInputElement | null = null;

  private running = false;
  private counter = 0;
  private myComponent!: HTMLElement;

  constructor(private host: ElementRef<HTMLElement>) {}

  ngOnInit(): void {
    this.myComponent = this.host.nativeElement;

    this.counterDisplay = this.myComponent.querySelector('#counterDisplay') as HTMLInputElement | null;
    this.slider = this.myComponent.querySelector('#speedSlider') as HTMLInputElement | null;
    this.sliderValue = this.myComponent.querySelector('#sliderValue') as HTMLInputElement | null;

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
