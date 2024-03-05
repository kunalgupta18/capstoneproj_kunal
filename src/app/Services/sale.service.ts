import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sales } from '../Models/SaleModel';
@Injectable({
  providedIn: 'root'
})
export class SaleService {
  private apiUrl = 'https://localhost:7094/api/Sales';

  constructor(private http: HttpClient) { }

  getSales(): Observable<Sales[]> {
    return this.http.get<Sales[]>(this.apiUrl);
  }

  addSales(sales: Sales): Observable<Sales> {
    return this.http.post<Sales>(this.apiUrl, sales);
  }

  updateSales(sales: Sales): Observable<Sales> {
    return this.http.put<Sales>(`${this.apiUrl}/${sales.salesId}`, sales);
  }

  deleteSales(salesId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${salesId}`);
  }
}
