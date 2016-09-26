import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

export class InventoryItem {

    Id: number;
    Name: string;
    ImageUrl: string;
    BackpackUrl: string;

    constructor(id: number, name: string, imageUrl: string, backpackUrl: string) {
        this.Id = id;
        this.Name = name;
        this.ImageUrl = imageUrl;
        this.BackpackUrl = backpackUrl;
    }
}

@Injectable()
export class InventoryService
{
    constructor(private http: Http)
    {
    }

    getItems(steamId: number, appId: number)
    {
        return this.http.get("api/inventory/" + appId);
            //.map(response => (<Response>response));
    }

    private extractData(res: Response)
    {
        let body = res.json();
        
        return body.data || {};
    }

    private handleError(error: any)
    {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';

        console.error(errMsg); // log to console instead

        return Observable.throw(errMsg);
    }
}