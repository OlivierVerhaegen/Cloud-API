<template>
  <div id="app">
    <nav id="nav">
      <div>
        <router-link to="/">Home</router-link> |
        <router-link to="/exercises">Exercises</router-link> |
        <router-link to="/bodyparts">Body parts</router-link> |
        <router-link to="/myexercises">Add exercises</router-link> |
        <router-link to="/mybodyparts">Add body parts</router-link>
      </div>
      <p v-if="googleAccount != null" >Logged in as: {{ googleAccount.Pt.Ad }}</p>
    </nav>
    <router-view/>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'App',
  data:() => ({

  }),
  computed: {
    ...mapState([
      'isLoggedIn',
      'googleAccount'
    ]),
  },
  mounted() {
  },
  methods: {
  }
}
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

#nav {
  display: flex;
  justify-content: space-between;
  padding: 1rem;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: RGB(var(--vs-primary));
    }
  }
}
</style>
