import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-success-popup',
  imports: [],
  templateUrl: './success-popup.component.html',
  styleUrl: './success-popup.component.scss',
  animations: [
    trigger('fadeInOut', [
      state(
        'void',
        style({
          opacity: 0,
          transform: 'translateY(-20px)',
        })
      ),
      transition('void <=> *', animate('300ms ease-in-out')),
    ]),
  ],
})
export class SuccessPopupComponent implements OnInit, OnDestroy {
  @Input() message: string = '';
  @Input() duration: number = 5000; // Default 5 seconds

  visible: boolean = false;
  progressWidth: number = 100;
  private timeout: any;
  private progressInterval: any;

  ngOnInit(): void {
    // Make visible when component is initialized
    this.visible = true;
    this.startTimer();
  }

  ngOnDestroy(): void {
    // Clean up to prevent memory leaks
    this.clearTimers();
  }

  hidePopup(): void {
    this.visible = false;
    this.clearTimers();
  }

  private startTimer(): void {
    // Set up progress bar
    const updateFrequency = 10; // Update every 10ms
    const steps = this.duration / updateFrequency;
    const decrement = 100 / steps;

    this.progressInterval = setInterval(() => {
      this.progressWidth -= decrement;
      if (this.progressWidth <= 0) {
        this.progressWidth = 0;
      }
    }, updateFrequency);

    // Set timeout for hiding the popup
    this.timeout = setTimeout(() => {
      this.hidePopup();
    }, this.duration);
  }

  private clearTimers(): void {
    if (this.timeout) {
      clearTimeout(this.timeout);
    }
    if (this.progressInterval) {
      clearInterval(this.progressInterval);
    }
  }
}
