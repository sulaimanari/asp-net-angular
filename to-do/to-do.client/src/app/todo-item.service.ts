import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ToDoItem } from './ToDoItem';





@Injectable({
  providedIn: 'root'
})
export class TodoItemService {

  private apiToDoItemsUrl = 'api/ToDoItems';

  public todoItems: ToDoItem[] = []; 
  constructor(private http: HttpClient) {

  }

  getToDoItems(): Observable<ToDoItem[]> {
    return this.http.get<ToDoItem[]>(this.apiToDoItemsUrl)
      .pipe(
        tap(items => console.log(`fetched ${items.length} items`)),
        catchError(async (err) => {
          console.log(err);
          return [];
        })
    );
    
  }


}
