import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './features/products/products.component';
import { ClientsComponent } from './features/clients/clients.component';
import { HomeComponent } from './home/home.component';
import { ProductAddComponent } from './features/products/product-add/product-add.component';
import { ProductEditComponent } from './features/products/product-edit/product-edit.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { ClientAddComponent } from './features/clients/client-add/client-add.component';
import { ClientEditComponent } from './features/clients/client-edit/client-edit.component';
import { OrdersComponent } from './features/orders/orders.component';
import { OrderAddComponent } from './features/orders/order-add/order-add.component';
import { OrderEditComponent } from './features/orders/order-edit/order-edit.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'products', component: ProductsComponent },
  { path: 'product-add', component: ProductAddComponent },
  { path: 'product-edit', component: ProductEditComponent },
  { path: 'clients', component: ClientsComponent },
  { path: 'client-add', component: ClientAddComponent },
  { path: 'client-edit', component: ClientEditComponent },  
  { path: 'orders', component: OrdersComponent },
  { path: 'order-add', component: OrderAddComponent },
  { path: 'order-edit', component: OrderEditComponent },    
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
