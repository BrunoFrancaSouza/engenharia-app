import { Component, OnInit, ViewChild, OnChanges } from '@angular/core';

import { Role } from 'src/app/models/Entities/Role';
import { RoleService } from 'src/app/services/roles/role.service';
import { ErrorService } from 'src/app/services/errors/error.service';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { RoleDetailsComponent } from './role-details/role-details.component';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { DialogResult } from 'src/app/models/Enums/DialogResult.enum';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  modalOptions: NgbModalOptions;

  totalRecords: number;
  currentPage: number = 1;
  itemsPerPage: number = 5;

  constructor(
    private roleService: RoleService,
    private errorService: ErrorService,
    // public dialog: MatDialog,
    private modalService: NgbModal,
    private notificationService: NotificationService
  ) {
    this.modalOptions = { // -> https://ng-bootstrap.github.io/#/components/modal/api
      centered: true,
      backdrop: 'static',
      backdropClass: 'customBackdrop',
      // scrollable: true
    }
  }

  checkedAll: boolean = false;

  roles: Role[] = [];
  filteredRoles: Role[] = [];

  filterBy: string;
  filterValue: string;

  displayedColumns = ['Id', 'Nome', 'Descrição', 'Ativo'];

  loading: boolean = false;

  ngOnInit() {
    this.checkedAll = false;
    this.getRoles();
  }

  getRoles() {
    this.loading = true;

    this.roleService.getAll().subscribe(
      (response: Role[]) => {
        this.roles = response;
        this.filteredRoles = response;
        this.totalRecords = this.filteredRoles.length;
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

  filterRoles(value: string) {
    this.filteredRoles = this.applyFilter(this.filterBy, value);
  }

  checkUncheckAll(): void {

    for (var role in this.filteredRoles) {
      this.filteredRoles[role].checked = this.checkedAll
    }

  }

  onChangeCheckbox(): void {
    var checkboxHeader = <HTMLInputElement>document.getElementById("checkbox-header");

    if (this.filteredRoles.some(r => r.checked == true) == true && this.filteredRoles.some(r => r.checked == false) == true) {
      checkboxHeader.indeterminate = true;
    }
    else if (!this.filteredRoles.some(r => r.checked == true)) {
      checkboxHeader.checked = false;
      checkboxHeader.indeterminate = false;
    }
    else {
      checkboxHeader.checked = true;
      checkboxHeader.indeterminate = false;
    }
  }

  getCheckedRoles(): Role[] {
    var result = this.filteredRoles.filter(r => r.checked == true)
    return result;
  }

  getTextSelectedItems(): string {

    var count = this.getCheckedRoles().length;

    if (count == 0)
      return null;

    var result = `${count} ${count == 1 ? 'item' : 'itens'} ${count == 1 ? 'selecionado' : 'selecionados'} `;

    return result
  }

  applyFilter(filterBy: string, value: string): Role[] {

    if (!value)
      return this.roles;

    value = value.toLocaleLowerCase();

    if (filterBy) {
      var result = this.roles.filter(r => r.hasOwnProperty(filterBy) &&
        r[filterBy].toString().toLocaleLowerCase().indexOf(value) !== -1);

      return result;
    }
    else {
      var result = this.roles.filter(r => r.name.toLocaleLowerCase().indexOf(value) !== -1 ||
        r.description.toLocaleLowerCase().indexOf(value) !== -1
      );

      return result
    }
  }

  addRole() {
    const modalRef = this.modalService.open(RoleDetailsComponent, this.modalOptions);

    modalRef.componentInstance.my_modal_title = 'I your title';
    modalRef.componentInstance.my_modal_content = 'I am your content';

    modalRef.result.then(
      (result) => {
        if (result === DialogResult.Submit)
          this.getRoles();
      },
      (reason) => {
        console.log('reason', reason)
      });
  }

  editRole(role: Role) {
    // const dialogRef = this.dialog.open(EditRoleComponent, {
    //   data: { role }
    // });

    // dialogRef.afterClosed().subscribe(result => {
    //   if (result === 1)
    //     this.getRoles();
    // });
  }

  deleteRole(role: Role) {
    // const dialogRef = this.dialog.open(DeleteRoleComponent, {
    //   data: { role }
    // });

    // dialogRef.afterClosed().subscribe(result => {
    //   if (result === 1)
    //     this.getRoles();
    // });
  }

  deleteSelectedRoles(): void {
    var selectedRoles = this.filteredRoles.filter(r => r.checked == true);

    if (!selectedRoles)
      return;

    var roleIds = selectedRoles.map(role => role.id);

    this.roleService.deleteMany(roleIds).subscribe(
      response => {
        this.notificationService.showSuccess("Roles deletadas com sucesso")
        this.getRoles();
      },
      error => {
        this.errorService.handleError("Erro ao deletar roles", error)
      }
    );

  }

  isLoading(): boolean {
    return this.loading;
  }

  hasData(): boolean {
    return this.filteredRoles.length > 0 ? true : false;
  }

  onRowClick(role: Role) {
    const modalRef = this.modalService.open(RoleDetailsComponent, this.modalOptions);

    modalRef.componentInstance.role = role;
    // modalRef.componentInstance.my_modal_content = 'I am your content';

    modalRef.result.then(
      (result) => {
        if (result === 1) // Set when closed
          this.getRoles();
      },
      (reason) => {
        console.log('reason', reason)
      });
  }

  sortTable(n) {

    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("table-roles");
    switching = true;
    // Set the sorting direction to ascending:
    dir = "asc";
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
      // Start by saying: no switching is done:
      switching = false;
      rows = table.rows;
      /* Loop through all table rows (except the
      first, which contains table headers): */
      for (i = 1; i < (rows.length - 1); i++) {
        // Start by saying there should be no switching:
        shouldSwitch = false;
        /* Get the two elements you want to compare,
        one from current row and one from the next: */
        x = rows[i].getElementsByTagName("TD")[n];
        y = rows[i + 1].getElementsByTagName("TD")[n];
        /* Check if the two rows should switch place,
        based on the direction, asc or desc: */
        if (dir == "asc") {
          if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
            // If so, mark as a switch and break the loop:
            shouldSwitch = true;
            break;
          }
        } else if (dir == "desc") {
          if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
            // If so, mark as a switch and break the loop:
            shouldSwitch = true;
            break;
          }
        }
      }
      if (shouldSwitch) {
        /* If a switch has been marked, make the switch
        and mark that a switch has been done: */
        rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
        switching = true;
        // Each time a switch is done, increase this count by 1:
        switchcount++;
      } else {
        /* If no switching has been done AND the direction is "asc",
        set the direction to "desc" and run the while loop again. */
        if (switchcount == 0 && dir == "asc") {
          dir = "desc";
          switching = true;
        }
      }
    }
  }

}



