import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { addProductRequest, deleteProductRequest, Product } from '../models/product';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private ProductsUrl = `${environment.baseURL}/Products`;
  private ProductByIdUrl = `${environment.baseURL}/Products/byId`;

  constructor(private httpClient : HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(this.ProductsUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  getProductById(id: number): Observable<Product> {
    return this.httpClient.get<Product>(`${this.ProductByIdUrl}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  addProduct(addProductRequest: addProductRequest): Observable<Product> {
    return this.httpClient.post<Product>(this.ProductsUrl, addProductRequest)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteProduct(deleteProductRequest: deleteProductRequest): Observable<Product> {
    return this.httpClient.delete<Product>(this.ProductsUrl, { body: deleteProductRequest })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error);
  }
}
