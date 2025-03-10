import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Category } from '../../../models/category';
import { CategoriesService } from '../../../services/category.service';
import { CategoryComponent } from '../category/category.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-categories-list',
  imports: [CommonModule, CategoryComponent, RouterLink],
  templateUrl: './categories-list.component.html',
  styleUrl: './categories-list.component.scss'
})
export class CategoriesListComponent {
  categories: Category[] = [];

  constructor(private CategoriesService: CategoriesService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.CategoriesService.getCategories().subscribe({
      next: (response: any) => {
        if (response && Array.isArray(response.result)) {
          this.categories = response.result;
          console.log('Categories loaded:', this.categories);
        } else {
          console.error('Expected an array of categories, but got:', response);
          this.categories = [];
        }
      },
      error: (error: any) => {
        console.error('Error loading categories:', error);
      }
    });
  }
}
