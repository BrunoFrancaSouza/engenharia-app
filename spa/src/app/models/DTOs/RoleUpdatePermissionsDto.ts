import { Role } from '../Entities/Role';
import { UpdateTypes } from '../Enums/UpdateTypes.enum';

export class RoleUpdatePermissionsDto {
    constructor(
        public role: Role,
        // public permissions: Permission[]
        public permissionIds: number[],
        public updateType: UpdateTypes
    ) { }
}
