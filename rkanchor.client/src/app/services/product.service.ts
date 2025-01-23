import { HttpClient } from "@angular/common/http";
import { Product } from "../models/product.model";
import { filter, Observable, Subject, tap } from "rxjs";
import { EventEmitter, Injectable } from "@angular/core";
import { ProductDto } from "../models/product.dto";

@Injectable()
export class ProductService {
  public products: Product[] = [];
  productSelected = new EventEmitter<Product>();
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

  removeProduct(productId: number): Observable<boolean> {
    return this.http.delete<boolean>(`https://localhost:7048/Product/${productId}`).pipe(
      filter((respone) => respone === true), 
      tap(() => this.refreshProductsSubject.next())
    );
  }

  selectProduct(product: Product): void {
    this.productSelected.emit(product);
  }

}
