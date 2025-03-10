import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Category, deleteCategoryRequest } from '../../../models/category';
import { UserService } from '../../../services/user.service';
import { TokenService } from '../../../services/token.service';
import { CategoriesService } from '../../../services/category.service';

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
      userId: 0
    };

  constructor(private userService: UserService, private tokenService:TokenService, private categoryService: CategoriesService) {}

  ngOnInit(): void {
    this.getAddedByUsername();
  }

  getAddedByUsername(): void {
    this.userService.getUserById(this.category.addedById).subscribe({
      next: (response: any) => {
        console.log('response:', response);
        this.addedByUsername = response.result.user.userName;
        console.log('User loaded:', this.addedByUsername);
      },
      error: (error: any) => {
        console.error('Error loading user:', error);
      }
    });
  }

  createDeleteCategoryRequest(): void {
    this.deleteCategoryRequest = {
      name: this.category.name,
      userId: this.tokenService.getUserIdByToken()
    };
  }

  deleteCategory(): void {
    this.createDeleteCategoryRequest();
    console.log('Deleting category:', this.deleteCategoryRequest);
    this.categoryService.deleteCategory(this.deleteCategoryRequest).subscribe({
      next: (response: any) => {
        console.log('Category deleted:', response);
        window.location.reload();
      },
      error: (error: any) => {
        console.error('Error deleting category:', error);
      }
    });
  }
}
