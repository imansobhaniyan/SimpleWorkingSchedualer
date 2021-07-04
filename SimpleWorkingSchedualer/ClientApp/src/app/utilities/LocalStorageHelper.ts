export default class LocalStorageHelper {

    private static tokenKey: string = 'token';

    private static roleKey: string = 'role';

    public static setToken(token: string): void {
        localStorage.setItem(this.tokenKey, token);
    }

    public static getToken(): string {
        return localStorage.getItem(this.tokenKey);
    }

    public static hasToken(): boolean {
        return this.getToken() ? true : false;
    }

    public static setRole(role: number): void {
        localStorage.setItem(this.roleKey, role.toString());
    }

    public static getRole(): number {
        return Number.parseInt(localStorage.getItem(this.roleKey));
    }
}