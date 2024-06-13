import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TodoItemService } from './todo-item.service';
import { ToDoItem } from './ToDoItem';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {

  todoItems$: Observable<ToDoItem[]> | undefined;

  constructor(private toDoItemService: TodoItemService) { }

  ngOnInit() {
    this.getToDoItems();
  }

  getToDoItems() {
    this.todoItems$ = this.toDoItemService.getToDoItems();
  }

  title = 'to-do.client';
}
