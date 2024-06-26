import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, firstValueFrom } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ToDoItem } from './ToDoItem';





@Injectable({
  providedIn: 'root'
})
export class TodoItemService {

  private apiToDoItemsUrl = 'api/ToDoItems';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

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

  async addToDoItem(addNeuItem: string) {
    return await firstValueFrom(this.http.post<ToDoItem>(this.apiToDoItemsUrl, `"${addNeuItem}"`, this.httpOptions));
  }

  async updateToDoItem(updateItem: ToDoItem) {
    return await firstValueFrom(this.http.put<ToDoItem>(this.apiToDoItemsUrl, updateItem, this.httpOptions));
  } 


  async deleteToDoItem(deleteId: string): Promise<void> {
    const url = `${this.apiToDoItemsUrl}/${deleteId}`;
    await firstValueFrom(this.http.delete<ToDoItem>(url));
  }

 
}
