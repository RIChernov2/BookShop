import { Component, OnInit } from '@angular/core';
import { NavItem } from 'src/app/common/models/nav-item.model';
import { PageInfoService } from 'src/app/common/services/page-info.service';
import { RouterParametersService } from 'src/app/common/services/router-parameters.service';


@Component({
  selector: 'profile-navigation',
  templateUrl: './profile-navigation.component.html',
  styleUrls: ['./profile-navigation.component.scss']
})
export class ProfileNavigationComponent implements OnInit {
  public readonly navItems: NavItem[] = [
    new NavItem({ pageCode: 'pp-info', pageTitle: 'Info', relativeUrl: 'profile/info', sortOrder: 1 }),
    new NavItem({ pageCode: 'pp-notifications', pageTitle: 'Notifications', relativeUrl: 'profile/notifications', sortOrder: 2 }),
    new NavItem({ pageCode: 'pp-order-list', pageTitle: 'Order list', relativeUrl: 'profile/orders/list', sortOrder: 3 }),
    new NavItem({ pageCode: 'pp-cart', pageTitle: 'Cart', relativeUrl: 'profile/cart', sortOrder: 4 })
  ];
  public currentUrl: string | undefined;

  constructor(private routerParameterService: RouterParametersService,
              private pageInfoService: PageInfoService,) { }

  ngOnInit(): void {
    this.pageInfoService.urlWithoutQuery$.subscribe(url => this.currentUrl = url);
  }

  public onLinkClick(url: string): void {
    if (this.currentUrl === url) return;
    this.routerParameterService.updateRoute([url]);
  }

}
