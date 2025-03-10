import { Component } from '@angular/core';
import { addProductRequest } from '../../../models/product';
import { Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { TokenService } from '../../../services/token.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-product',
  imports: [CommonModule,FormsModule],
  templateUrl: './new-product.component.html',
  styleUrl: './new-product.component.scss'
})
export class NewProductComponent {
    name: string = '';
    price: number = 0;
    imageUrl: string = '';
    description: string = '';
    addProductRequest: addProductRequest = {
      name: '',
      description: '',
      price: 0,
      imageUrl: ''
    };
  
    
    constructor(private router:Router, private ProductService: ProductService, private TokenService: TokenService) {}
    
    createAddProductRequest(): addProductRequest {
      return {
        name: this.name,
        description: this.description,
        price: this.price,
        imageUrl: this.imageUrl
      };
    }
    
    onSubmit() {
      this.ProductService.addProduct(this.addProductRequest)
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
    
    close() {
      this.router.navigate(['/Categories']);
    }
  }
