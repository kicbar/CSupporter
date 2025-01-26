import { HttpClient } from "@angular/common/http";
import { Product } from "../models/product.model";
import { BehaviorSubject, filter, Observable, Subject, tap } from "rxjs";
import { EventEmitter, Injectable } from "@angular/core";
import { ProductDto } from "../models/product.dto";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  public products: Product[] = [];
  private productSelectedSubject = new BehaviorSubject<Product | null>(null);
  productSelected$ = this.productSelectedSubject.asObservable();
  private refreshProductsSubject = new Subject<void>(); 
  refreshProductsSubject$ = this.refreshProductsSubject.asObservable();

  constructor(private http: HttpClient) { }
  getProduct(productId: number): Observable<Product> {
    return this.http.get<Product>(`https://localhost:7048/Product/${productId}`);
  }

  getAllProducts(): Observable<Product[]> {
      return this.http.get<Product[]>('https://localhost:7048/Product');
  }

  addProduct(product: ProductDto): Observable<Product> {
    return this.http.post<Product>('https://localhost:7048/Product', product);
  }

  editProduct(productId: number, product: ProductDto): Observable<Product> {
    return this.http.put<Product>(`https://localhost:7048/Product/${productId}`, product);
  }

  removeProduct(productId: number): Observable<boolean> {
    return this.http.delete<boolean>(`https://localhost:7048/Product/${productId}`).pipe(
      filter((respone) => respone === true), 
      tap(() => this.refreshProductsSubject.next())
    );
  }

  selectProduct(product: Product): void {
    this.productSelectedSubject.next(product);
  }

}
