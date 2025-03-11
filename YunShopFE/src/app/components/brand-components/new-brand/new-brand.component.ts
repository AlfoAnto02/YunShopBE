import { Component, HostListener } from '@angular/core';
import { addBrandRequest } from '../../../models/brand';
import { Router } from '@angular/router';
import { TokenService } from '../../../services/token.service';
import { BrandService } from '../../../services/brand.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-brand',
  imports: [CommonModule, FormsModule],
  templateUrl: './new-brand.component.html',
  styleUrl: './new-brand.component.scss'
})
export class NewBrandComponent {
  name: string = '';
  currentUserId: number = 0;
  addBrandRequest: addBrandRequest = {
    name: '',
    addedBy: 0
  };

  constructor(private router: Router, private BrandsService: BrandService,
    private TokenService: TokenService) { }

  ngOnInit(): void {
    this.getCurrentUserId();
  }

  getCurrentUserId() {
    const token = this.TokenService.getToken();
    if (token) {
      const decodedPayload = this.TokenService.decodeToken(token);
      this.currentUserId = decodedPayload.user_id;
    }
  }

  createAddBrandRequest(): void {
    this.addBrandRequest =
    {
      name: this.name,
      addedBy: this.currentUserId
    };
  }

  onSubmit() {
    this.createAddBrandRequest()
    this.BrandsService.addBrand(this.addBrandRequest)
      .subscribe({
        next: response => {
          console.log('Brand created successfully:', response);
          alert('Brand created successfully');
          this.name = '';
          this.router.navigate(['/New-Brand']);
        },
        error: error => {
          console.error('Error creating Brand:', error);
          alert('Error creating Brand: ' + (error.error?.message || 'Unknown error'));
          this.name = '';
        }
      });
  }

  @HostListener('document:keydown.escape', ['$event'])
  handleEscape(event: KeyboardEvent) {
    this.close();
  }

  close() {
    this.router.navigate(['/Brands']);
  }
}
