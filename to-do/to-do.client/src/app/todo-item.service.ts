import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { catchError, map , tap } from 'rxjs/operators';
import { ToDoItem } from './ToDoItem';





@Injectable({
  providedIn: 'root'
})
export class TodoItemService {

  private apiGetToDoITemUrl = 'api/ToDoItems';

  public todoItems: ToDoItem[] = []; 
  constructor(private http: HttpClient) {

  }

  getToDoItems(): Observable<ToDoItem[]> {
    return this.http.get<ToDoItem[]>(this.apiGetToDoITemUrl)
      .pipe(
        tap(items => console.log(`fetched ${items.length} items`)),
        catchError(async (err) => {
          console.log(err);
          return [];
        })
    );
    
  }


}
