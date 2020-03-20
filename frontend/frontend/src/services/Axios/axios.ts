import _Vue from 'vue';
import Axios, { AxiosRequestConfig } from 'axios';

type ValidInterceptor = 'request' | 'response' | 'error';
const defaultInterceptors = {
  request: (req: any) => req,
  response: (res: any) => res,
  error: (err: any) => Promise.reject(err)
};

function getAxiosInterceptor (type: ValidInterceptor) {
  try {
    const requiredInterceptor = require(`@/services/Axios/${type}`);
    return requiredInterceptor.default;
  } catch (error) {
    return defaultInterceptors[type];
  }
}

export function createAxiosInstance (config?: AxiosRequestConfig | undefined) {
  const interceptors = {
    request: getAxiosInterceptor('request'),
    response: getAxiosInterceptor('response'),
    error: getAxiosInterceptor('error')
  };
  const axiosInstance = Axios.create(config);
  axiosInstance.interceptors.request.use(interceptors.request);
  axiosInstance.interceptors.response.use(interceptors.response, interceptors.error);
  return axiosInstance;
}

export default function VueAxiosPlugin (Vue: typeof _Vue, config?: AxiosRequestConfig | undefined): void {
  const axios = createAxiosInstance();
  Vue.prototype.$axios = axios;
}
