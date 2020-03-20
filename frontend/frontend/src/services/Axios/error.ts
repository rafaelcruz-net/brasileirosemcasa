import Vue from 'vue';
import router from '@/router';
export default (err: any) => {
  if (err && err.response && err.response.status === 401) {
    window.localStorage.clear();
    window.location.reload();
    router.push('forbidden');
  } else if (err && err.response && err.response.status !== 404) {
    return Promise.reject(err);
  } else {
    return Promise.reject(err);
  }
};
