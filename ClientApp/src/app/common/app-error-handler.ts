import { AppError } from './app-error';
import { SentryErrorHandler } from './sentry-error-handler';
import { ErrorHandler, Injectable , Inject, Injector, NgZone} from '@angular/core';
import { NotificationService } from '../services/notification.service';


@Injectable({
    providedIn: 'root'
})



export class AppErrorHandler implements ErrorHandler{

    constructor(private injector: Injector , private ngZone:NgZone ,private sentry : SentryErrorHandler) {

    }
    

    // Helper property to resolve the service dependency.
    private get toastService(): NotificationService {
        return this.injector.get(NotificationService);
    }

    handleError(error: AppError): void {
        this.sentry.handleError(error.orginalError);

        console.log(error);
        this.ngZone.run(() =>{
            this.toastService.showError("An unexpected error occured","Error");
        })
        
    }

}