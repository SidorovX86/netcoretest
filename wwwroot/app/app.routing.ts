import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SellComponent }   from './sell.component';
import { AboutComponent }   from './about.component';

const appRoutes: Routes = [
    { path: 'Sell', component: SellComponent },
    { path: 'About', component: AboutComponent },
];

export const appRoutingProviders: any[] = [

];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);