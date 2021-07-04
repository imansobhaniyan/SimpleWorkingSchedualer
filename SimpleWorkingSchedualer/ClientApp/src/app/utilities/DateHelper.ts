export default class DateHelper {

    private static days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

    private static formatter = Intl.DateTimeFormat('en', { year: 'numeric', month: 'short', day: '2-digit' });

    public static getFirstDayOfWeek(weekIndex: number = 0): Date {
        var date = new Date();

        date.setDate(date.getDate() - date.getDay() + (weekIndex * 7));
        date.setHours(0, 0, 0, 0);

        return date;
    }

    public static addDays(date: Date, days: number = 1) {
        return new Date(date.getFullYear(), date.getMonth(), date.getDate() + days, 0, 0, 0, 0);
    }

    public static formatDate(date: Date): string {
        return this.formatter.format(date);
    }

    public static getDayName(date: Date): string {
        return this.days[date.getDay()];
    }

    public static getWeekNumber(date: Date): number {
        var start = new Date(date.getFullYear(), 0, 0);
        var diff = (date.getTime() - start.getTime()) + ((start.getTimezoneOffset() - date.getTimezoneOffset()) * 60 * 1000);
        var oneWeek = 1000 * 60 * 60 * 24 * 7;
        return Math.floor(diff / oneWeek);
    }
}