import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ListComponent } from './list.component';

describe('ListComponent', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [RouterTestingModule],
    declarations: [ListComponent]
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(ListComponent);
    const list = fixture.componentInstance;
    expect(list).toBeTruthy();
  });

  it('should have a empty list by default', () => {
    const fixture = TestBed.createComponent(ListComponent);
    const list = fixture.componentInstance;
    expect(list.items).toEqual([]);
  });

});
