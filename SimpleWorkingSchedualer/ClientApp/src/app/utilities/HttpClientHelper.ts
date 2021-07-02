import { HttpClient } from "@angular/common/http";
import { Inject } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import ApiResult from "../models/api-result";

export default class HttpClientHelper {

    private baseUrl: string;

    private httpClient: HttpClient;

    private router: Router;

    constructor(httpCLient: HttpClient, router: Router, @Inject('BASE_URL') baseUrl: string) {
        this.httpClient = httpCLient;
        this.router = router;
        this.baseUrl = baseUrl;
    }

    public post<T>(url: string, body: any, callBack: (result: T) => void): void {
        this.httpClient.post<ApiResult<T>>(this.baseUrl + url, body).subscribe(result => {
            if (!result.success && result.error === 'invalid token')
                this.router.navigate(['login']);
            else
                callBack(result.data);
        }, error => console.error(error));
    }
}