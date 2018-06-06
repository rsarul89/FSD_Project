import { Injectable } from "@angular/core";
import { ConnectionBackend, RequestOptions, Request, RequestOptionsArgs, Response, Http, Headers } from "@angular/http";
import { Observable } from "rxjs/Rx";
import { environment } from '../../environments/environment';
import { SpinnerService } from '../components/spinner/index';


@Injectable()
export class InterceptedHttp extends Http {

    spinner: SpinnerService;
    private spinTimeout = 1;

    constructor(backend: ConnectionBackend, defaultOptions: RequestOptions, spinnerService: SpinnerService) {
        super(backend, defaultOptions);
        this.spinner = spinnerService;
    }

    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        return super.request(url, options);
    }

    get(url: string, options?: RequestOptionsArgs): Observable<Response> {
        this.showLoader();
        url = this.updateUrl(url);
        return super.get(url, this.getRequestOptionArgs(options))
            .catch(this.onCatch)
            .do((res: Response) => {
                this.onSuccess(res);
            }, (error: any) => {
                this.onError(error);
            })
            .finally(() => {
                this.onEnd();
            });
    }

    post(url: string, body: string, options?: RequestOptionsArgs): Observable<Response> {
        this.showLoader();
        url = this.updateUrl(url);
        return super.post(url, body, this.getRequestOptionArgs(options))
            .catch(this.onCatch)
            .do((res: Response) => {
                this.onSuccess(res);
            }, (error: any) => {
                this.onError(error);
            })
            .finally(() => {
                this.onEnd();
            });
    }

    put(url: string, body: string, options?: RequestOptionsArgs): Observable<Response> {
        this.showLoader();
        url = this.updateUrl(url);
        return super.put(url, body, this.getRequestOptionArgs(options))
            .catch(this.onCatch)
            .do((res: Response) => {
                this.onSuccess(res);
            }, (error: any) => {
                this.onError(error);
            })
            .finally(() => {
                this.onEnd();
            });
    }

    delete(url: string, options?: RequestOptionsArgs): Observable<Response> {
        this.showLoader();
        url = this.updateUrl(url);
        return super.delete(url, this.getRequestOptionArgs(options))
            .catch(this.onCatch)
            .do((res: Response) => {
                this.onSuccess(res);
            }, (error: any) => {
                this.onError(error);
            })
            .finally(() => {
                this.onEnd();
            });
    }

    private updateUrl(req: string) {
        return environment.origin + req;
    }

    private getRequestOptionArgs(options?: RequestOptionsArgs): RequestOptionsArgs {
        if (options == null) {
            options = new RequestOptions();
        }
        if (options.headers == null) {
            options.headers = new Headers();
        }
        let user = JSON.parse(localStorage.getItem('loggedUser'));
        if (user != null) {
            options.headers.append('Authorization', `Bearer ${user.Token}`)
        }
        options.headers.append('Accept', 'application/json');
        options.headers.append('Content-Type', 'application/json');

        return options;
    }
    private onCatch(error: any, caught: Observable<any>): Observable<any> {
        return Observable.throw(error);
    }

    private onSuccess(res: Response): void {
        //console.log('Request successful');
    }

    private onError(error: Response | any): void {
        console.log('Error, status code: ' + error.code);
    }

    private onEnd(): void {
        this.hideLoader();
    }

    private showLoader(): void {
        this.spinner.show();
    }

    private hideLoader(): void {
        const timeoutMs = this.spinTimeout * 1000;
        setTimeout(() => {
            this.spinner.hide();
        }, timeoutMs);
    }
}