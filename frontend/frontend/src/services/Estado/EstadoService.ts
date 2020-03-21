import HttpService from '@/services/HttpService';
import Estado from '@/model/Estado';
import { AxiosPromise } from 'axios';

export class EstadoService extends HttpService {
  constructor () {
    super({ baseURL: process.env.VUE_APP_ROOT_API });
  }

  public async getEstado () {
    return this.get<Estado>(`/Estado`);
  }
}

export default new EstadoService();
