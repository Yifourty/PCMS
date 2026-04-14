import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from '../../core/services/category.service';
import { Category, CreateCategory } from '../../shared/models/category.model';
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
    name: ['Appliances', Validators.required],
    description: ['Electrical appliances and gadgets'],
    parentCategoryId: [''],
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
      },
    });
  }


  submit() {
    if (this.form.invalid) return;

    const categoryData: CreateCategory = {
      name: this.form.value.name!,
      description: this.form.value.description as string || undefined,
      parentCategoryId: this.form.value.parentCategoryId || undefined,
    };

    this.categoryService.create(categoryData).subscribe({
      next: () => this._router.navigate(['../'], {relativeTo: this.route}).then(r =>{}),
      error: () => alert('Error creating category'),
    });
  }
} 