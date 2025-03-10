import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ProductComponent } from '../product/product.component';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';

@Component({
  selector: 'app-products-list',
  imports: [CommonModule, ProductComponent],
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
    this.ProductService.getProducts().subscribe(
      (data: Product[]) => {
        this.products = data;
      },
      error => console.error('There was an error!', error)
    );
  }
}
