export interface Product {
  id: number;
  name: string;
  price: number;
  size: number;
  description: string;
  brand: string;
  imageUrls: addProductRequest[];
  createdAt: Date;
  updatedAt: Date;
  categoryId: number;
  stock: number;
  userId: number;
}

export interface addProductRequest {
  name: string;
  price: number;
  size: number;
  description: string;
  brand: string;
  images: addImageRequest[];
  categoryId: number;
  stock: number;
  userId: number;
  }

export interface deleteProductRequest {
  ProductId: number;
  UserId: number;
}

export interface addImageRequest {
  url: string;
}