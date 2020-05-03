import { Permission } from './Permission';

export class PermissionsGrouped {
    constructor(
        public groupName: string,
        public permissions: Permission[],
    ) { }
}
