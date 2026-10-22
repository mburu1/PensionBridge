import { Component, OnInit } from '@angular/core';
import { ApiService, ContributionBatch } from '../../services/api';

@Component({
  selector: 'pensionbridge-batches',
  templateUrl: './batches.html',
  styleUrls: ['./batches.scss']
})
export class BatchesComponent implements OnInit {
  batches: ContributionBatch[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.loadBatches();
  }

  loadBatches() {
    this.apiService.getBatches().subscribe({
      next: (data) => this.batches = data,
      error: (err) => console.error('Error loading batches', err)
    });
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }
}
