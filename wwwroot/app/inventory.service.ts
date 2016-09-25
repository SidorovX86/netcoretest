import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable }     from 'rxjs/Observable';

export class InventoryItem {
    constructor(public id: number, public name: string, public imageUrl: string, public backpackUrl: string) { }
}

@Injectable()
export class InventoryService
{
    constructor(private http: Http) { }

    getItems(steamId: number, gameId: number): Observable<InventoryItem[]>
    {
        // 76561197777777777

        let url = "https://steamcommunity.com/id/" + steamId + "/inventory/json/" + gameId + "/2";
               
        return null;// this.http.get(url)
                      //  .map(this.extractData)
                        //.catch(this.handleError);
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