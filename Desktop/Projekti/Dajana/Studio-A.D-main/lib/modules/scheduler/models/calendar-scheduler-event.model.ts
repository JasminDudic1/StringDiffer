import { EventColor } from 'calendar-utils';

export interface CalendarSchedulerEvent {
    id: string;
    start: Date;
    end?: Date;
    person: string;
    title: string;
    content?: string;
    color: EventColor;
    actions?: CalendarSchedulerEventAction[];
    status?: CalendarSchedulerEventStatus;
    cssClass?: string;
    isDisabled?: boolean;
    isClickable?: boolean;
    isCancelled?: boolean;
    resizable?: {
        beforeStart?: boolean;
        afterEnd?: boolean;
    };
    draggable?: boolean;
}

export type CalendarSchedulerEventStatus = 'ok' | 'warning' | 'danger' | 'Dajana' | "Arnesa" ;

export interface CalendarSchedulerEventAction {
    when?: 'enabled' | 'disabled' | 'cancelled';
    label: string;
    title: string;
    cssClass?: string;
    onClick(event: CalendarSchedulerEvent): void;
}
