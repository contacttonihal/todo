import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CoreService } from '../core/core.service';
import { ToDoService } from '../services/todo.service';

@Component({
  selector: 'app-emp-add-edit',
  templateUrl: './todo-add-edit.component.html',
  styleUrls: ['./todo-add-edit.component.scss'],
})
export class ToDoAddEditComponent implements OnInit {
  todoForm: FormGroup;

  constructor(
    private _fb: FormBuilder,
    private _todoService: ToDoService,
    private _dialogRef: MatDialogRef<ToDoAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _coreService: CoreService
  ) {
    this.todoForm = this._fb.group({
      title: '',
      description: '',
      isactive: false
    });
  }

  ngOnInit(): void {
    this.todoForm.patchValue(this.data);
  }

  onFormSubmit() {
    if (this.todoForm.valid) {
      debugger;
      if (this.data) {
        this._todoService
          .updateToDo(this.data.id, this.todoForm.value)
          .subscribe({
            next: (val: any) => {
              this._coreService.openSnackBar('ToDo detail updated!');
              this._dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
            },
          });
      } else {
        this._todoService.addToDo(this.todoForm.value).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('ToDo added successfully');
            this._dialogRef.close(true);
          },
          error: (err: any) => {
            console.error(err);
          },
        });
      }
    }
  }
}
