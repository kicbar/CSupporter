export interface ApiResult<T> {
  statusCode: number;
  isSuccess: boolean;
  message?: string;
  data?: T;
}
