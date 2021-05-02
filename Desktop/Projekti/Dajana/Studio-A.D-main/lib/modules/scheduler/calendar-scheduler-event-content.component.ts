import { Component, Input, ElementRef, AfterViewInit } from '@angular/core';
import {
    CalendarSchedulerEvent
} from './models';
import {AppComponent} from '../../../src/app/app.component';

@Component({
    selector: 'calendar-scheduler-event-content',
    template: `
        <div *ngIf="event.content && isLoggedIn()"
            class="cal-scheduler-event-content"
            [style.max-height.px]="maxHeight"
            [innerHTML]="maxHeight && maxHeight >= 30 ? event.content : event.content">
        </div>
    `,
    host: {
        'class': 'cal-scheduler-event-content-container'
    }
})
export class CalendarSchedulerEventContentComponent implements AfterViewInit {

    @Input() event: CalendarSchedulerEvent;

    @Input() eventContainer: HTMLElement;

    maxHeight: number;

    constructor(private hostElement: ElementRef) {  }

    public ngAfterViewInit(): void {
        setTimeout(() => { this.maxHeight = this.eventContainer.clientHeight - 70; }, 0);
    }

  isLoggedIn() {
    return AppComponent.loggedIn != null;
  }
}
