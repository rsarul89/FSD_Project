import { NgModule } from '@angular/core';
import { CommonModule, APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule, Http, XHRBackend, RequestOptions } from '@angular/http';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent, routing } from './index';
import { NavComponent } from './components/nav/index';
import { FooterComponent } from './components/footer/index';
import { AlertComponent } from './directives/index';
import { AuthGuard } from './guards/index';
import { AlertService, AuthenticationService, UserService, WorkoutService, CategoryService, ReportService } from './services/index';
import { WorkoutComponent, WorkoutListComponent, AddWorkoutComponent, WorkoutEditComponent, WorkoutStartComponent, WorkoutEndComponent } from './components/workout/index';
import { ReportComponent } from './components/report/index';
import { CategoryComponent, CategoryListComponent, CategoryEditComponent } from './components/category/index';
import { LoginComponent } from './components/login/index';
import { RegisterComponent } from './components/register/index';
import { AppSettings } from './shared/index';
import { SpinnerComponent, SpinnerService } from './components/spinner/index';
import { SearchFilterPipe } from './pipes/index';
import { httpFactory } from './helpers/index';


@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        routing,
        CommonModule
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        NavComponent,
        FooterComponent,
        LoginComponent,
        RegisterComponent,
        WorkoutComponent,
        WorkoutListComponent,
        AddWorkoutComponent,
        WorkoutEditComponent,
        WorkoutStartComponent,
        WorkoutEndComponent,
        CategoryComponent,
        CategoryListComponent,
        CategoryEditComponent,
        ReportComponent,
        SpinnerComponent,
        SearchFilterPipe
    ],
    providers: [
        AlertService,
        AuthGuard,
        AuthenticationService,
        UserService,
        WorkoutService,
        CategoryService,
        ReportService,
        AppSettings,
        SpinnerService,
        { provide: APP_BASE_HREF, useValue: '/' },
        { provide: Http, useFactory: httpFactory, deps: [XHRBackend, RequestOptions, SpinnerService] }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }