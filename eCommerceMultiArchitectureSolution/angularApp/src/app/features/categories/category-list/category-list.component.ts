import { Component, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common'; // For built-in directives if needed
import { CategoryService } from '../category.service';
import { GetAllByPageCategoryDto } from '../models/category-model';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, RouterLink, NgbPaginationModule],
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss'],
})
export class CategoryListComponent implements OnInit {
  categories: GetAllByPageCategoryDto[] = [];
  totalItems = 0;
  page = 1;
  pageSize = 10;
  loading = false;

  constructor(private categoryService: CategoryService) {}

  ngOnInit() {
    this.getCategories();
  }

  async getCategories() {
    this.loading = true;
    try {
      const response = await this.categoryService.getAllPaged(
        this.page,
        this.pageSize
      );
      if (response.data) {
        this.categories = response.data.data;
        this.totalItems = response.data.totalCount;
      }
    } finally {
      this.loading = false;
    }
  }

  pageChange(page: number) {
    this.page = page;
    this.getCategories();
  }
}
