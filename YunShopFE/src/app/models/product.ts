//TODO sistemare il contenuto di questo file
export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  category: string;
  imageUrl: string;
}

export interface addProductRequest {
  name: string;
  description: string;
  price: number;
  imageUrl: string;
}