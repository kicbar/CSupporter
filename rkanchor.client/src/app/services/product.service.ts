import { HttpClient } from "@angular/common/http";
import { Product } from "../models/product.model";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class ProductService {
    public products: Product[] = [];

    constructor(private http: HttpClient) { }
    
    getAllProducts(): Observable<Product[]> {
        return this.http.get<Product[]>('https://localhost:7048/Product');
    }
}
