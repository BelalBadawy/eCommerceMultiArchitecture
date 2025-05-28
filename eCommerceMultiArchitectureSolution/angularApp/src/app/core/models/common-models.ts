export interface MyAppResponse<T> {
  statusCode: number;
  succeeded: boolean;
  message?: string;
  errors?: string[];
  data?: T;
  redirectTo?: string;
}

// Interface for paged result
export interface PagedResult<T> {
  currentPage: number;
  totalPages: number;
  totalCount: number;
  pageSize: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  data: T[];
}
