import Axios, { AxiosInstance, AxiosRequestConfig, AxiosPromise } from 'axios';
import { createAxiosInstance } from '@/services/Axios/axios';

export default class HttpService {
  protected axiosInstance: AxiosInstance;
  protected axiosOptions?: AxiosRequestConfig;

  constructor (config?: AxiosRequestConfig | undefined) {
    this.axiosOptions = config;
    this.axiosInstance = createAxiosInstance(config);
  }

  protected request<T = any> (config: AxiosRequestConfig): AxiosPromise<T> {
    return this.axiosInstance.request(config);
  }

  protected get<T = any> (url: string, config?: AxiosRequestConfig): AxiosPromise<T> {
    return this.axiosInstance.get(url, config);
  }

  protected delete (url: string, config?: AxiosRequestConfig): AxiosPromise {
    return this.axiosInstance.delete(url, config);
  }
  protected head (url: string, config?: AxiosRequestConfig): AxiosPromise {
    return this.axiosInstance.head(url, config);
  }
  protected post<T = any> (url: string, data?: any, config?: AxiosRequestConfig): AxiosPromise<T> {
    return this.axiosInstance.post(url, data, config);
  }
  protected put<T = any> (url: string, data?: any, config?: AxiosRequestConfig): AxiosPromise<T> {
    return this.axiosInstance.put(url, data, config);
  }
  protected patch<T = any> (url: string, data?: any, config?: AxiosRequestConfig): AxiosPromise<T> {
    return this.axiosInstance.patch(url, data, config);
  }
}
