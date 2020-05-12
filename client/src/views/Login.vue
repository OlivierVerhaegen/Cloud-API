<template>
  <div class="login">
      <vs-dialog prevent-close  v-model="showDialog">
        <template #header>
          <h4 class="not-margin">
            Welcome to <b>Olivier's API</b>
          </h4>
        </template>

        <GoogleLogin :renderParams="renderParams" :params="params" :onSuccess="onSuccess" :onFailure="onFailure">Login</GoogleLogin>

        
      </vs-dialog>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import GoogleLogin from 'vue-google-login';
import { mapActions } from 'vuex';

export default {
  name: 'Login',
  components: {
    GoogleLogin
  },
  data:() => ({
    showDialog: true,
    params: {
      client_id: '438795392975-jr28jkqv4iollb78ctsf2764if8aq4le.apps.googleusercontent.com',
    },
    renderParams: {
      width: 250,
      height: 50,
      longTitle: true
    }
  }),
  mounted() {
  },
  methods: {
    ...mapActions([
      'login'
    ]),
    async onSuccess(googleUser) {
      console.log(googleUser);
      this.showDialog = false;
      this.login(googleUser)
    },
    onFailure(err) {
      console.log(err);
    },
  }
}
</script>

<style lang="scss">
.margin-bottom {
  margin-bottom: 1rem;
}

.not-margin {
  margin: 0;
  font-weight: normal;
  padding: 10px;
}

.vs-dialog .vs-dialog__content {
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>