import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Purchase } from '../Models/PurchaseModel';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {
  private apiUrl = 'https://localhost:7094/api/Purchase'; 

  constructor(private http: HttpClient) {}

  getAllPurchases(): Observable<Purchase[]> {
    return this.http.get<Purchase[]>(this.apiUrl);
  }

  getPurchaseById(purchaseId: number): Observable<Purchase> {
    return this.http.get<Purchase>(`${this.apiUrl}/${purchaseId}`);
  }

  addPurchase(purchase: Purchase): Observable<Purchase> {
    return this.http.post<Purchase>(this.apiUrl, purchase);
  }

  updatePurchase(purchase: Purchase): Observable<Purchase> {
    return this.http.put<Purchase>(`${this.apiUrl}/${purchase.purchaseId}`, purchase);
  }

  deletePurchase(purchaseId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${purchaseId}`);
  }
}