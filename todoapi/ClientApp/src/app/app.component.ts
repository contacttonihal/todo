import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToDoAddEditComponent } from './todo-add-edit/todo-add-edit.component';
import { ToDoService } from './services/todo.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CoreService } from './core/core.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  displayedColumns: string[] = [
   // 'id',
    'title',
    'description',
    'action',
  ];
  dataSource!: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _dialog: MatDialog,
    private _todoService: ToDoService,
    private _coreService: CoreService
  ) {}

  ngOnInit(): void {
    this.getToDoList();
  }

  openAddEditEmpForm() {
    const dialogRef = this._dialog.open(ToDoAddEditComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getToDoList();
        }
      },
    });
  }

  getToDoList() {
    this._todoService.getToDoList().subscribe({
      next: (res) => {        
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: console.log,
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteToDo(id: number) {
    this._todoService.deleteToDo(id).subscribe({
      next: (res) => {
        this._coreService.openSnackBar('Employee deleted!', 'done');
        this.getToDoList();
      },
      error: console.log,
    });
  }

  openEditForm(data: any) {
    debugger;
    const dialogRef = this._dialog.open(ToDoAddEditComponent, {
      data,
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getToDoList();
        }
      },
    });
  }
}
