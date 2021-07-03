export default class DateHelper {

    private static days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

    private static formatter = Intl.DateTimeFormat('en', { year: 'numeric', month: 'short', day: '2-digit' });

    public static getFirstDayOfWeek(weekIndex: number = 0): Date {
        var date = new Date();

        date.setDate(date.getDate() - date.getDay() + (weekIndex * 7));

        return date;
    }

    public static formatDate(date: Date): string {
        return this.formatter.format(date);
    }

    public static getDayName(date: Date): string {
        return this.days[date.getDay()];
    }
}