import DateHelper from "src/app/utilities/DateHelper";

export default class ColumnModel {
    title: string;
    subTitle: string;
    value: Date;
    isToday: boolean;

    constructor(value: Date) {
        this.value = value;
        this.title = DateHelper.getDayName(value);
        this.subTitle = DateHelper.formatDate(value);
        this.isToday = value.getDate() == new Date().getDate();
    }
}