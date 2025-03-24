import { Component } from '@angular/core';
import { Brand } from '../../../models/brand';
import { BrandService } from '../../../services/brand.service';
import { CommonModule } from '@angular/common';
import { BrandComponent } from '../brand/brand.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-brands-list',
  imports: [CommonModule, BrandComponent, RouterLink],
  templateUrl: './brands-list.component.html',
  styleUrl: './brands-list.component.scss'
})
export class BrandsListComponent {
  brands: Brand[] = [];

  constructor(private BrandService: BrandService) { }

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands(): void {
    if (this.loadBrandsFromLocalStorage()) {
      console.log('Brands loaded from localStorage:', this.brands);
    } else {
      this.loadBrandsFromDatabase();
    }
  }

  loadBrandsFromLocalStorage(): boolean {
    const cachedBrands = localStorage.getItem('brands');
    if (cachedBrands) {
      this.brands = JSON.parse(cachedBrands);
      return true;
    }
    return false;
  }

  loadBrandsFromDatabase(): void {
    this.BrandService.getBrands().subscribe({
      next: (response: any) => {
        if (response && Array.isArray(response.result)) {
          this.brands = response.result;
          localStorage.setItem('brands', JSON.stringify(this.brands));
          console.log('Brands loaded from API:', this.brands);
        } else {
          console.error('Expected an array of brands, but got:', response);
          this.brands = [];
        }
      },
      error: (error: any) => {
        console.error('Error loading brands:', error);
      }
    });
  }
}
