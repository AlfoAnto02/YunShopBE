import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { addBrandRequest, deleteBrandRequest, Brand } from '../models/brand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  GetBrandsUrl = `${environment.baseURL}/Brand/getAll`;
  GetBrandByIdUrl = `${environment.baseURL}/Brand/getById`;
  AddBrandUrl = `${environment.baseURL}/Brand/add`;
  DeleteBrandByIdUrl = `${environment.baseURL}/Brand/delete`;

  constructor(private httpClient: HttpClient) { }

  getBrands(): Observable<Brand[]> {
    return this.httpClient.get<Brand[]>(this.GetBrandsUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  getBrandById(id: number): Observable<Brand> {
    return this.httpClient.get<Brand>(`${this.GetBrandByIdUrl}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  addBrand(addBrandRequest: addBrandRequest): Observable<Brand> {

    return this.httpClient.post<Brand>(this.AddBrandUrl, addBrandRequest)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteBrand(deleteBrandRequest: deleteBrandRequest): Observable<Brand> {

    return this.httpClient.delete<Brand>(this.DeleteBrandByIdUrl, { body: deleteBrandRequest })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error);
  }
}
