import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { addProductRequest, Product } from '../models/product';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private GetProductsUrl = `${environment.baseURL}/Product/GetProducts`;
  private GetProductByIdUrl = `${environment.baseURL}/Product/GetProductById`;
  private AddProductUrl = `${environment.baseURL}/Product/AddProduct`;
  private DeleteProductUrl = `${environment.baseURL}/Product/DeleteProduct`;

  constructor(private httpClient : HttpClient) { }

  getProducts(): Observable<Product[]> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.get<Product[]>(this.GetProductsUrl, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  getProductById(id: number): Observable<Product> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.get<Product>(`${this.GetProductByIdUrl}/${id}`, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  addProduct(addProductRequest: addProductRequest): Observable<Product> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.post<Product>(this.AddProductUrl, addProductRequest, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteProduct(id: number): Observable<Product> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.delete<Product>(`${this.DeleteProductUrl}/${id}`, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error);
  }
}
