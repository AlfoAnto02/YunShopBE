import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { CategoriesService } from '../../../services/category.service';
import { addCategoryRequest } from '../../../models/category';
import { TokenService } from '../../../services/token.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-category',
  imports: [CommonModule, FormsModule],
  templateUrl: './new-category.component.html',
  styleUrl: './new-category.component.scss'
})
export class NewCategoryComponent {
  name: string = '';
  CurrentUserId: number = 0;
  addCategoryRequest: addCategoryRequest = {
    name: '',
    addedById: 0
  };

  
  constructor(private router:Router, private CategoriesService: CategoriesService, private TokenService: TokenService) {}
  
  ngOnInit(): void {
    this.getCurrentUserId();
  }

  getCurrentUserId() {
    const token = this.TokenService.getToken();
    if (token) {
      const decodedPayload = this.TokenService.decodeToken(token);
      console.log('Decoded token payload:', decodedPayload);
    }
  }

  close() {
    this.router.navigate(['/Categories']);
  }

  //createAddCategoryRequest(): addCategoryRequest {}

  onSubmit() {
    this.CategoriesService.addCategory(this.addCategoryRequest)
      .subscribe({
        next: response => {
          console.log('Category created successfully:', response);
          this.router.navigate(['/Categories']);
        },
        error: error => {
          console.error('Error creating category:', error);
          alert('Error creating category: ' + (error.error?.message || 'Unknown error'));
        }
      });
  }
}
