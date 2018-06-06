import { XHRBackend, Http, RequestOptions } from "@angular/http";
import { InterceptedHttp } from "./http.interceptor";
import { SpinnerService } from '../components/spinner/index';


export function httpFactory(xhrBackend: XHRBackend, requestOptions: RequestOptions, spinnerService: SpinnerService): Http {
    return new InterceptedHttp(xhrBackend, requestOptions, spinnerService);
}