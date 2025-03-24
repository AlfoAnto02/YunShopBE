import { Component, HostListener } from '@angular/core';
import { addImageRequest, addProductRequest, addProductSizeRequest } from '../../../models/product';
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
  addProductRequest: addProductRequest = {
    name: '',
    description: '',
    images: [],
    categoryId: 0,
    userId: 0,
    brandId: 0,
    sizes: []
  };
  name: string = '';
  description: string = '';
  imageUrl: string = '';
  addImageRequest: addImageRequest = {
    url: ''
  };
  images: addImageRequest[] = [];
  category: string = '';
  categories: Category[] = [];
  userId: number = 0;
  brand: string = '';
  brands: Brand[] = [];

  addProductSizeRequestBoxes: any[] = [
    {
      sizeValue: 0,
      price: 0,
      stock: 0,
      express: false,
      hide: false,
    }
  ];
  productSizeRequests: addProductSizeRequest[] = [];
  sizes: Size[] = [];
  sizeValue: string = '';
  stock: number = 0;
  price: number = 0;
  express: boolean = true;
  hide: boolean = false;


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
    const cachedCategories = localStorage.getItem('categories');
    if (cachedCategories) {
      this.categories = JSON.parse(cachedCategories);
      console.log('Categories loaded from localStorage:', this.categories);
    } else {
      this.CategoryService.getCategories().subscribe({
        next: (response: any) => {
          if (response && Array.isArray(response.result)) {
            this.categories = response.result;
            localStorage.setItem('categories', JSON.stringify(this.categories));
            console.log('Categories loaded from API:', this.categories);
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
  }

  loadBrands(): void {
    const cachedBrands = localStorage.getItem('brands');
    if (cachedBrands) {
      this.brands = JSON.parse(cachedBrands);
      console.log('Brands loaded from localStorage:', this.brands);
    } else {
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

  loadSizes(): void {
    const cachedSizes = localStorage.getItem('sizes');
    if (cachedSizes) {
      this.sizes = JSON.parse(cachedSizes);
      console.log('Sizes loaded from localStorage:', this.sizes);
    } else {
      this.SizeService.getSizes().subscribe({
        next: (response: any) => {
          if (response && Array.isArray(response.result)) {
            this.sizes = response.result;
            localStorage.setItem('sizes', JSON.stringify(this.sizes));
            console.log('Sizes loaded from API:', this.sizes);
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

  getBrandIdByName(name: string): number {
    for (let i = 0; i < this.brands.length; i++) {
      if (this.brands[i].name === name) {
        console.log('Brand ID:', this.brands[i].id);
        return this.brands[i].id;
      }
    }
    return 0;
  }

  getSizeIdByName(sizeValue: string): number {
    for (let i = 0; i < this.sizes.length; i++) {
      if (this.sizes[i].sizeValue === sizeValue) {
        console.log('Size ID:', this.sizes[i].id);
        return this.sizes[i].id;
      }
    }
    return 0;
  }

  validateaddProductSizeRequestBox(box: any): boolean {
    if (!box.sizeValue || box.sizeValue === null) {
      console.error('Size cannot be null');
      return false;
    }
    if (box.stock < 0) {
      console.error('Stock must be greater than or equal to 0');
      return false;
    }
    if (box.price <= 0) {
      console.error('Price must be a positive number');
      return false;
    }
    return true;
  }

  createAddProductSizeRequest(): addProductSizeRequest[] {
    for (let i = 0; i < this.addProductSizeRequestBoxes.length; i++) {
      if (this.validateaddProductSizeRequestBox(this.addProductSizeRequestBoxes[i])) {
        this.productSizeRequests.push({
          sizeId: this.getSizeIdByName(this.addProductSizeRequestBoxes[i].sizeValue),
          stock: this.addProductSizeRequestBoxes[i].stock,
          price: this.addProductSizeRequestBoxes[i].price,
          express: this.addProductSizeRequestBoxes[i].express,
          hide: this.addProductSizeRequestBoxes[i].hide
        });
      } else {
        console.error('Validation failed for box:', this.addProductSizeRequestBoxes[i]);
        alert('Validation failed. Please check the input values.');
        return this.productSizeRequests = [];
      }
    }
    return this.productSizeRequests;
  }

  createAddProductRequest(): addProductRequest {
    return {
      name: this.name,
      description: this.description,
      images: this.images,
      categoryId: this.getCategoryIdByName(this.category),
      userId: this.TokenService.getUserIdByToken(),
      brandId: this.getBrandIdByName(this.brand),
      sizes: this.createAddProductSizeRequest()
    };
  }

  onSubmit() {
    this.addProductRequest = this.createAddProductRequest();
    this.ProductService.addProduct(this.addProductRequest)
      .subscribe({
        next: response => {
          console.log('Product created successfully:', response);
          alert('Product created successfully');
          this.resetForm();
          this.router.navigate(['/New-Product']);
        },
        error: error => {
          console.error('Error creating product:', error);
          alert('Error creating product: ' + (error.error?.message || 'Unknown error'));
        }
      });
  }

  addNewItem(): void {
    this.addProductSizeRequestBoxes.push({
      sizeId: 0,
      price: 0,
      stock: 0,
      express: false,
      hide: false
    });
  }

  removeLastItem(): void {
    if (this.addProductSizeRequestBoxes.length > 0) {
      this.addProductSizeRequestBoxes.pop();
    }
  }

  resetForm(): void {
    this.name = '';
    this.description = '';
    this.imageUrl = '';
    this.images = [];
    this.category = '';
    this.userId = 0;
    this.brand = '';
    this.productSizeRequests = [];
    this.sizeValue = '';
    this.addProductSizeRequestBoxes = [
      {
        sizeValue: '',
        price: 0,
        stock: 0,
        express: false,
        hide: false,
      }
    ];
  }

  @HostListener('document:keydown.escape', ['$event'])
  handleEscape(event: KeyboardEvent) {
    this.close();
  }

  close() {
    this.router.navigate(['/Products']);
  }
}
