import { Injectable, inject } from "@angular/core";
import { ApiService } from "./api.service";

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

  create(category: any) {
    return this.api.post(`${this.baseUrl}`, category);
  }
}