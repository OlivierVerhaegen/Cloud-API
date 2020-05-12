import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    isLoggedIn: false,
    googleAccount: null,
    account: null
  },
  mutations: {
    LOGIN: (state, googleAccount) => {
      state.isLoggedIn = true;
      state.googleAccount = googleAccount;
    },
    SET_ACCOUNT: (state, account) => {
      state.account = account;
    }
  },
  actions: {
    login: async ({commit}, googleAccount) => {
      let res = await fetch("https://" + window.location.hostname + ":44369/api/v1/users?email=" + googleAccount.Pt.yu);
      let account = await res.json();

      if (account.length > 0) {
        account = account[0];
      } else {
        let res = await fetch("https://" + window.location.hostname + ":44369/api/v1/users",
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            name: googleAccount.Pt.Ad,
            email: googleAccount.Pt.yu
          }),
        });

        account = await res.json();
      }
      commit('SET_ACCOUNT', account)
      commit('LOGIN', googleAccount)
    }
  },
  modules: {
  }
})
