import { Component, Input, OnInit } from '@angular/core';
import { AccountSettings } from '../models/accountsettings';
import { AccountService } from '../services/account.service';

@Component({
    selector: 'accountsettings-component',
    templateUrl: './app/components/accountsettings.component.html'
})
export class AccountSettingsComponent implements OnInit {

    model: AccountSettings;

    constructor(private accountService: AccountService) {
    }

    ngOnInit() {

        this.model = new AccountSettings('', '');

        this.loadSettings();
    }

    onSubmit() {
        this.saveSettings();
    }

    loadSettings() {
        let self = this;

        self.accountService.getSettings()
            .subscribe(res => {

                var data: any = res.json();

                self.model.Email = data.email;
                self.model.TradeUrl = data.tradeUrl;
            });
    }

    saveSettings() {
        this.accountService.setSettings(this.model)
    }
}