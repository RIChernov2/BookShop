import { Injectable } from '@angular/core';
import { Router, Event, NavigationEnd } from '@angular/router';
import { filter, map, distinctUntilChanged } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class PageInfoService {
  private readonly urlRegexp = new RegExp(/\/([\w-\/]*)(\?|$)/); // get everything between '/' and '?'
  private urlWithoutQueryEmitter = new ReplaySubject<string>(1);
  public urlWithoutQuery$ = this.urlWithoutQueryEmitter.asObservable();

  constructor(private router: Router) {
    this.router.events.pipe(
      filter( (e: Event) => e instanceof NavigationEnd ),
      map( (e: Event) => {
        const navEndEvent = e as NavigationEnd;
        return navEndEvent.urlAfterRedirects || navEndEvent.url
      }),
      filter( url => this.urlRegexp.test(url) ),
      map( url => url.match(this.urlRegexp)![1] ),
      distinctUntilChanged()
    ).subscribe( urlWithoutQuery => this.urlWithoutQueryEmitter.next(urlWithoutQuery) );
  }

}
