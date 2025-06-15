export class UserData {
    id: number;
    username: string;
    displayName: string;
    email: string;
    bio: string | null;

    constructor(
        id: number,
        username: string,
        displayName: string,
        email: string,
        bio: string | null
    ) {
        this.id = id;
        this.username = username;
        this.displayName = displayName;
        this.email = email;
        this.bio = bio;
    }
}