import HttpService from '@/services/HttpService';
import { AxiosPromise } from 'axios';

export class CovidService extends HttpService {
  constructor () {
    super({ baseURL: process.env.VUE_APP_ROOT_API });
  }

  public async getMapPerState () {
    return this.get(`/Covid19/Map`);
  }
}

export default new CovidService();
