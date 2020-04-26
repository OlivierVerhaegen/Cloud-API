<template>
  <div class="login">
      <vs-dialog prevent-close  v-model="showDialog">
        <template #header>
          <h4 class="not-margin">
            Welcome to <b>Olivier's API</b>
          </h4>
        </template>


        <div class="con-form">
          <vs-input v-if="register" v-model="name" placeholder="Name">
            <template #icon>
              <i class='bx bxs-user'></i>
            </template>
          </vs-input>
          <vs-input v-model="email" placeholder="Email">
            <template #icon>
              <b>@</b>
            </template>
          </vs-input>
          <vs-input type="password" v-model="password" placeholder="Password">
            <template #icon>
              <!-- <box-icon name="lock"></box-icon> -->
              <i class='bx bxs-lock'></i>
            </template>
          </vs-input>
          <vs-input v-if="register" type="password" v-model="passwordConfirm" placeholder="Confirm password">
            <template #icon>
              <!-- <box-icon name="lock"></box-icon> -->
              <i class='bx bxs-lock'></i>
            </template>
          </vs-input>
        </div>

        <template #footer>
          <div class="footer-dialog">
            <vs-button block v-if="!register">
              Sign In
            </vs-button>

            <vs-button block v-if="register">
              Register
            </vs-button>

            <div v-if="!register" class="new">
              New Here? <a href="#" @click="register = !register">Create New Account</a>
            </div>

            <div v-if="register" class="new">
              Already have an account? <a href="#" @click="register = !register">Sign in</a>
            </div>
          </div>
        </template>
      </vs-dialog>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'Login',
  data:() => ({
    showDialog: true,
    email: '',
    password: '',
    register: false,
  }),
  mounted() {
  }
}
</script>

<style lang="scss">
.not-margin {
  margin: 0;
  font-weight: normal;
  padding: 10px;
}

.con-form {
  width: 100%;

  .flex {
    display: flex;
    align-items: center;
    justify-content: space-between;

    a {
      font-size: .8rem;
      opacity: .7;
      &:hover {
        opacity: 1;
      }
    }
  }

  .vs-checkbox-label {
    font-size: .8rem;
  }

  .vs-input-label {
    margin: 10px 8px;
    width: 100%;
  }

  .vs-input-content {
    margin: 10px 0;
    width: 100%;

    .vs-input {
      width: 100%;
    }
  }
}

.footer-dialog {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  width: 100%;

  .new {
    margin: 0;
    margin-top: 20px;
    padding: 0;
    font-size: .7rem;

    a {
      color: --vs-primary !important;
      margin-left: 6px;

      &:hover {
        text-decoration: underline;
      }
    }
  }

  .vs-button {
    margin: 0;
  }
}
</style>