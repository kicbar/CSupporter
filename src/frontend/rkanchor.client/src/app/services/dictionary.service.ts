import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { environment } from '../../envrinments/environments';
import { DictionaryType } from '../enums/dictionary-type.enum';
import { Observable } from 'rxjs';
import { ApiResult } from '../models/api.result';

@Injectable({
  providedIn: 'root'
})
export class DictionaryService {
  baseUrl: string = environment.apiBaseUrl;

  constructor(private httpClient: HttpClient, private authService: AuthService) { }

  getDictionary(dictionaryType: DictionaryType) : Observable<ApiResult<string[]>> {
    var token = this.authService.getToken();
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.httpClient.get<ApiResult<string[]>>(`${this.baseUrl}/Dictionary/${dictionaryType}`, { headers });
  }  
}
