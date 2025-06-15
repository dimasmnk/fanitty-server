export class UserByUsernameResponse {
    id: string;
    username: string;
    displayName: string;
    bio: string | null;

    constructor(
        id: string,
        username: string,
        displayName: string,
        bio: string
    ) {
        this.id = id;
        this.username = username;
        this.displayName = displayName;
        this.bio = bio;
    }
}