import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './components/pages/products/products.component';
import { PurchaseComponent } from './components/pages/purchase/purchase.component';
import { SupplierComponent } from './components/pages/supplier/supplier.component';
import { SaleComponent } from './components/pages/sale/sale.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AuthGuard } from './guards/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AdminGuard } from './guards/admin.guard';
import { ManagerGuard } from './guards/manager.guard';
import { StaffGuard } from './guards/staff.guard';
import { UsersComponent } from './components/pages/users/users.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { 
    path: 'dashboard', 
    component: DashboardComponent, 
    canActivate: [AuthGuard]
  },
  { 
    path: 'products', 
    component: ProductsComponent, 
    canActivate: [AuthGuard]
  },
  { 
    path: 'suppliers', 
    component: SupplierComponent, 
    canActivate: [AuthGuard,ManagerGuard]
  },
  { 
    path: 'purchases', 
    component: PurchaseComponent, 
    canActivate: [AuthGuard]
  },
  { 
    path: 'sales', 
    component: SaleComponent, 
    canActivate: [AuthGuard, AdminGuard]
  },
  {
    path: 'users',
    component : UsersComponent,
    canActivate :[AuthGuard, AdminGuard]
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }