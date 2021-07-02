export default class LocalStorageHelper {

    private static tokenKey: string = 'token';

    public static setToken(token: string): void {
        localStorage.setItem(this.tokenKey, token);
    }

    public static getToken(): string {
        return localStorage.getItem(this.tokenKey);
    }

    public static hasToken(): boolean {
        return this.getToken() ? true : false;
    }
}