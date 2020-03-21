import * as vuex from 'vuex';
import estadoService from '@/services/Estado/EstadoService';
import Estado from '@/model/Estado';

const estadoStore: vuex.Module<{}, {}> = {
  namespaced: true,
  state: {
  },
  mutations: {
  },
  actions: {
    getEstado: async ({ commit, state }) => {
      return estadoService.getEstado();
    }
  }
};

export default estadoStore;
