import { Routes } from '@angular/router';

export const CATEGORY_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./category-tree.component')
        .then(m => m.CategoryTreeComponent)
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./category-form.component')
        .then(m => m.CategoryFormComponent)
  },
];