import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Category } from '../../../models/category';

@Component({
  selector: 'app-categories',
  imports: [CommonModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss'
})
export class CategoryComponent {
  @Input() category!: Category;

  constructor() {}

  deleteCategory(): void {
    alert('Not implemented yet');
  }
}
