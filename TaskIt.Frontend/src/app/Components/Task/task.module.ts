import { NgModule } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import { TaskComponent } from './List/tasks.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    TaskComponent
  ],
  imports: [
    CommonModule,
    MatTableModule
  ],
  exports: [
    TaskComponent
  ],
  providers: [],
  bootstrap: [TaskModule]
})
export class TaskModule { }
