// import { Routes } from '@angular/router';

// export const routes: Routes = [
//   {
//     path: '',
//     redirectTo: 'products',
//     pathMatch: 'full'
//   },
//   {
//     path: 'products',
//     loadChildren: () =>
//       import('./features/products/product.routes')
//         .then(m => m.PRODUCT_ROUTES)
//   },
//   {
//     path: 'categories',
//     loadChildren: () =>
//       import('./features/categories/category.routes')
//         .then(m => m.CATEGORY_ROUTES)
//   },
//   {
//     path: '**',
//     redirectTo: 'products'
//   }
// ];


import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        redirectTo: 'products',
        pathMatch: 'full'
      },
      {
        path: 'products',
        loadChildren: () =>
          import('./features/products/product.routes')
            .then(m => m.PRODUCT_ROUTES)
      },
      {
        path: 'categories',
        loadChildren: () =>
          import('./features/categories/category.routes')
            .then(m => m.CATEGORY_ROUTES)
      },
      {
        path: '**',
        redirectTo: 'products'
      },
      { path: '', redirectTo: 'products', pathMatch: 'full' }
    ]
  }
];