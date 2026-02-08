import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsComponent } from './products/products.component';
import { ProductsListComponent } from './products/products-list/products-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTabsModule } from '@angular/material/tabs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ClientsComponent } from './clients/clients.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductListItemComponent } from './products/products-list/product-list-item/product-list-item.component';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { ProductAddComponent } from './products/product-add/product-add.component';
import { ProductService } from './services/product.service';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { ClientDetailsComponent } from './clients/client-details/client-details.component';
import { ClientAddComponent } from './clients/client-add/client-add.component';
import { ClientsListComponent } from './clients/clients-list/clients-list.component';
import { ClientService } from './services/client.service';
import { AuthService } from './services/auth.service';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { DictionaryService } from './services/dictionary.service';
import { NotificationService } from './services/notification.service';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { ClientListItemComponent } from './clients/clients-list/client-list-item/client-list-item.component';
import { ClientEditComponent } from './clients/client-edit/client-edit.component';
import { OrdersComponent } from './orders/orders.component';
import { OrderAddComponent } from './orders/order-add/order-add.component';
import { OrderEditComponent } from './orders/order-edit/order-edit.component';
import { OrdersListComponent } from './orders/orders-list/orders-list.component';
import { OrderDetailsComponent } from './orders/order-details/order-details.component';
import { OrderListItemComponent } from './orders/orders-list/order-list-item/order-list-item.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductsComponent,
    ProductsListComponent,
    ProductListItemComponent,
    ProductDetailsComponent,
    ProductAddComponent,
    ClientsComponent,
    ProductAddComponent,
    ConfirmationDialogComponent,
    ProductEditComponent,
    ClientDetailsComponent,
    ClientAddComponent,
    ClientsListComponent,
    LoginComponent,
    RegisterComponent,
    ClientListItemComponent,
    ClientEditComponent,
    OrdersComponent,
    OrderAddComponent,
    OrderEditComponent,
    OrdersListComponent,
    OrderDetailsComponent,
    OrderListItemComponent
  ],
  imports: [
    BrowserModule, 
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatTabsModule,
    MatFormFieldModule, 
    MatInputModule,    
    MatButtonModule,
    MatDialogModule,
    MatSnackBarModule,
    MatSelectModule,
    MatOptionModule,
    MatDatepickerModule,
    MatNativeDateModule    
  ],
  providers: [
    ProductService,
    ClientService,
    AuthService,
    DictionaryService,
    NotificationService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
