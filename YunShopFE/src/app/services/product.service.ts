import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { addProductRequest, deleteProductRequest, Product } from '../models/product';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private GetProductsUrl = `${environment.baseURL}/Product/GetAll`;
  private GetProductByIdUrl = `${environment.baseURL}/Product/GetById`;
  private AddProductUrl = `${environment.baseURL}/Product/Add`;
  private DeleteProductUrl = `${environment.baseURL}/Product/Delete`;

  constructor(private httpClient : HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(this.GetProductsUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  getProductById(id: number): Observable<Product> {
    return this.httpClient.get<Product>(`${this.GetProductByIdUrl}?id=${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  addProduct(addProductRequest: addProductRequest): Observable<Product> {
    return this.httpClient.post<Product>(this.AddProductUrl, addProductRequest)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteProduct(deleteProductRequest: deleteProductRequest): Observable<Product> {
    return this.httpClient.delete<Product>(this.DeleteProductUrl, { body: deleteProductRequest })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error);
  }
}
