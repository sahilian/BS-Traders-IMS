export interface Product {
  id: number;
  name: string;
  description: string;
  stockLevel: number;
  price: number;
  discount?: number; // Optional property
  categoryId: number;
}
