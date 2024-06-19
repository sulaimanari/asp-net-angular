import { Component, OnInit } from '@angular/core';
import { TodoItemService } from './todo-item.service';
import { ToDoItem } from './ToDoItem';
import { Observable, Subject, firstValueFrom, tap } from 'rxjs';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  title = 'to-do.client';
  todoItems?: ToDoItem[];

  neuToDoItem: string = '';

  constructor(private toDoItemService: TodoItemService) { }

  ngOnInit() {
    this.getToDoItems();
  }

  async getToDoItems() {
    this.todoItems = await firstValueFrom(this.toDoItemService.getToDoItems());
  }

  async addNeuItem() {
    const newItem = await this.toDoItemService.addToDoItem(this.neuToDoItem);
    this.todoItems?.push(newItem);
  }
  
}
