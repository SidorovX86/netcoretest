export class User {
    SteamId: number;
    UserName: string;
    AvatarUrl: string;

    constructor(steamId: number, userName: string, avatarUrl: string) {
        this.SteamId = steamId;
        this.UserName = userName;
        this.AvatarUrl = avatarUrl;
    }
}