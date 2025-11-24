import {Component, OnInit} from '@angular/core';


@Component({
  selector: 'app-example1',
  standalone: false,
  templateUrl: './example1.component.html',
  styleUrl: './example1.component.scss',
})
export class Example1Component implements OnInit {


  ngOnInit(): void {
    this.counterDisplay = document.querySelector("#counterDisplay");
    this.slider = document.querySelector("#speedSlider");
    this.sliderValue = document.querySelector("#sliderValue");

    if (this.counterDisplay == null)
      throw 'counterDisplay == null';
    if (this.slider == null)
      throw 'slider == null';
    if (this.sliderValue == null)
      throw 'sliderValue == null';

    this.slider.oninput = () => {
      console.log(` Nova vrijednost za varijablu slider.value = ${this.slider.value}.`)
      this.sliderValue.textContent = this.slider.value;
    };
  }

  counter = 0;
  running = false;
  counterDisplay:any = null;
  slider:any = null;
  sliderValue:any = null;

  tick() {
    if (!this.running) return;

    this.counter++;
    this.counterDisplay!.value = this.counter;

    // svaki put nanovo uzima interval iz slidera
    const interval = parseInt(this.slider.value, 10);
    console.log(`Novi setTimeout za interval = ${interval}.`)
    setTimeout(()=>this.tick(), interval); //<--- ispravka nakon nastave | this unutar narednog timeout pozva se odnosi na java script globalni "window" objekat a ne na angular componentu
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
