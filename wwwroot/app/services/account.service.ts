import { Http, Response, Request } from '@angular/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable()
export class AccountService {

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
}