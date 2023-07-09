
import { NgModule } from '@angular/core';
import { DashboardComponent } from './Dashboard/dashboard-page.component';
import { TaskModule } from '../Components/Task/task.module';
import { PagesRoutingModule } from './pages-routing.module';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    DashboardComponent,
  ],
  imports: [
    CommonModule,
    TaskModule,
    PagesRoutingModule
  ]
})
export class PageModule { }
