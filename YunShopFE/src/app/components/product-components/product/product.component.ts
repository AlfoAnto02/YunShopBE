import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouteConfigLoadEnd, RouterLink } from '@angular/router';
import { Product } from '../../../models/product';

@Component({
  selector: 'app-product',
  imports: [CommonModule, RouterLink],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent {
  @Input() product!: Product;
}
