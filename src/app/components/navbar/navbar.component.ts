import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService } from 'src/app/Services/common.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit{
  username!:string  
  role!:string
  constructor(public commonService: CommonService, private router: Router){}
  logout(){
    sessionStorage.clear()
    this.commonService.isAdmin.next(false)
    this.commonService.loginStatus.next(false)
    this.commonService.isManager.next(false)
    this.commonService.isStaff.next(false)
    this.router.navigate(['/login'])
  }
  ngOnInit(): void {
      this.username = sessionStorage.getItem('name') as string
      this.role = sessionStorage.getItem('role') as string
  }
}
