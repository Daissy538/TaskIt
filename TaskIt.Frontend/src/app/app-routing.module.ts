import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from "./app.component";

const routes: Routes = [
  {path: '', redirectTo: 'task', pathMatch: 'full'},
  {path: 'task', loadChildren: () => import('src/app/Components/Task/task-routing.module').then(m => m.TaskRoutingModule)}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
