import { HttpClient } from "@angular/common/http";
import { Product } from "../models/product.model";
import { Observable } from "rxjs";
import { EventEmitter, Injectable } from "@angular/core";
import { ProductDto } from "../models/product.dto";

@Injectable()
export class ProductService {
  public products: Product[] = [];
  productSelected = new EventEmitter<Product>();

    constructor(private http: HttpClient) { }
    
    getAllProducts(): Observable<Product[]> {
        return this.http.get<Product[]>('https://localhost:7048/Product');
    }

    addProduct(product: ProductDto): Observable<Product> {
      return this.http.post<Product>('https://localhost:7048/Product', product);
  }
}
