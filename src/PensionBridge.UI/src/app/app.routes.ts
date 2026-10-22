import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard';
import { BatchesComponent } from './components/batches/batches';
import { SchemesComponent } from './components/schemes/schemes';
import { UploadComponent } from './components/upload/upload';

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'batches', component: BatchesComponent },
  { path: 'schemes', component: SchemesComponent },
  { path: 'upload', component: UploadComponent },
];
