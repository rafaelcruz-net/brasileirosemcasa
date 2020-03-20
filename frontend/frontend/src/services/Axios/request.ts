import store from '@/store';
import _ from 'lodash';

export default (req: any) => {
  const auth = store.getters['auth'];
  if (auth) {
    req.headers.Authorization = `Bearer ${auth.token.token}`;
  }

  return req;
};
