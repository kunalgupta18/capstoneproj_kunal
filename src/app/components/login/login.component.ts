import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from 'src/app/Services/common.service';

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
  
    const user = {
      username: this.loginForm.value.username,
      password: this.loginForm.value.password,
    };
  
    this.http.post<any>('https://localhost:7094/api/Login', user).subscribe(
      (response) => {
        sessionStorage.setItem('token', response.token);
        sessionStorage.setItem('role', response.role);
        sessionStorage.setItem('name', this.loginForm.value.username)
        
        if (this.rememberMe) {
          localStorage.setItem('username', user.username);
          localStorage.setItem('password', user.password);
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
      }
    );
  }


  redirectToSignup() {
    this.router.navigateByUrl('/signup');
  }
}
