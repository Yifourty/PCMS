import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Product } from '../../shared/models/product.model';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private api = inject(ApiService);
  private baseUrl = 'api/products'; 

  getProducts(params?: {
    page?: number;
    pageSize?: number;
    search?: string;
    categoryId?: string;
  }): Observable<Product[]> {
    const query = new URLSearchParams(params as any).toString();
    return this.api.get<Product[]>(`${this.baseUrl}?${query}`);
  }

  getById(id: string) {
    return this.api.get<Product>(`${this.baseUrl}/${id}`);
  }

  create(product: Product) {
    return this.api.post<Product>(`${this.baseUrl}`, product);
  }

  update(id: string, product: Product) {
    return this.api.put(`${this.baseUrl}/${id}`, product);
  }

  delete(id: string) {
    return this.api.delete(`${this.baseUrl}/${id}`);
  }
}