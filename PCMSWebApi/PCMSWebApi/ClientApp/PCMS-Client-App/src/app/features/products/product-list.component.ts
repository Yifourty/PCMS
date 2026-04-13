import { Component, inject, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { combineLatest, startWith, debounceTime, switchMap } from 'rxjs';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../core/services/category.service';
import { ProductService } from '../../core/services/product.service';
import { Product } from '../../shared/models/product.model';
import { Category } from '../../shared/models/category.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-product-list',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './product-list.component.html',
})
export class ProductListComponent {

  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  loading = signal<boolean>(false);

  // Signals
  products = signal<Product[]>([]);
  categories = signal<Category[]>([]);
  page = signal(1);

  // Controls
  searchControl = new FormControl('');
  categoryControl = new FormControl('');
  pageSizeControl = new FormControl(10);

    constructor(
    private route: ActivatedRoute,
    private _router: Router
  ) {

    }
    
  ngOnInit() {
    this.loadCategories();
    this.setupStream();
  }

  loadCategories() {
    this.loading.set(true);
    this.categoryService.getAll().subscribe({
      next: (res) => {
        this.loading.set(false);
        this.categories.set(res);
      },
      error: () => {
        this.loading.set(false);
        alert('Error loading categories');
      },
    });
  }

  viewDetails(productId: string) {
    this._router.navigate([productId], {relativeTo: this.route}).then(r =>{})
  }

  setupStream() {
    this.loading.set(true);

    combineLatest([
      this.searchControl.valueChanges.pipe(startWith(''), debounceTime(400)),
      this.categoryControl.valueChanges.pipe(startWith('')),
      this.pageSizeControl.valueChanges.pipe(startWith(10)),
    ])
      .pipe(
        switchMap(([search, categoryId, pageSize]) =>
          this.productService.getProducts({
            search: search || '',
            categoryId: categoryId || '',
            page: this.page(),
            pageSize: pageSize || 10,
          })
        )
      )
      .subscribe({
      next: (res) => {
        this.loading.set(false);
        this.products.set(res);
      },
      error: () => {
        this.loading.set(false);
        alert('Error loading products');
      },
    });
  }

  nextPage() {
    this.page.update((p) => p + 1);
    this.reload();
  }

  prevPage() {
    this.page.update((p) => Math.max(1, p - 1));
    this.reload();
  }

  reload() {
    this.productService
      .getProducts({
        search: this.searchControl.value || '',
        categoryId: this.categoryControl.value || '',
        page: this.page(),
        pageSize: this.pageSizeControl.value || 10,
      })
      .subscribe((res) => this.products.set(res));
  }
}