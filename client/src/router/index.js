import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Exercises from '../views/Exercises.vue';

Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: '/exercises',
    name: 'Exercises',
    component: Exercises
  }
]

const router = new VueRouter({
  routes
})

export default router
