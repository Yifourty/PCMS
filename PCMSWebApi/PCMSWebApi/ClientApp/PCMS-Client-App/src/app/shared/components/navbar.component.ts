import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, CommonModule],
  template: `
    <nav class="bg-gray-900 text-white px-6 py-4 shadow">
      <div class="max-w-7xl mx-auto flex justify-between items-center">

        <!-- Logo -->
        <a routerLink="/" class="text-xl font-bold tracking-wide">
          🛍️ PCMS
        </a>

        <!-- Links -->
        <div class="flex items-center gap-6">

          <a
            routerLink="/products"
            routerLinkActive="text-yellow-400"
            class="hover:text-yellow-300 transition"
          >
            Products
          </a>

          <a
            routerLink="/products/create"
            routerLinkActive="text-yellow-400"
            class="hover:text-yellow-300 transition"
          >
            Add Product
          </a>

          <a
            routerLink="/categories"
            routerLinkActive="text-yellow-400"
            class="hover:text-yellow-300 transition"
          >
            Categories
          </a>

                    <a
            routerLink="/categories/create"
            routerLinkActive="text-yellow-400"
            class="hover:text-yellow-300 transition"
          >
            Add Category
          </a>

        </div>

      </div>
    </nav>
  `,
})
export class NavbarComponent {}