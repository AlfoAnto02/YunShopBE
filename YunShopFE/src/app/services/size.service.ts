import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { addSizeRequest, deleteSizeRequest, Size } from '../models/size';

@Injectable({
  providedIn: 'root'
})
export class SizeService {
  private SizesUrl = `${environment.baseURL}/Sizes`;

  constructor(private httpClient: HttpClient) { }

  getSizes(): Observable<Size[]> {
    return this.httpClient.get<Size[]>(this.SizesUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  getSizeById(id: number): Observable<Size> {
    return this.httpClient.get<Size>(`${this.SizesUrl}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  addSize(addSizeRequest: addSizeRequest): Observable<Size> {
    return this.httpClient.post<Size>(this.SizesUrl, addSizeRequest)
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteSize(deleteSizeRequest: deleteSizeRequest): Observable<Size> {
    return this.httpClient.delete<Size>(`${this.SizesUrl}/${deleteSizeRequest.sizeId}`, { body: deleteSizeRequest })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error);
  }
}
