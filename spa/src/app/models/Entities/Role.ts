import { Permission } from './Permission';

export class Role {
    constructor(
        public id: number,
        public name: string,
        public description: string,
        public active: boolean,
        public checked: boolean,
        // public permissionsGrouped: PermissionsGrouped
        public permissions: Permission[]
    ) { }

}
