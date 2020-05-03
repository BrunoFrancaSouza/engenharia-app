import { Component, OnInit } from '@angular/core';
import { Role } from 'src/app/models/Entities/Role';
import { RoleService } from 'src/app/services/roles/role.service';
import { ErrorService } from 'src/app/services/errors/error.service';
import { Permission } from 'src/app/models/Entities/Permission';
import { PermissionsGrouped } from 'src/app/models/Entities/Permissions';
import { PermissionService } from 'src/app/services/permissions/permission.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { RoleUpdatePermissionsDto } from 'src/app/models/DTOs/RoleUpdatePermissionsDto';
import { UpdateTypes } from 'src/app/models/Enums/UpdateTypes.enum';

@Component({
  selector: 'app-roles-permissions',
  templateUrl: './roles-permissions.component.html',
  styleUrls: ['./roles-permissions.component.css']
})
export class RolesPermissionsComponent implements OnInit {


  roles: Role[];
  selectedRole: Role;
  allPermissions: Permission[];
  availablePermissions: Permission[];
  definedPermissions: Permission[];

  loading: boolean;

  constructor(private roleService: RoleService,
    private permissionService: PermissionService,
    private errorService: ErrorService,
    private notificationService: NotificationService,
  ) { }


  ngOnInit() {
    this.getRoles();
    this.getAllPermissions()
  }

  getRoles() {
    this.loading = true;

    this.roles = [];
    this.selectedRole = null;

    this.roleService.getAll().subscribe(
      (response: Role[]) => {
        this.roles = response;
        this.loading = false;
      },
      error => {
        this.loading = false;
        this.errorService.handleError('Erro ao buscar roles.', error);
      }
    ).add(() => {
      //Called when operation is complete (both success and error)
      this.loading = false;
    })
  }

  onChangeSelect(): void {
    console.log(this.selectedRole);

    this.definedPermissions = this.selectedRole.permissions;
    // this.definedPermissions.map(p => p.checked = true);
    this.availablePermissions = this.getAvailablePermissions(this.definedPermissions);

  }

  getAllPermissions() {
    this.permissionService.getAll().subscribe(
      (response: Permission[]) => {
        this.allPermissions = response;
        // this.loading = false;
      },
      error => {
        // this.loading = false;
        this.errorService.handleError('Erro ao buscar permissões.', error);
      }
    ).add(() => {
      //Called when operation is complete (both success and error)
      // this.loading = false;
    })
  }

  getAvailablePermissions(definedPermissions: Permission[]): Permission[] {

    var result = [];
    for (var i = 0; i < this.allPermissions.length; i++) {
      if (definedPermissions.filter(p => p.name == this.allPermissions[i].name).length == 0) {
        result.push(this.allPermissions[i]);
      }
    }

    return result;
  }

  onChangeCheckbox(): void {
    // if this.availablePermissions.filter(p => p.checked == true).length !== 0)

  }

  getAvailablePermissionsSelected(): Permission[] {
    var result = this.availablePermissions.filter(p => p.checked == true)
    return result;
  }

  getDefinedPermissionsSelected(): Permission[] {
    var result = this.definedPermissions.filter(p => p.checked == true)
    return result;
  }

  hasAvailablePermissionSelected(): boolean {
    var result = this.getAvailablePermissionsSelected().length == 0 ? false : true;
    return result;
  }

  hasDefinedPermissionSelected(): boolean {
    var result = this.getDefinedPermissionsSelected().length == 0 ? false : true;
    return result;
  }

  addSelectedPermissions(): void {
    var selectedPermissions = this.getAvailablePermissionsSelected();

    if (selectedPermissions == null || this.selectedRole == null)
      return;

    this.updateRolePermissions(this.selectedRole, selectedPermissions, UpdateTypes.Create);
  }

  removeSelectedPermissions(): void {
    var selectedPermissions = this.getDefinedPermissionsSelected();

    if (selectedPermissions == null || this.selectedRole == null)
      return;

    this.updateRolePermissions(this.selectedRole, selectedPermissions, UpdateTypes.Delete);
  }

  updateRolePermissions(role: Role, permissions: Permission[], updateType: UpdateTypes): void {

    if (role == null || permissions == null || updateType == null)
      return;

    var ids = permissions.map(p => p.id);

    var roleUpdatePermissionsDto = new RoleUpdatePermissionsDto(this.selectedRole, ids, updateType);

    this.roleService.updatePermissions(roleUpdatePermissionsDto).subscribe(
      response => {
        var msg = `Permissões ${updateType == UpdateTypes.Create ? 'adicionadas' : 'removidas'} com sucesso.` 
        this.notificationService.showSuccess("Permissões desvinculadas com sucesso")
        this.getRoles();
      },
      error => {
        this.errorService.handleError("Erro ao desvincular permissões", error)
      }
    );
  }

}
