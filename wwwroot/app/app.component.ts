import { Component } from '@angular/core';
import { AccountService } from './services/account.service';

@Component({
    selector: 'netcoretest-app',
    templateUrl: './app/app.component.html'
})
export class AppComponent {
    title = 'My Site2';
    name = 'unknown';

    constructor(public accountService: AccountService) {
    }

    isUserLoggedIn(): boolean
    {
        return this.accountService.isUserAuthenticated();
    }

    getUserName(): string
    {
        var user = this.accountService.getLoggedInUser();

        if (user != null)
            return user.UserName;
        else
            return '<Unknown>';
    }

    getUserAvatar(): string
    {
        var user = this.accountService.getLoggedInUser();

        if (user != null)
            return user.AvatarUrl;
        else
            return null;
    }
}
