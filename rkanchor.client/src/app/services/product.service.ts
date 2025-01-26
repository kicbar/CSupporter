import { HttpClient } from "@angular/common/http";
import { Product } from "../models/product.model";
import { BehaviorSubject, filter, Observable, Subject, tap } from "rxjs";
import { Injectable } from "@angular/core";
import { ProductDto } from "../models/product.dto";
import { environment } from "../../envrinments/environments";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = environment.apiBaseUrl;
  public products: Product[] = [];
  private productSelectedSubject = new BehaviorSubject<Product | null>(null);
  productSelected$ = this.productSelectedSubject.asObservable();
  private refreshProductsSubject = new Subject<void>(); 
  refreshProductsSubject$ = this.refreshProductsSubject.asObservable();

  constructor(private http: HttpClient) { }
  getProduct(productId: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/Product/${productId}`);
  }

  getAllProducts(): Observable<Product[]> {
      return this.http.get<Product[]>(`${this.baseUrl}/Product`);
  }

  addProduct(product: ProductDto): Observable<Product> {
    return this.http.post<Product>(`${this.baseUrl}/Product`, product);
  }

  editProduct(productId: number, product: ProductDto): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}/Product/${productId}`, product);
  }

  removeProduct(productId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/Product/${productId}`).pipe(
      filter((respone) => respone === true), 
      tap(() => this.refreshProductsSubject.next())
    );
  }

  selectProduct(product: Product): void {
    this.productSelectedSubject.next(product);
  }

}
