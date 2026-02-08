import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { ClientsComponent } from './clients/clients.component';
import { HomeComponent } from './home/home.component';
import { ProductAddComponent } from './products/product-add/product-add.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { ClientAddComponent } from './clients/client-add/client-add.component';
import { ClientEditComponent } from './clients/client-edit/client-edit.component';
import { OrdersComponent } from './orders/orders.component';
import { OrderAddComponent } from './orders/order-add/order-add.component';
import { OrderEditComponent } from './orders/order-edit/order-edit.component';

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
