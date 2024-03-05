import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Display } from '../Models/Display';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DisplayService {
  private apiUrl = 'https://localhost:7094/api/Display'; // Replace <API_URL> with your actual API URL

  constructor(private http: HttpClient) { }

  getSummary(): Observable<Display> {
      return this.http.get<Display>(this.apiUrl);
  }
}
