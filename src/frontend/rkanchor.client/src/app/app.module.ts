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
import { ClientListComponent } from './clients/client-list/client-list.component';
import { ClientService } from './services/client.service';
import { AuthService } from './services/auth.service';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { DictionaryService } from './services/dictionary.service';
import { NotificationService } from './services/notification.service';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';

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
    ClientListComponent,
    LoginComponent,
    RegisterComponent,
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
