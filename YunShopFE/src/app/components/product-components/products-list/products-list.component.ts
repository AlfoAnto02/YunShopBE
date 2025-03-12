import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ProductComponent } from '../product/product.component';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-products-list',
  imports: [CommonModule, ProductComponent, RouterLink],
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.scss'
})
export class ProductsListComponent {
  products: Product[] = [];

  constructor(private ProductService:ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.ProductService.getProducts().subscribe({
      next: (response: any) => {
        if (response && Array.isArray(response.result)) {
          this.products = response.result;
          console.log('Products loaded:', this.products);
        } else {
          console.error('Expected an array of produts, but got:', response);
          this.products = [];
        }
      },
      error: (error: any) => {
        console.error('Error loading products:', error);
      }
    });
  }
}
