import {ChangeDetectorRef, Component} from '@angular/core';

@Component({
  selector: 'app-example3',
  standalone: false,
  templateUrl: './example3.html',
  styleUrl: './example3.css',
})
export class Example3Component {

  counter = 0;
  interval = 1000;
  running = false;
  private timeoutId: any = null;

  public tick(): void {
    if (!this.running) return;

    this.counter++;
    this.cd.detectChanges();//angular ne prepozna izmjene nastale sa klasičnim intervalom (npr setTimeout)

    this.timeoutId = setTimeout(() => this.tick(), this.interval);
  }

  constructor(private cd: ChangeDetectorRef) {
  }


  startTimer(): void {
    if (this.running) return;
    this.running = true;
    this.tick();
  }

  stopTimer(): void {
    this.running = false;
    if (this.timeoutId !== null) {
      clearTimeout(this.timeoutId);
      this.timeoutId = null;
    }
  }
}
