import { Component, OnInit, Inject, HostListener, ViewEncapsulation, Input } from '@angular/core';
import { RoleService } from 'src/app/services/roles/role.service';
import { Role } from 'src/app/models/Entities/Role';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ErrorService } from 'src/app/services/errors/error.service';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogResult } from 'src/app/models/Enums/DialogResult.enum';
import { Permission } from 'src/app/models/Entities/Permission';

@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.scss']
})
export class RoleDetailsComponent implements OnInit {

  @Input() role: Role;
  // @Input() my_modal_content;

  form: FormGroup;
  submitted = false;
  tittle: string;

  constructor(
    private roleService: RoleService,
    public fb: FormBuilder,
    private errorService: ErrorService,
    private notificationService: NotificationService,
    private activeModal: NgbActiveModal
  ) { }

  ngOnInit() {
    // console.log(this.role);
    this.validation();
    this.tittle = this.getTittle();
  }

  getRoleDescription() {
    // console.log('getRoleDescription -> ', this.role)
    return this.role.description;
  }

  getTittle(): string {
    // console.log('getTittle -> ',  (this.role));
    return (this.role) ? 'Editar Role' : 'Salvar Role'
  }

  @HostListener('window:keyup.esc') onKeyUp() {
    this.activeModal.close(DialogResult.Cancel);
  }

  validation() {
    this.form = new FormGroup({
      id: new FormControl(this.role && this.role.id ? this.role.id : null),
      name: new FormControl(this.role ? this.role.name : '', [Validators.required, Validators.maxLength(256)]),
      description: new FormControl(this.role ? this.role.description : '', Validators.required),
      active: new FormControl(this.role && this.role.active ? this.role.active : false),
    });

  }

  getErrorMessage() {
    return this.form.hasError('required') ? 'Campo obrigatório' : '';
  }

  onSubmit(): void {
    // this.roleService.add(this.role);
    this.roleService.save(<Role>this.form.value).subscribe(
      success => {
        this.notificationService.showSuccess("Role salva com sucesso");
        // this.location.back();
        this.activeModal.close(DialogResult.Submit);
      },
      error => {
        this.errorService.handleError("Erro ao salvar role", error)
      }
    );
  }

  onCancel(): void {
    this.submitted = false;
    this.form.reset();
    // console.log('onCancel');
    this.activeModal.close(DialogResult.Cancel);
  }

  hasError = (controlName: string, errorName: string = null) => {
    if (errorName)
      return this.form.controls[controlName].hasError(errorName);

    return this.form.get(controlName).errors;
  }

}
