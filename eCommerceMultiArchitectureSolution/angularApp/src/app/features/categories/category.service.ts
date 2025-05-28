import { environment } from './../../../environments/environment';
import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { AuthService } from '../auth/auth.service';
import { MyAppResponse, PagedResult } from '../../core/models/common-models';
import { GetAllByPageCategoryDto } from './models/category-model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  http = inject(HttpClient);
  authService = inject(AuthService);

  constructor() {}

  async getAllPaged(
    pageIndex: number = 1,
    pageSize: number = 10,
    searchValue: string = '',
    orderBy: string = '',
    orderAscendingDirection: boolean = true
  ): Promise<MyAppResponse<PagedResult<GetAllByPageCategoryDto>>> {
    // Build query parameters
    let params = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString())
      .set('orderAscendingDirection', orderAscendingDirection.toString());

    if (searchValue) {
      params = params.set('searchValue', searchValue);
    }

    if (orderBy) {
      params = params.set('orderBy', orderBy);
    }

    const categories$ = this.http.get<
      MyAppResponse<PagedResult<GetAllByPageCategoryDto>>
    >(`${environment.apiRoot}/v1/Category/GetAllPagedList`, { params });

    const MyAppResponse = await firstValueFrom(categories$);
    return MyAppResponse;
  }

  async getAll(
    searchValue: string = '',
    orderBy: string = '',
    orderAscendingDirection: boolean = true
  ): Promise<MyAppResponse<GetAllByPageCategoryDto[]>> {
    let params = new HttpParams().set(
      'orderAscendingDirection',
      orderAscendingDirection.toString()
    );

    if (searchValue) {
      params = params.set('searchValue', searchValue);
    }

    if (orderBy) {
      params = params.set('orderBy', orderBy);
    }

    const categories$ = this.http.get<MyAppResponse<GetAllByPageCategoryDto[]>>(
      `${environment.apiRoot}/v1/Category/GetAll`,
      { params }
    );

    const MyAppResponse = await firstValueFrom(categories$);
    return MyAppResponse;
  }

  async getById(id: string): Promise<MyAppResponse<GetAllByPageCategoryDto>> {
    const category$ = this.http.get<MyAppResponse<GetAllByPageCategoryDto>>(
      `${environment.apiRoot}/v1/Category/${id}`
    );

    const MyAppResponse = await firstValueFrom(category$);
    return MyAppResponse;
  }

  async create(
    category: Omit<GetAllByPageCategoryDto, 'id'>
  ): Promise<MyAppResponse<GetAllByPageCategoryDto>> {
    const category$ = this.http.post<MyAppResponse<GetAllByPageCategoryDto>>(
      `${environment.apiRoot}/v1/Category`,
      category
    );

    const MyAppResponse = await firstValueFrom(category$);
    return MyAppResponse;
  }

  async update(
    id: string,
    category: Partial<GetAllByPageCategoryDto>
  ): Promise<MyAppResponse<GetAllByPageCategoryDto>> {
    const category$ = this.http.put<MyAppResponse<GetAllByPageCategoryDto>>(
      `${environment.apiRoot}/v1/Category/${id}`,
      category
    );

    const MyAppResponse = await firstValueFrom(category$);
    return MyAppResponse;
  }

  async delete(id: string): Promise<MyAppResponse<boolean>> {
    const category$ = this.http.delete<MyAppResponse<boolean>>(
      `${environment.apiRoot}/v1/Category/${id}`
    );

    const MyAppResponse = await firstValueFrom(category$);
    return MyAppResponse;
  }

  // Helper method to get categories for dropdowns/selects
  async getActiveCategories(): Promise<GetAllByPageCategoryDto[]> {
    try {
      const response = await this.getAll();
      if (response.succeeded && response.data) {
        return response.data.filter((category) => category.isActive);
      }
      return [];
    } catch (error) {
      console.error('Error fetching active categories:', error);
      return [];
    }
  }
}
