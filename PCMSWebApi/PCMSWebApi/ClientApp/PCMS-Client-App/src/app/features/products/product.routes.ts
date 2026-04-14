import { Routes } from '@angular/router';

export const PRODUCT_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./product-list.component')
        .then(m => m.ProductListComponent)
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./product-form.component')
        .then(m => m.ProductFormComponent)
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./product-detail.component')
        .then(m => m.ProductDetailComponent)
  }
];