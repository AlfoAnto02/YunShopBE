import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { addBrandRequest, deleteBrandRequest, Brand } from '../models/brand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  private BrandsUrl = `${environment.baseURL}/Brands`;

  constructor(private httpClient: HttpClient) { }

  getBrands(): Observable<Brand[]> {
    return this.httpClient.get<Brand[]>(this.BrandsUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  getBrandById(id: number): Observable<Brand> {
    return this.httpClient.get<Brand>(`${this.BrandsUrl}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  addBrand(addBrandRequest: addBrandRequest): Observable<Brand> {

    return this.httpClient.post<Brand>(this.BrandsUrl, addBrandRequest)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteBrand(deleteBrandRequest: deleteBrandRequest): Observable<Brand> {

    return this.httpClient.delete<Brand>(this.BrandsUrl, { body: deleteBrandRequest })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error);
  }
}
