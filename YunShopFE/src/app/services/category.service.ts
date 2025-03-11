import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { TokenService } from './token.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Category, deleteCategoryRequest } from '../models/category';
import { addCategoryRequest } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  GetCategoriesUrl = `${environment.baseURL}/Category/getAll`;
  GetCategoryByIdUrl = `${environment.baseURL}/Category/getById`;
  AddCategoryUrl = `${environment.baseURL}/Category/add`;
  DeleteCategoryByNameUrl = `${environment.baseURL}/Category/delete`;

  constructor(private httpClient: HttpClient, private tokenService: TokenService) { }

  getCategories(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(this.GetCategoriesUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  getCategoryById(id: number): Observable<Category> {
    return this.httpClient.get<Category>(`${this.GetCategoryByIdUrl}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  addCategory(addCategoryRequest: addCategoryRequest): Observable<Category> {

    return this.httpClient.post<Category>(this.AddCategoryUrl, addCategoryRequest)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteCategory(deleteCategoryRequest: deleteCategoryRequest): Observable<Category> {

    return this.httpClient.delete<Category>(this.DeleteCategoryByNameUrl, { body: deleteCategoryRequest })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error);
  }
}
