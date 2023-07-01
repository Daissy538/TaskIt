import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'TaskIt';

  constructor(translate: TranslateService, route: Router) {
    translate.setDefaultLang('en');
    translate.use('en');

  }
}
