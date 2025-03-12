import { Component } from '@angular/core';
import { addImageRequest, addProductRequest } from '../../../models/product';
import { Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { TokenService } from '../../../services/token.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Category } from '../../../models/category';
import { CategoriesService } from '../../../services/category.service';

@Component({
  selector: 'app-new-product',
  imports: [CommonModule,FormsModule],
  templateUrl: './new-product.component.html',
  styleUrl: './new-product.component.scss'
})
export class NewProductComponent {
    name: string = '';
    price: number = 0;
    size: number = 0;
    description: string = '';
    brand: string = '';
    category: string = '';
    stock: number = 0;
    userId: number = 0;
    
    imageUrl: string = '';
    images: addImageRequest[] = [];
    
    categories: Category[] = [];

    addProductRequest: addProductRequest = {
      name: '',
      price: 0,
      size: 0,
      description: '',
      brand: '',
      images: [],
      categoryId: 0,
      stock: 0,
      userId: 0
    };

    addImageRequest: addImageRequest = {
      url: ''
    };
  
    constructor(private router:Router, private ProductService: ProductService, 
                private TokenService: TokenService, private CategoriesService: CategoriesService) {}
    
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

    addImageUrl(): void {
      if (this.imageUrl) {
        this.addImageRequest.url = this.imageUrl;
        this.images.push(this.addImageRequest);
        this.imageUrl = '';
        this.addImageRequest = {
          url: ''
        };
      }
    }

    getCategoryIdByName(name: string): number {
      for (let i = 0; i < this.categories.length; i++) {
        if (this.categories[i].name === name) {
          console.log('Category ID:', this.categories[i].id);
          return this.categories[i].id;
        }
      }
      return 0;
    }
    
    createAddProductRequest(): addProductRequest {
      return {
        name: this.name,
        price: this.price,
        size: this.size,
        description: this.description,
        brand: this.brand,
        images: this.images,
        categoryId: this.getCategoryIdByName(this.category),
        stock: this.stock,
        userId: this.TokenService.getUserIdByToken()
      };
    }
    
    onSubmit() {
      this.addProductRequest = this.createAddProductRequest();
      console.log('Creating product:', this.addProductRequest);
      this.ProductService.addProduct(this.addProductRequest)
      .subscribe({
        next: response => {
          console.log('Product created successfully:', response);
          this.router.navigate(['/Products']);
        },
        error: error => {
          console.error('Error creating product:', error);
          alert('Error creating product: ' + (error.error?.message || 'Unknown error'));
        }
      });
    }
    
    close() {
      this.router.navigate(['/Products']);
    }
  }
