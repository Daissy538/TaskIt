import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { TaskComponent } from "./Item/task.component";
import { TasksComponent } from "./List/tasks.component";

const routes: Routes = [
  {path: '', component: TasksComponent},
  {path: ':id', component: TaskComponent}
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaskRoutingModule { }
