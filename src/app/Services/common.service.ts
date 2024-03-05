import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  loginStatus = new BehaviorSubject<boolean>(false)
  loginStatus$ = this.loginStatus.asObservable()

  isAdmin = new BehaviorSubject<boolean>(false)
  isAdmin$ = this.isAdmin.asObservable()
  
  isManager = new BehaviorSubject<boolean>(false)
  isManager$ = this.isManager.asObservable()

  isStaff = new BehaviorSubject<boolean>(false)
  isStaff$ = this.isStaff.asObservable()
  
  constructor() { }
}
