import {Component, Input, TemplateRef} from '@angular/core';
import {
  CalendarSchedulerEvent
} from './models';
import {AppComponent} from '../../../src/app/app.component';

@Component({
  selector: 'calendar-scheduler-event-title',
  template: `
    <ng-template #defaultTemplate>

      <calendar-scheduler-event-actions
        *ngIf="showActions && !showContent && (event.isClickable || event.isDisabled)"
        class="no-content-actions"
        [event]="event">
      </calendar-scheduler-event-actions>

      <div *ngIf="event.status && showStatus"
           class="cal-scheduler-event-status"
           [class.ok]="event.status === 'ok'"
           [class.warning]="event.status === 'warning'"
           [class.danger]="event.status === 'danger'"
           [class.Dajana]="event.status === 'Dajana'"
           [class.Arnesa]="event.status === 'Arnesa'">
        {{event.person}}
      </div>

      <div *ngIf="isLoggedIn()"
           class="cal-scheduler-event-title"
           [innerHTML]="event.title | schedulerEventTitle:view:event">
      </div>

    </ng-template>

    <ng-template
      [ngTemplateOutlet]="customTemplate || defaultTemplate"
      [ngTemplateOutletContext]="{
                view: view,
                showStatus: showStatus,
                event: event,
                showContent: showContent,
                showActions: showActions
            }">
    </ng-template>
  `,
  host: {
    'class': 'cal-scheduler-event-title-container'
  }
})
export class CalendarSchedulerEventTitleComponent {

  @Input() view: string;

  @Input() event: CalendarSchedulerEvent;

  @Input() showStatus: boolean = true;

  @Input() showContent: boolean = true;

  @Input() showActions: boolean = true;

  @Input() customTemplate: TemplateRef<any>;

  isLoggedIn() {
    return AppComponent.loggedIn != null;
  }
}
