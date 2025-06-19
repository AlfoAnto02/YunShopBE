import { Component, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { CategoryService } from '../../../services/category.service';
import { BrandService } from '../../../services/brand.service';
import { SizeService } from '../../../services/size.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { addImageRequest, addProductSizeRequest } from '../../../models/product';
import { updateProductRequest } from '../../../models/product';
import { Category } from '../../../models/category';
import { Brand } from '../../../models/brand';
import { Size } from '../../../models/size';
import { TokenService } from '../../../services/token.service';

@Component({
  selector: 'app-product-edit',
  imports: [CommonModule, FormsModule],
  templateUrl: './product-edit.component.html',
  styleUrl: './product-edit.component.scss'
})
export class ProductEditComponent {
  productId!: number;
  product: any;

  updateProductRequest: updateProductRequest = {
    id: 0,
    addProductRequest: {
      name: '',
      description: '',
      images: [],
      categoryId: 0,
      addedBy: 0,
      brandId: 0,
      sizes: []
    }
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

  addProductSizeRequestBoxes: addProductSizeRequest[] = [
    {
      sizeId: 0,
      sizeValue: '',
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

  constructor(private route: ActivatedRoute, private ProductService: ProductService,
    private router: Router, private CategoryService: CategoryService,
    private BrandService: BrandService, private SizeService: SizeService,
    private TokenService: TokenService
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    this.productId = idParam ? Number(idParam) : 0;
    this.loadFields();
    this.getProduct(this.productId);
  }

  loadFields(): void {
    this.loadCategories();
    this.loadBrands();
    this.loadSizes();
  }

  getProduct(id: number): void {
    this.ProductService.getProductById(id).subscribe({
      next: (response: any) => {
        this.product = response.result.product;
        console.log('Product retrieved:', this.product);
        this.name = this.product.name;
        this.description = this.product.description;
        this.category = this.product.categoryName;
        this.brand = this.product.brandName;
        this.populateImages();
        this.populateProductSizes();
      },
      error: (error: any) => {
        console.error('Error getting product:', error);
      }
    });
  }

  populateImages() {
  if (this.product && Array.isArray(this.product.imageUrls)) {
    this.product.imageUrls.forEach((url: string) => {
      this.imageUrl = url;
      this.addImageUrl();
    });
      this.imageUrl = '';
    }
  }

  populateProductSizes() {
    if (this.product && Array.isArray(this.product.sizes)) {
      this.addProductSizeRequestBoxes = []; // Reset the array to avoid duplicates
      this.product.sizes.forEach((size: any) => {
        this.addProductSizeRequestBoxes.push({
          sizeId: size.sizeId,
          sizeValue: size.size,
          price: size.price,
          stock: size.stock,
          express: size.express,
          hide: size.hide
        });
      });
    }
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

  addNewItem(): void {
    this.addProductSizeRequestBoxes.push({
      sizeId: 0,
      sizeValue: '',
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

  createAddProductSizeRequest(): addProductSizeRequest[] {
    for (let i = 0; i < this.addProductSizeRequestBoxes.length; i++) {
      if (this.validateaddProductSizeRequestBox(this.addProductSizeRequestBoxes[i])) {
        this.productSizeRequests.push({
          sizeId: this.getSizeIdByName(this.addProductSizeRequestBoxes[i].sizeValue),
          sizeValue: this.addProductSizeRequestBoxes[i].sizeValue,
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

  onSubmit() {
    this.updateProductRequest = this.createUpdateProductRequest();
    console.log('Update Product Request:', this.updateProductRequest);
  }

  createUpdateProductRequest(): updateProductRequest {
    return {
      id: this.productId,
      addProductRequest: {
        name: this.name,
        description: this.description,
        images: this.images,
        categoryId: this.getCategoryIdByName(this.category),
        addedBy: this.TokenService.getUserIdByToken(),
        brandId: this.getBrandIdByName(this.brand),
        sizes: this.createAddProductSizeRequest()
      }
    };
  }

  close() {
    this.router.navigate(['/Products']);
  }
}