export default interface ApiResult<T> {
    success: boolean;
    error: string;
    data: T;
}