import { Component, OnInit } from '@angular/core';
import { ApiService, ContributionBatch } from '../../services/api';

@Component({
  selector: 'pensionbridge-dashboard',
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class DashboardComponent implements OnInit {
  batches: ContributionBatch[] = [];
  totalBatches = 0;
  pendingBatches = 0;
  approvedBatches = 0;
  postedBatches = 0;

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.loadBatches();
  }

  loadBatches() {
    this.apiService.getBatches().subscribe({
      next: (data) => {
        this.batches = data;
        this.totalBatches = data.length;
        this.pendingBatches = data.filter(b => b.status === 'Pending').length;
        this.approvedBatches = data.filter(b => b.status === 'Approved').length;
        this.postedBatches = data.filter(b => b.status === 'Posted').length;
      },
      error: (err) => console.error('Error loading batches', err)
    });
  }
}
