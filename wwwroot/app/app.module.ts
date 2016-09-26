import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';

import { AppComponent }  from './app.component';
import { SellComponent } from './components/sell.component';
import { AboutComponent } from './components/about.component';

import { AccountService } from './services/account.service';

import { routing, appRoutingProviders  } from './app.routing';

@NgModule({
   imports: [
     BrowserModule,
     FormsModule,
     routing
  ],
   declarations: [
       AppComponent,
       SellComponent,
       AboutComponent
   ],
   providers: [
       appRoutingProviders, AccountService
   ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
