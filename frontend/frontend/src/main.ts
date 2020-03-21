import Vue from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import Vuelidate from 'vuelidate';
import VueAxiosPlugin from '@/services/Axios/axios';
import VueMaterial from 'vue-material';
import 'vue-material/dist/vue-material.min.css';
import 'vue-material/dist/theme/default-dark.css';
import HighchartsVue from 'highcharts-vue';
import Highcharts from 'highcharts';
import mapInit from 'highcharts/modules/map';
import addBrazilMap from './map/brazil';
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

mapInit(Highcharts);
addBrazilMap(Highcharts);
Vue.use(HighchartsVue);

Vue.use(VueMaterial);
Vue.use(VueAxiosPlugin, { config: process.env.VUE_APP_ROOT_API });
Vue.use(Vuelidate);
Vue.use(VueSweetalert2);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
