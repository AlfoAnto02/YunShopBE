export interface Product {
  id: number;
  name: string;
  description: string;
  categoryName: string;
  createdAt: Date;
  updatedAt: Date;
  images: image[];
  categoryId: number;
  addedBy: number;
  brandId: number;
  sizes: productSize[];
}

export interface image {
  id: number;
  url: string;
  productId: number;
}

export interface productSize {
  id: number;
  productId: number;
  sizeId: number;
  stock: number;
  price: number;
  express: boolean;
  hide: boolean;
}

export interface addProductRequest {
  name: string;
  description: string;
  images: addImageRequest[];
  categoryId: number;
  userId: number;
  brandId: number;
  sizes: addProductSizeRequest[];
}

export interface addProductSizeRequest {
  sizeId: number;
  stock: number;
  price: number;
  express: boolean;
  hide: boolean;
}

export interface deleteProductRequest {
  ProductId: number;
  UserId: number;
}

export interface addImageRequest {
  url: string;
}
