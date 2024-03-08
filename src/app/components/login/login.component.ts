import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from 'src/app/Services/common.service';
import * as crypto from 'crypto-js'
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;
  public rememberMe: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    public commonService: CommonService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: [localStorage.getItem('username') || '', Validators.required],
      password: [localStorage.getItem('password') || '', Validators.required],
    });
  }

  public toggleRememberMe(): void {
    this.rememberMe = !this.rememberMe;
  }

  public login(): void {
    if (this.loginForm.invalid) {
      return;
    }
    const hash = crypto.SHA256(this.loginForm.value.password)
    //console.log(hash.toString())
    const user = {
      username: this.loginForm.value.username,
      password: hash.toString(),
    };
  
    this.http.post<any>('https://localhost:7094/api/Login', user).subscribe(
      (response) => {
        sessionStorage.setItem('token', response.token);
        sessionStorage.setItem('role', response.role);
        sessionStorage.setItem('name', this.loginForm.value.username)
        
        if (this.rememberMe) {
          localStorage.setItem('username', user.username);
          localStorage.setItem('password', this.loginForm.value.password);
        } else {
          localStorage.removeItem('username');
          localStorage.removeItem('password');
        }
  
        if (response.role === 'admin') {
          this.commonService.isAdmin.next(true);
        }
        if (response.role === 'manager') {
          this.commonService.isManager.next(true);
        }
        if (response.role === 'staff') {
          this.commonService.isStaff.next(true);
        }
        this.commonService.loginStatus.next(true);
        this.router.navigate(['/dashboard']);
      },
      (error) => {
        console.log(error);
        window.alert('Invalid credentials. Please try again.')
      }
    );
  }


  redirectToSignup() {
    this.router.navigateByUrl('/signup');
  }
}
