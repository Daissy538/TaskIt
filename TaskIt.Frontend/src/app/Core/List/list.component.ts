import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'list',
  templateUrl: './list.component.html'
})
export class ListComponent implements OnInit  {
  private _items: any[];

  public set items(items: Object[]){
    this._items = items;
  }

  public get items(): any[]{
    return this._items;
  }

 /**
  *
  */
 constructor() {
    this._items = [];
 }

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
