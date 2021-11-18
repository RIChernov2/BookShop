import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from './common/guards/authentication.guard';
import { LoggedInGuard } from './common/guards/logged-in.guard';
import { ContactsPageComponent } from './pages/contacts-page/contacts-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { ProfileInfoComponent } from './pages/profile-page/components/profile-info/profile-info.component';
import { RootPageComponent } from './pages/root-page/root-page.component';


const routes: Routes = [
  {
    path: '',
    component: RootPageComponent,
    children: [
      {
        path: 'books',
        loadChildren: () => import('./pages/book-list-page/book-list-page.module').then(m => m.BookListPageModule)
      },
      {
        path: 'contacts',
        component: ContactsPageComponent
      },
      {
        path: 'profile',
        loadChildren: () => import('./pages/profile-page/profile-page.module').then(m => m.ProfilePageModule),
        canActivate: [AuthenticationGuard]
      },
      {
        path: 'login',
        component: LoginPageComponent,
        canActivate: [LoggedInGuard]
      },
      { path: '**', redirectTo: 'books', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
