export class AccountSettings {
    Email: string;
    TradeUrl: string;

    constructor(email: string, tradeUrl: string) {
        this.Email = email;
        this.TradeUrl = tradeUrl;
    }
}