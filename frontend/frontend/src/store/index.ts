import Vue from 'vue';
import Vuex from 'vuex';
import estadoStore from '@/store/Estado/EstadoStore';
import pessoaStore from './Pessoa/PessoaStore';
import covidStore from './Covid/CovidStore';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    'estado': estadoStore,
    'pessoa': pessoaStore,
    'covid': covidStore
  }
});
