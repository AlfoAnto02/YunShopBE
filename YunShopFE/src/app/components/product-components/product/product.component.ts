import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouteConfigLoadEnd, RouterLink, RouterModule } from '@angular/router';
import { deleteProductRequest, Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';
import { TokenService } from '../../../services/token.service';

@Component({
  selector: 'app-product',
  imports: [CommonModule, RouterModule, RouterLink],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})

export class ProductComponent {
  @Input() product!: Product;
  
  deleteProductRequest: deleteProductRequest = {
    productId: 0,
    deletedBy: 0
  };

  constructor(private productService:ProductService, private tokenService: TokenService) { }

  createDeleteProductRequest(): void {
    this.deleteProductRequest = {
      productId: this.product.id,
      deletedBy: this.tokenService.getUserIdByToken()
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
