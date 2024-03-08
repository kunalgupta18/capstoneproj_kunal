import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ProductsComponent } from './components/pages/products/products.component';
import { PurchaseComponent } from './components/pages/purchase/purchase.component';
import { SupplierComponent } from './components/pages/supplier/supplier.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SaleComponent } from './components/pages/sale/sale.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UsersComponent } from './components/pages/users/users.component';
import { MatDialogModule } from '@angular/material/dialog'
//import { NgxBarcodeScannerModule } from 'ngx-barcode-scanner';


const routes: Routes = [
  { path: 'products', component: ProductsComponent },
  { path: 'suppliers', component: SupplierComponent },
  { path: 'purchases', component:PurchaseComponent}
];
@NgModule({
  declarations: [
    AppComponent,
    PurchaseComponent,
    ProductsComponent,
    SupplierComponent,
    NavbarComponent,
    SaleComponent,
    LoginComponent,
    SignupComponent,
    DashboardComponent,
    UsersComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    MatDialogModule,
    //QRCodeModule
    //NgxBarcodeScannerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
