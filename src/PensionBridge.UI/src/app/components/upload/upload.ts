import { Component, OnInit } from '@angular/core';
import { ApiService, Scheme } from '../../services/api';

@Component({
  selector: 'pensionbridge-upload',
  templateUrl: './upload.html',
  styleUrls: ['./upload.scss']
})
export class UploadComponent implements OnInit {
  schemes: Scheme[] = [];
  selectedScheme = '';
  employerId = '';
  period = '';
  selectedFile: File | null = null;
  uploadStatus: { type: string; message: string } | null = null;

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

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  uploadFile() {
    if (!this.selectedFile || !this.selectedScheme || !this.employerId || !this.period) return;

    this.uploadStatus = { type: 'info', message: 'Uploading...' };
    
    this.apiService.uploadFile(this.selectedFile, this.employerId, this.selectedScheme, this.period).subscribe({
      next: () => {
        this.uploadStatus = { type: 'success', message: 'File uploaded successfully!' };
        this.resetForm();
      },
      error: (err) => {
        this.uploadStatus = { type: 'error', message: 'Upload failed. Please try again.' };
        console.error('Upload error', err);
      }
    });
  }

  resetForm() {
    this.selectedScheme = '';
    this.employerId = '';
    this.period = '';
    this.selectedFile = null;
  }
}
