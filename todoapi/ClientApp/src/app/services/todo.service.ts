import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ToDoService {
  constructor(private _http: HttpClient) {}

  addToDo(data: any): Observable<any> {debugger;
    return this._http.post('https://localhost:7177/api/todo', data);
  }

  updateToDo(id: number, data: any): Observable<any> {
    return this._http.put(`https://localhost:7177/api/todo/${id}`, data);
  }

  getToDoList(): Observable<any> {
    return this._http.get('https://localhost:7177/api/todo');
  }

  deleteToDo(id: number): Observable<any> {
    return this._http.delete(`https://localhost:7177/api/todo/${id}`);
  }
}
