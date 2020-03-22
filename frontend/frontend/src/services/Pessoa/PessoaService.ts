import HttpService from '@/services/HttpService';
import Pessoa from '@/model/Pessoa';
import { AxiosPromise } from 'axios';

export class PessoaService extends HttpService {
  constructor () {
    super({ baseURL: process.env.VUE_APP_ROOT_API });
  }

  public async postPessoa (pessoa: Pessoa) {
    return this.post<Pessoa>(`/Pessoa`, pessoa);
  }
  public async getCounter () {
    return this.get(`/Pessoa/Counter`);
  }
  public async getMap () {
    return this.get(`/Pessoa/Map`);
  }
  public async getCounterByStateAndMonth () {
    return this.get(`/Pessoa/CounterByStateAndMonth`);
  }
}

export default new PessoaService();
