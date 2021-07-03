import { Component } from '@angular/core';
import { faLongArrowAltDown } from '@fortawesome/free-solid-svg-icons';
import DateHelper from 'src/app/utilities/DateHelper';
import ColumnModel from './models/ColumnModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  faLongArrowDown = faLongArrowAltDown;

  columns: ColumnModel[] = [];

  weekIndex: number;

  constructor() {
    this.fillColumns();
  }

  fillColumns(weekIndex: number = 0): void {
    this.weekIndex = weekIndex;
    this.columns = [];

    var date = DateHelper.getFirstDayOfWeek(weekIndex);
    do {
      this.columns.push(new ColumnModel(date));
      date.setDate(date.getDate() + 1);
    } while (this.columns.length < 7);
  }
}