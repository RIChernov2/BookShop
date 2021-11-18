import { Injectable } from '@angular/core';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class RouterParametersService {
  private navigationQueue: Promise<boolean>;

  constructor(private router: Router) {
    this.navigationQueue = Promise.resolve(true);
  }

  public updateRoute(url: string[], queryParams: {[key: string]: any} = {}): Promise<boolean> {
    this.navigationQueue = this.navigationQueue.then(_ => 
      this.router.navigate(url, {
        queryParams,
        queryParamsHandling: 'merge'
      })
    );
    return this.navigationQueue;
  }

  public clearQuery(exceptParams: string[] = []): Promise<boolean> {
    const queryParams: {[key: string]: any} = {
      returnUrl: null
    };

    exceptParams.forEach( param => delete queryParams[param] );

    return this.updateRoute([], queryParams);
  }
}
