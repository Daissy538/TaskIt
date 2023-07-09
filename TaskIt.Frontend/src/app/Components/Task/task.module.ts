
import {MatTableModule} from '@angular/material/table';
import { TasksComponent } from './List/tasks.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { TaskComponent } from './Item/task.component';

@NgModule({
  declarations: [
    TasksComponent,
    TaskComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    SharedModule
  ],
  exports: [
    TaskComponent,
    TasksComponent
  ]
})
export class TaskModule { }
