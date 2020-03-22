import HttpService from '@/services/HttpService';
import Estado from '@/model/Estado';
import { AxiosPromise } from 'axios';
import Cidade from '@/model/Cidade';

export class EstadoService extends HttpService {
  constructor () {
    super({ baseURL: process.env.VUE_APP_ROOT_API });
  }

  public async getEstado () {
    return this.get<Estado>(`/Estado`);
  }

  public async getCidade (uf: String) {
    return this.get<Cidade>(`/Estado/cidade/${uf}`);
  }
}

export default new EstadoService();
