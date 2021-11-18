import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { NavItem } from '../../models/nav-item.model';
import { User } from '../../models/user.model';
import { AuthenticationService } from '../../services/authentication.service';
import { CartManagementService } from '../../services/cart-management.service';
import { NotificationsService } from '../../services/notifications.service';
import { PageInfoService } from '../../services/page-info.service';
import { RouterParametersService } from '../../services/router-parameters.service';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {
  public readonly navItems: NavItem[] = [
    new NavItem({ pageCode: 'bs-books', pageTitle: 'Books', relativeUrl: 'books/list', sortOrder: 1 }),
    //new NavItem({ pageCode: 'bs-contacts', pageTitle: 'Contacts', relativeUrl: 'contacts', sortOrder: 2 })
  ];
  public currentUrl: string | undefined;
  public currentUser: User | null = null;
  public cartProductsCount: number = 0;
  public hasNewNotifications: boolean = false;

  private destroy$ = new Subject<void>();

  constructor(private authenticationService: AuthenticationService,
              private routerParameterService: RouterParametersService,
              private pageInfoService: PageInfoService,
              private cartManagementService: CartManagementService,
              private notificationSercies: NotificationsService) { }

  ngOnInit(): void {
    this.pageInfoService.urlWithoutQuery$.pipe(takeUntil(this.destroy$))
      .subscribe(url => this.currentUrl = url);
    this.authenticationService.currentUser$.pipe(takeUntil(this.destroy$))
      .subscribe(user => this.currentUser = user);
    this.cartManagementService.cartProductsExtended$.pipe(takeUntil(this.destroy$))
      .subscribe(cProducts =>
        this.cartProductsCount = cProducts.reduce((sum, curr) => sum += curr.amount, 0)
      );
    this.notificationSercies.notifications$.pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.hasNewNotifications = this.notificationSercies.hasNewNotifications;
      });
  }

  public onLinkClick(url: string): void {
    if (this.currentUrl === url) return;
    this.routerParameterService.updateRoute([url]);
  }

  public onLogout(): void {
    this.cartManagementService.saveCurrentCart()
      .subscribe(() => this.authenticationService.logout());
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
