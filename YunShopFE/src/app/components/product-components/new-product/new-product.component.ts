import { Component, HostListener } from '@angular/core';
import { addImageRequest, addProductRequest } from '../../../models/product';
import { Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { TokenService } from '../../../services/token.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category.service';
import { BrandService } from '../../../services/brand.service';
import { Brand } from '../../../models/brand';
import { SizeService } from '../../../services/size.service';
import { Size } from '../../../models/size';

@Component({
  selector: 'app-new-product',
  imports: [CommonModule, FormsModule],
  templateUrl: './new-product.component.html',
  styleUrl: './new-product.component.scss'
})
export class NewProductComponent {
  name: string = '';
  price: number = 0;
  size: number = 0;
  description: string = '';
  brand: string = '';
  category: string = '';
  stock: number = 0;
  userId: number = 0;

  express: boolean = false;
  hide: boolean = false;
  oos: boolean = false;

  imageUrl: string = '';
  images: addImageRequest[] = [];

  categories: Category[] = [];
  brands: Brand[] = [];
  sizes: Size[] = [];

  addProductRequest: addProductRequest = {
    name: '',
    price: 0,
    size: 0,
    description: '',
    brand: '',
    images: [],
    categoryId: 0,
    stock: 0,
    userId: 0
  };

  addImageRequest: addImageRequest = {
    url: ''
  };

  boxes: any[] = [
    {
      size: 0,
      price: 0,
      stock: 0,
      express: false,
      hide: false,
      oos: false
    }
  ];

  constructor(private router: Router, private ProductService: ProductService,
    private TokenService: TokenService, private CategoryService: CategoryService,
    private BrandService: BrandService, private SizeService: SizeService) { }

  ngOnInit(): void {
    this.loadFields();
  }

  loadFields(): void {
    this.loadCategories();
    this.loadBrands();
    this.loadSizes();
  }

  loadCategories(): void {
    this.CategoryService.getCategories().subscribe({
      next: (response: any) => {
        if (response && Array.isArray(response.result)) {
          this.categories = response.result;
          console.log('Categories loaded:', this.categories);
        } else {
          console.error('Expected an array of categories, but got:', response);
          this.categories = [];
        }
      },
      error: (error: any) => {
        console.error('Error loading categories:', error);
      }
    });
  }

  loadBrands(): void {
    this.BrandService.getBrands().subscribe({
      next: (response: any) => {
        if (response && Array.isArray(response.result)) {
          this.brands = response.result;
          console.log('Brands loaded:', this.brands);
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

  loadSizes(): void {
    this.SizeService.getSizes().subscribe({
      next: (response: any) => {
        if (response && Array.isArray(response.result)) {
          this.sizes = response.result;
          console.log('Sizes loaded:', this.sizes);
        } else {
          console.error('Expected an array of sizes, but got:', response);
          this.sizes = [];
        }
      },
      error: (error: any) => {
        console.error('Error loading sizes:', error);
      }
    });
  }

  addImageUrl(): void {
    if (this.imageUrl) {
      this.addImageRequest.url = this.imageUrl;
      this.images.push(this.addImageRequest);
      this.imageUrl = '';
      this.addImageRequest = {
        url: ''
      };
    }
  }

  getCategoryIdByName(name: string): number {
    for (let i = 0; i < this.categories.length; i++) {
      if (this.categories[i].name === name) {
        console.log('Category ID:', this.categories[i].id);
        return this.categories[i].id;
      }
    }
    return 0;
  }

  createAddProductRequest(): addProductRequest {
    return {
      name: this.name,
      price: this.price,
      size: this.size,
      description: this.description,
      brand: this.brand,
      images: this.images,
      categoryId: this.getCategoryIdByName(this.category),
      stock: this.stock,
      userId: this.TokenService.getUserIdByToken(),
    };
  }


  onSubmit() {
    this.addProductRequest = this.createAddProductRequest();
    console.log('Creating product:', this.addProductRequest);
    this.ProductService.addProduct(this.addProductRequest)
      .subscribe({
        next: response => {
          console.log('Product created successfully:', response);
          alert('Product created successfully');
          this.clearCurrentInput();
          this.router.navigate(['/New-Product']);
        },
        error: error => {
          console.error('Error creating product:', error);
          alert('Error creating product: ' + (error.error?.message || 'Unknown error'));
          this.clearCurrentInput();
          this.router.navigate(['/New-Product']);
        }
      });
  }

  clearCurrentInput(): void {
    this.name = '';
    this.price = 0;
    this.size = 0;
    this.description = '';
    this.brand = '';
    this.category = '';
    this.stock = 0;
    this.imageUrl = '';
    this.images = [];
  }

  addNewItem(): void {
    this.boxes.push({
      size: 0,
      price: 0,
      stock: 0,
      express: false,
      hide: false,
      oos: false
    });
  }

  @HostListener('document:keydown.escape', ['$event'])
  handleEscape(event: KeyboardEvent) {
    this.close();
  }

  close() {
    this.router.navigate(['/Products']);
  }
}
