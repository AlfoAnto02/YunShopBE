import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';

@Component({
  selector: 'app-product-detail',
  imports: [],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent {
  product!: Product;

  constructor(private ProductService: ProductService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.ProductService.getProductById(+id).subscribe(
        (data: Product) => this.product = data,
        error => console.error('There was an error!', error)
      );
    }
  }
}
