import { Component, Input, OnInit } from '@angular/core';
import { InventoryItem, InventoryService } from '../services/inventory.service'

@Component({
    selector: 'sell-component',
    templateUrl: './app/components/sell.component.html'
})

export class SellComponent implements OnInit {

    items: InventoryItem[];

    constructor(private inventoryService: InventoryService) { }

    ngOnInit() { this.getItems(); }

    getItems() {

        //this.inventoryService.getItems(76561197777777777, 570)
        //    .subscribe(items => this.items = items);
    }
}