import { Component } from '@angular/core';
import { faLongArrowAltDown } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  faLongArrowDown = faLongArrowAltDown;
}
