export class UpdateUserCommand {
    username: string | null;
    displayName: string | null;
    bio: string | null;

    constructor(
        username: string | null,
        displayName: string | null,
        bio: string | null
    ) {
        this.username = username;
        this.displayName = displayName;
        this.bio = bio;
    }
}