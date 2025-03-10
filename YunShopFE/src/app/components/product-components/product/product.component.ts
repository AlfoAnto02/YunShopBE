import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouteConfigLoadEnd, RouterLink } from '@angular/router';
import { deleteProductRequest, Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';
import { TokenService } from '../../../services/token.service';

@Component({
  selector: 'app-product',
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})

export class ProductComponent {
  @Input() product!: Product;
  deleteProductRequest: deleteProductRequest = {
    ProductId: 0,
    UserId: 0
  };

  constructor(private productService:ProductService, private tokenService: TokenService) { }

  createDeleteProductRequest(): void {
    this.deleteProductRequest = {
      ProductId: this.product.id,
      UserId: this.tokenService.getUserIdByToken()
    };
  }
  
  deleteProduct(): void {
    this.createDeleteProductRequest();
    console.log('Deleting product:', this.deleteProductRequest);
    this.productService.deleteProduct(this.deleteProductRequest).subscribe({
      next: (response: any) => {
        console.log('Product deleted:', response);
        window.location.reload();
      },
      error: (error: any) => {
        console.error('Error deleting product:', error);
      }
    });
  }
}
