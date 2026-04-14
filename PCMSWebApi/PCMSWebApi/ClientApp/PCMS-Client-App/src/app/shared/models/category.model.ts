// shared/models/category.model.ts
export interface Category {
  id: string;
  name: string;
  description?: string;
  parentCategoryId?: string | null;
  children?: Category[];
}

export interface CreateCategory {
  name: string;
  description?: string;
  parentCategoryId?: string;
}