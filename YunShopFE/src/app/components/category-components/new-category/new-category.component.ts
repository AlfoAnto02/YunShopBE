import { Component, HostListener } from '@angular/core';
import { Route, Router } from '@angular/router';
import { CategoryService } from '../../../services/category.service';
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
  currentUserId: number = 0;
  addCategoryRequest: addCategoryRequest = {
    Name: '',
    AddedBy: 0
  };


  constructor(private router: Router, private CategoriesService: CategoryService, private TokenService: TokenService) { }

  ngOnInit(): void {
    this.getCurrentUserId();
  }

  getCurrentUserId() {
    const token = this.TokenService.getToken();
    if (token) {
      const decodedPayload = this.TokenService.decodeToken(token);
      this.currentUserId = decodedPayload.user_id;
    }
  }

  createAddCategoryRequest(): void {
    this.addCategoryRequest =
    {
      Name: this.name,
      AddedBy: this.currentUserId
    };
  }

  onSubmit() {
    this.createAddCategoryRequest()
    this.CategoriesService.addCategory(this.addCategoryRequest)
      .subscribe({
        next: response => {
          console.log('Category created successfully:', response);
          alert('Category created successfully');
          this.name = '';
          this.router.navigate(['/New-Category']);
        },
        error: error => {
          console.error('Error creating category:', error);
          alert('Error creating category: ' + (error.error?.message || 'Unknown error'));
          this.name = '';
        }
      });
  }

  @HostListener('document:keydown.escape', ['$event'])
  handleEscape(event: KeyboardEvent) {
    this.close();
  }

  close() {
    this.router.navigate(['/Categories']);
  }
}
