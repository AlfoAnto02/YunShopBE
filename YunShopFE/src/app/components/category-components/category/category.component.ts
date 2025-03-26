import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Category, deleteCategoryRequest } from '../../../models/category';
import { UserService } from '../../../services/user.service';
import { TokenService } from '../../../services/token.service';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-categories',
  imports: [CommonModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss'
})
export class CategoryComponent {
  @Input() category!: Category;
  addedByUsername: string = '';
  deleteCategoryRequest: deleteCategoryRequest = {
    name: '',
    deletedBy: 0
  };

  constructor(private userService: UserService, private tokenService: TokenService, private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getAddedByUsername();
  }

  getAddedByUsername(): void {
    this.userService.getUserById(this.category.addedBy).subscribe({
      next: (response: any) => {
        this.addedByUsername = response.result.user.userName;
      },
      error: (error: any) => {
        console.error('Error loading user:', error);
      }
    });
  }

  createDeleteCategoryRequest(): void {
    this.deleteCategoryRequest = {
      name: this.category.name,
      deletedBy: this.tokenService.getUserIdByToken()
    };
  }

  deleteCategory(): void {
        this.createDeleteCategoryRequest();
        console.log('Deleting Category:', this.deleteCategory);
        this.categoryService.deleteCategory(this.deleteCategoryRequest).subscribe({
          next: (response: any) => {
            console.log('Category deleted:', response);
            
            // Remove the cateogry from localStorage
            const cachedCategories = localStorage.getItem('categories');
            if (cachedCategories) {
              let categories = JSON.parse(cachedCategories);
              categories = categories.filter((category: Category) => category.id !== this.category.id);
              localStorage.setItem('categories', JSON.stringify(categories));
            }
            
            window.location.reload();
          },
          error: (error: any) => {
            console.error('Error deleting Category:', error);
          }
        });
      }
}
