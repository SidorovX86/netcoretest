import { Http, Headers, Response, Request } from '@angular/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { AccountSettings } from '../models/accountsettings';

@Injectable()
export class AccountService {

    constructor(private http: Http) {
    }

    isUserAuthenticated(): boolean
    {
        return (localStorage.getItem('user') != null);
    }

    getLoggedInUser(): User
    {
        if (this.isUserAuthenticated())
        {
            var userData = JSON.parse(localStorage.getItem('user'));

            return new User(userData.steamid, userData.personaname, userData.avatar);
        }

        return null;
    }

    getSettings() {
        return this.http.get('api/account/settings');
    }

    setSettings(settings: AccountSettings) {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        this.http.put('api/account/settings', JSON.stringify(settings), { headers: headers }).subscribe((r: any) => {
        });
    }
}