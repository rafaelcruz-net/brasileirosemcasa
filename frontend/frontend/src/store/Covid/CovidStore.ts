import * as vuex from 'vuex';
import covidService from '@/services/Covid/CovidService';

const covidStore: vuex.Module<{}, {}> = {
  namespaced: true,
  state: {
  },
  mutations: {
  },
  actions: {
    getMapPerState: async ({ commit, state }) => {
      return covidService.getMapPerState();
    }

  }
};

export default covidStore;
