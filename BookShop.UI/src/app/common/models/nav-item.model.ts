export class NavItem {
  pageCode: string = '';
  pageTitle: string = '';
  relativeUrl: string = '';
  sortOrder: number = 0;

  constructor(init?: Partial<NavItem>) {
    Object.assign(this, init);
  }
}