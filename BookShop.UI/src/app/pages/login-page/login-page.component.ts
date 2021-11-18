import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginModel } from 'src/app/common/models/login.model';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { RouterParametersService } from 'src/app/common/services/router-parameters.service';


@Component({
  selector: 'login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit, OnDestroy {
  public loginForm: FormGroup;
  public pendingAuthentication: boolean = false;

  constructor(private routerParameterService: RouterParametersService,
              private authenticationService: AuthenticationService) {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  ngOnInit(): void {
  }

  public onSubmit(): void {
    this.pendingAuthentication = true;
    const loginModel = new LoginModel(this.loginForm.value.email, this.loginForm.value.password);
    this.authenticationService.login(loginModel).subscribe(
      () => {
        this.pendingAuthentication = false;
        this.routerParameterService.updateRoute(['profile'])
      },
      () => this.pendingAuthentication = false
    );
  }

  ngOnDestroy(): void {
    this.routerParameterService.clearQuery();
  }

}
