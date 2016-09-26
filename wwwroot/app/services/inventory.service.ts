import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

export class InventoryItem {
    constructor(public id: number, public name: string, public imageUrl: string) { }
}

@Injectable()
export class InventoryService
{
    constructor(private http: Http)
    {
    }

    getItems(appId: number)
    {
        return this.http.get("api/inventory/" + appId);
          //  .map(response => (<Response>response));
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