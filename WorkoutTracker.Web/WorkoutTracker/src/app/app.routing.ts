import { Routes, RouterModule } from '@angular/router';

import { WorkoutComponent, AddWorkoutComponent, WorkoutEditComponent, WorkoutStartComponent, WorkoutEndComponent } from './components/workout/index';
import { CategoryComponent, CategoryListComponent, CategoryEditComponent } from './components/category/index';
import { ReportComponent } from './components/report/index';

import { LoginComponent } from './components/login/index';
import { RegisterComponent } from './components/register/index';

import { AuthGuard } from './guards/index';

const appRoutes: Routes = [
    { path: '', component: WorkoutComponent, canActivate: [AuthGuard] },
    { path: 'category', component: CategoryComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    {
        path: 'workout/add',
        component: AddWorkoutComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'workout/edit/:workout-id',
        component: WorkoutEditComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'workout/start/:workout-id',
        component: WorkoutStartComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'workout/end/:workout-id',
        component: WorkoutEndComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'category/edit/:category-id',
        component: CategoryEditComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'report',
        component: ReportComponent,
        canActivate: [AuthGuard]
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
