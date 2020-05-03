export class Permission {
    constructor(
        public id: number,
        // public groupName: string,
        public name: string,
        public description: string,
        public isActive: boolean,
        public checked: boolean,
    ) { }
}
