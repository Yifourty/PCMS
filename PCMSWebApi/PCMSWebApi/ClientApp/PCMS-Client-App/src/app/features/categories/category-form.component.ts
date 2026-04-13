import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from '../../core/services/category.service';
import { Category } from '../../shared/models/category.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-category-form',
  imports: [ReactiveFormsModule],
  templateUrl: './category-form.component.html',
})
export class CategoryFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private categoryService = inject(CategoryService);
  
  categories = signal<Category[]>([]);
  loading = signal<boolean>(false);

  form = this.fb.group({
    name: ['', Validators.required],
    description: ['sku123', Validators.required],
    parentCategoryId: ['', Validators.required],
  });


  constructor(
    private route: ActivatedRoute,
    private _router: Router
  ) {

    }

  ngOnInit() {
    this.loadCategories();
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


  submit() {
    if (this.form.invalid) return;

    this.categoryService.create(this.form.value as any).subscribe({
      next: () => this._router.navigate(['../'], {relativeTo: this.route}).then(r =>{}),
      error: () => alert('Error creating category'),
    });
  }
} 