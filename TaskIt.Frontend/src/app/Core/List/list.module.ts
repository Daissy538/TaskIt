import { NgModule } from '@angular/core';
import { ListComponent } from './list.component';

@NgModule({
  declarations: [
    ListComponent
  ],
  exports: [
    ListComponent
  ]
  ,
  imports: [
  ],
  providers: [],
  bootstrap: [ListModule]
})
export class ListModule { }
