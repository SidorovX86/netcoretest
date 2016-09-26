import { Component, Input, OnInit } from '@angular/core';
import { InventoryItem, InventoryService } from '../services/inventory.service'
import { AccountService } from '../services/account.service';
import { DxButtonModule } from 'devextreme-angular2'

@Component({
    selector: 'sell-component',
    templateUrl: './app/components/sell.component.html'
})

export class SellComponent implements OnInit {

    items: InventoryItem[];

    appId: number;

    constructor(private accountService: AccountService, private inventoryService: InventoryService) { }

    ngOnInit() {

        this.appId = 570;

        this.refresh();
    }

    refresh() {

        var user = this.accountService.getLoggedInUser();

        if (user != null) {

            let self = this;
            self.inventoryService.getItems(user.SteamId, self.appId)
                .subscribe(res => {

                    var data: any = res.json();

                    self.items = data.map((i: any) => new InventoryItem(
                        i.id,
                        i.name,
                        'https://steamcommunity-a.akamaihd.net/economy/image/' + i.imageUrl,
                        `https://steamcommunity.com/profiles/${user.SteamId}/inventory/#${self.appId}_2_${i.id}`)
                    );
                }
            );
        }
    }
}