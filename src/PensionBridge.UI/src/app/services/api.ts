import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Scheme {
  id: string;
  code: string;
  name: string;
  businessCentralCompany: string;
  currency: string;
  status: string;
}

export interface ContributionBatch {
  id: string;
  employerId: string;
  schemeId: string;
  period: string;
  fileName: string;
  status: string;
  totalRows: number;
  acceptedRows: number;
  rejectedRows: number;
  totalAmount: number;
  createdAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) {}

  getSchemes(): Observable<Scheme[]> {
    return this.http.get<Scheme[]>(`${this.apiUrl}/schemes`);
  }

  getBatches(): Observable<ContributionBatch[]> {
    return this.http.get<ContributionBatch[]>(`${this.apiUrl}/contributionbatches`);
  }

  uploadFile(file: File, employerId: string, schemeId: string, period: string): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('employerId', employerId);
    formData.append('schemeId', schemeId);
    formData.append('period', period);
    return this.http.post(`${this.apiUrl}/contributionbatches`, formData);
  }
}
