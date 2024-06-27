import { Component, OnInit } from '@angular/core';
import { TodoItemService } from './todo-item.service';
import { ToDoItem } from './ToDoItem';
import { firstValueFrom } from 'rxjs';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  title = 'to-do.client';
  todoItems?: ToDoItem[];
  editable!: boolean;
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
    await this.getToDoItems();
  }

  async updateItem(updatItem: ToDoItem) {
     await this.toDoItemService.updateToDoItem(updatItem);
  }

  async deleteItem(deleteItem: ToDoItem) {
    await this.toDoItemService.deleteToDoItem(deleteItem.id);
    await this.getToDoItems();
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.todoItems!, event.previousIndex, event.currentIndex);
    const ids = this.todoItems!.map(item => item.id);
    this.toDoItemService.updatePriority(ids).subscribe(updatedItems => {
      this.todoItems = updatedItems;
    });
  }
}
