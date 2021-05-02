import {Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { map } from 'rxjs/operators';

import * as moment from 'moment';
import {Subject} from 'rxjs';

import {
  CalendarSchedulerEvent,
  CalendarSchedulerEventStatus,
  CalendarSchedulerEventAction
} from 'angular-calendar-scheduler';
import {
  addDays,
  startOfHour,
  addHours,
  subHours,
  setHours,
  subMinutes,
  addMinutes,
  add
} from 'date-fns';
import { Observable, Subscription } from 'rxjs';


@Injectable()
export class AppService {

  //events: CalendarSchedulerEvent[] = JSON.parse(localStorage.getItem('events')) || [];
  events =[];
  max = 0;
  initialized = false;

  initialize(actions: CalendarSchedulerEventAction[]) {

    this.http.get<any>('http://109.237.39.237:7777/get-all',{}).subscribe(data => {


      data.forEach(event => {              

          let addEvent = ({
            id: event.id + '',
            start: event.start,
            end: event.end,
            person: event.person,
            title: event.title,
            content: event.content,
            color: event.color,
            actions: actions,
            status: event.status,
            isClickable: event.isClickable,
            isDisabled: event.isDisabled,
            draggable: event.draggable,
            resizable: event.resizable
          });

          if(Number(addEvent.id) >  this.max )
          this.max=Number(addEvent.id)+1;

          addEvent.id = addEvent.id.replace(/"/g,"");
          addEvent.start = addEvent.start.replace(/"/g,"");
          addEvent.end = addEvent.end.replace(/"/g,"");
          addEvent.person = addEvent.person.replace(/"/g,"");
          addEvent.title = addEvent.title.replace(/"/g,"");
          addEvent.content = addEvent.content.replace(/"/g,"");
         // addEvent.color = addEvent.color.replace(/"/g,"");
          addEvent.color = JSON.parse(addEvent.color)
          addEvent.status = addEvent.status.replace(/"/g,"");
          addEvent.isClickable = addEvent.isClickable.replace(/"/g,"");
          addEvent.isDisabled = addEvent.isDisabled.replace(/"/g,"");
          addEvent.draggable = addEvent.status.replace(/"/g,"");
          addEvent.resizable = addEvent.resizable.replace(/"/g,"");

          addEvent.start=new Date(addEvent.start);
          addEvent.end=new Date(addEvent.end);

         // {primary: this.color, secondary: this.color},

         console.log(addEvent.color);

         //addEvent = addEvent.replace(/\\"/g,"");

          console.log(addEvent);

          this.events.push(addEvent);



      });


    });

   /* this.max = this.events.length ? Math.max(...this.events.map(x => parseInt(x.id, 10))) + 1 : 1;

    for (let i = 0; i < this.events.length; i++) {;
      this.events[i].start = new Date(this.events[i].start.toString());
      this.events[i].end = new Date(this.events[i].end.toString());
      this.events[i].actions = actions;
    }*/

  }

  initializeEvents(){
    this.events = JSON.parse(localStorage.getItem('events')) ;
  }

  getEvents(actions: CalendarSchedulerEventAction[]): Promise<CalendarSchedulerEvent[]> {

    if (this.initialized === false) {
      this.initialized = true;
      this.initialize(actions);
    }

    return new Promise(resolve => setTimeout(() => resolve(this.events), 3000));

  }

  constructor(private http: HttpClient ) {
  }


   addNewEvent(event: any, refresh :Subject<any>){

    return this.http.post<any>('http://109.237.39.237:7777/add-new', { info: event }).subscribe(data => {
       
        if(data.message != "Added")return false;

        console.log("I added "+this.events);

        this.events.push(event);
        console.log("I added "+JSON.stringify(this.events)+"\n\n"+event);
        localStorage.setItem('events', JSON.stringify(this.events));
        this.max++;
        refresh.next();
        return true;

    })



    

  
  }

  removeEvent(id: string) {

    for (let i = 0; i < this.events.length; i++) {
      if (this.events[i].id === id) {
        this.events.splice(i, 1);
        localStorage.setItem('events', JSON.stringify(this.events));
        return;
      }
    }


  }

  editEvent(id: string, event: any) {

    const eventz = this.events.find(x => x.id === id);


    eventz.start = event.start;
    eventz.end = event.end;
    eventz.title = event.title;
    eventz.content = event.content;
    eventz.person = event.person;
    eventz.color = event.color;
    localStorage.setItem('events', JSON.stringify(this.events));

  }

  getMax() {
    return this.max;
  }


}


