import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { ActivatedRoute, Router, RouterLink, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule, CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product-detail',
  imports: [FormsModule, CommonModule, CurrencyPipe, RouterModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent implements OnInit {
  productId: number = 0;
  product: any = {};
  currentImageIndex: number = 0;
  constructor(private route: ActivatedRoute, private router: Router, private ProductService: ProductService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.productId = +params.get('id')!;
      this.getProductById(this.productId);
    });
  }

  getProductById(id: number): void {
    this.ProductService.getProductById(id).subscribe({
      next: (response: any) => {
        this.product = response.result;
      },
      error: (error: any) => {
        console.error('Error getting product:', error);
      }
    });
  }

  prevImage(): void {
    if (this.currentImageIndex > 0) {
      this.currentImageIndex--;
    }
  }

  nextImage(): void {
    if (this.currentImageIndex < this.product.product.imageUrls.length - 1) {
      this.currentImageIndex++;
    }
  }

  goBack(): void {
    this.router.navigate(['/Products']);
  }
}
