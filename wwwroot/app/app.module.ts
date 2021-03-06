import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule }   from '@angular/forms';

import { DevExtremeModule } from 'devextreme-angular2';

import { AppComponent }  from './app.component';
import { SellComponent } from './components/sell.component';
import { AboutComponent } from './components/about.component';
import { AccountSettingsComponent } from './components/accountsettings.component';

import { AccountService } from './services/account.service';
import { InventoryService } from './services/inventory.service';

import { routing, appRoutingProviders  } from './app.routing';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        DevExtremeModule,
        routing
    ],
    declarations: [
        AppComponent,
        SellComponent,
        AboutComponent,
        AccountSettingsComponent
    ],
    providers: [
        appRoutingProviders, AccountService, InventoryService
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }
