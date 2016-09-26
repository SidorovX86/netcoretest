import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SellComponent } from './components/sell.component';
import { AboutComponent } from './components/about.component';
import { AccountSettingsComponent } from './components/accountsettings.component';

const appRoutes: Routes = [
    { path: 'Sell', component: SellComponent },
    { path: 'About', component: AboutComponent },
    { path: 'AccountSettings', component: AccountSettingsComponent },
];

export const appRoutingProviders: any[] = [

];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);