import { Component, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {
  @ViewChild('usernameInput', { static: false }) usernameInput!: ElementRef<HTMLInputElement>;
  @ViewChild('passwordInput', { static: false }) passwordInput!: ElementRef<HTMLInputElement>;
  @ViewChild('firstnameInput', { static: false }) firstnameInput!: ElementRef<HTMLInputElement>;
  @ViewChild('lastnameInput', { static: false }) lastnameInput!: ElementRef<HTMLInputElement>;
  @ViewChild('roleInput', { static: false }) roleInput!: ElementRef<HTMLSelectElement>;
  isFormValid: boolean = false;

  constructor(private http: HttpClient, private router: Router) { }

  signup() {
    const username = this.usernameInput.nativeElement.value;
    const password = this.passwordInput.nativeElement.value;
    const firstname = this.firstnameInput.nativeElement.value;
    const lastname = this.lastnameInput.nativeElement.value;
    const role = this.roleInput.nativeElement.value;
    const user = { username, password, firstname, lastname, role }

    this.http.post<any>('https://localhost:7094/api/Signup', user).subscribe(response => {
      // Success, do something with the response
      console.log(response);
      alert('User Added');
    }, error => {
      // Error handling
      console.error(error);
      alert('Username Already Taken');
    });
  }

  checkFormValidity() {
    const username = this.usernameInput.nativeElement.value;
    const password = this.passwordInput.nativeElement.value;
    this.isFormValid = username.trim().length > 0 && password.trim().length > 0;
  }

  redirectToLogin() {
    this.router.navigateByUrl('/login');
  }
}