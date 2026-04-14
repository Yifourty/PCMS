import { Component, inject, signal, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router'; // Import this
import { CategoryService } from '../../core/services/category.service';
import { ProductService } from '../../core/services/product.service';
import { Product } from '../../shared/models/product.model';


@Component({
  standalone: true,
  selector: 'app-product-detail',
  imports: [ReactiveFormsModule],
  templateUrl: './product-detail.component.html'
})
export class ProductDetailComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private categoryService = inject(CategoryService);
  private productService = inject(ProductService);

  productId = signal<string | null>(null);
  categories = signal<any[]>([]);
  isLoading = signal(false);
  isSaving = signal(false);

  form = this.fb.group({
    name: ['', Validators.required],
    sku: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(1)]],
    quantity: [0, [Validators.required, Validators.min(1)]],
    categoryId: ['', Validators.required],
  });

    constructor(
    private _router: Router
  ) {

    }
    
  ngOnInit() {
    this.loadCategories();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productId.set(id);
      this.loadProduct(id);
    }
  }

  loadCategories() {
    this.categoryService.getAll().subscribe(res => this.categories.set(res));
  }

  loadProduct(id: string) {
    this.isLoading.set(true);
    this.productService.getById(id).subscribe({
      next: (product) => {
        this.form.patchValue(product); // 2. Fill the form with data
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  submit() {
    if (this.form.invalid) return;
    
    this.isSaving.set(true);
    const id = this.productId();
    
    const productData = this.form.value as Product;

    const request = id 
      ? this.productService.update(id, productData) 
      : this.productService.create(productData);

    request.subscribe({
      next: () => {this._router.navigate(['../'], {relativeTo: this.route}).then(r =>{}) },
      complete: () => this.isSaving.set(false)
    });
  }
}
