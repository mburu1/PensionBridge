import { Component, OnInit } from '@angular/core';
import { ApiService, Scheme } from '../../services/api';

@Component({
  selector: 'pensionbridge-schemes',
  templateUrl: './schemes.html',
  styleUrls: ['./schemes.scss']
})
export class SchemesComponent implements OnInit {
  schemes: Scheme[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.loadSchemes();
  }

  loadSchemes() {
    this.apiService.getSchemes().subscribe({
      next: (data) => this.schemes = data,
      error: (err) => console.error('Error loading schemes', err)
    });
  }
}
