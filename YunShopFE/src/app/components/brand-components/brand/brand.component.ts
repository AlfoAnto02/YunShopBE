import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Brand } from '../../../models/brand';
import { UserService } from '../../../services/user.service';
import { TokenService } from '../../../services/token.service';
import { BrandService } from '../../../services/brand.service';

@Component({
  selector: 'app-brand',
  imports: [CommonModule],
  templateUrl: './brand.component.html',
  styleUrl: './brand.component.scss'
})
export class BrandComponent {
  @Input() brand!: Brand;
  addedByUsername: string = '';

  deleteBrandRequest: any = {
    brandId: 0
  };

  constructor(private userService: UserService, private tokenService: TokenService,
    private brandService: BrandService) { }

  ngOnInit(): void {
    this.getAddedByUsername();
  }

  getAddedByUsername(): void {
    this.userService.getUserById(this.brand.addedBy).subscribe({
      next: (response: any) => {
        this.addedByUsername = response.result.user.userName;
      },
      error: (error: any) => {
        console.error('Error loading user:', error);
      }
    });
  }

  createDeleteBrandRequest(): void {
    this.deleteBrandRequest = {
      brandId: this.brand.id,
    };
  }

  deleteBrand(): void {
    this.createDeleteBrandRequest();
    console.log('Deleting Brand:', this.deleteBrandRequest);
    this.brandService.deleteBrand(this.deleteBrandRequest).subscribe({
      next: (response: any) => {
        console.log('Brand deleted:', response);
        
        // Remove the brand from localStorage
        const cachedBrands = localStorage.getItem('brands');
        if (cachedBrands) {
          let brands = JSON.parse(cachedBrands);
          brands = brands.filter((brand: Brand) => brand.id !== this.brand.id);
          localStorage.setItem('brands', JSON.stringify(brands));
        }
        
        window.location.reload();
      },
      error: (error: any) => {
        console.error('Error deleting Brand:', error);
      }
    });
  }
}
