import { Injectable, inject } from "@angular/core";
import { ApiService } from "./api.service";
import { CreateCategory } from "../../shared/models/category.model";

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private api = inject(ApiService);
  private baseUrl = 'api/categories'; 

  getAll() {
    return this.api.get<any[]>(`${this.baseUrl}`);
  }

  getTree() {
    return this.api.get<any[]>(`${this.baseUrl}/tree`);
  }

  create(category: CreateCategory) {
    return this.api.post(`${this.baseUrl}`, category);
  }
}