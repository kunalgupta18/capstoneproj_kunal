import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { CommonService } from './Services/common.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent{
  title="capstone-proj"
  isLoginPage: boolean = false;
  isSignUpPage: boolean = false;

  constructor(private router: Router, public commonService: CommonService) {
    const token = sessionStorage.getItem('token')
    if( token ) this.commonService.loginStatus.next(true)
    const role = sessionStorage.getItem('role') as string
    if( role === 'admin' ) this.commonService.isAdmin.next(true)
    if( role === 'manager' ) this.commonService.isManager.next(true)
    if( role === 'staff' ) this.commonService.isStaff.next(true)
  }
}
