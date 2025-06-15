export class CheckUsernameAvailabilityRepsonse {
    isAvailable: boolean;

    constructor(isAvailable: boolean) {
        this.isAvailable = isAvailable;
    }
}