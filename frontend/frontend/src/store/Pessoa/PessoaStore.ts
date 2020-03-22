import * as vuex from 'vuex';
import pessoaService from '@/services/Pessoa/PessoaService';
import Estado from '@/model/Estado';
import Pessoa from '@/model/Pessoa';

const pessoaStore: vuex.Module<{}, {}> = {
  namespaced: true,
  state: {
  },
  mutations: {
  },
  actions: {
    savePessoa: async ({ commit, state }, pessoa: Pessoa) => {
      return pessoaService.postPessoa(pessoa);
    },
    getCounter: async ({ commit, state }) => {
      return pessoaService.getCounter();
    },
    getMap: async ({ commit, state }) => {
      return pessoaService.getMap();
    },
    getCounterByStateAndMonth: async ({ commit, state }) => {
      return pessoaService.getCounterByStateAndMonth();
    }
  }
};

export default pessoaStore;
