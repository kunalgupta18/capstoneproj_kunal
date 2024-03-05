import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { users } from '../Models/Users';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  apiUrl = 'https://localhost:7094/api/Client';

  constructor(private http: HttpClient) {}

  getUsers(): Observable<users[]> {
    return this.http.get<users[]>(this.apiUrl);
  }

  deleteUser(id: number): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete(url);
  }
}
