import { CommonModule } from "@angular/common";
import { Component, inject, Input, signal } from "@angular/core";
import { Category } from "../../shared/models/category.model";
import { CategoryService } from "../../core/services/category.service";

@Component({
  standalone: true,
  selector: 'app-category-tree',
  imports: [CommonModule],
  template: `
    <ul>
      <ng-container *ngFor="let cat of categories()">
        <li>
          {{ cat.name }}
          <span *ngFor="let cat_children of cat.children">({{ cat_children }})</span>
        </li>
      </ng-container>
    </ul>
  `,
})
export class CategoryTreeComponent {
    private categoryService = inject(CategoryService);

  @Input() childrenCategories: any[] = [];
  categories = signal<Category[]>([]);

    ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getAll().subscribe((res) => {
      this.categories.set(res);
    });
  }

}