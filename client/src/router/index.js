import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Exercises from '../views/Exercises.vue';
import BodyParts from '../views/BodyParts.vue';
import MyExercises from '../views/MyExercises.vue';

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
  },
  {
    path: '/bodyparts',
    name: 'Bodyparts',
    component: BodyParts
  },
  {
    path: '/myexercises',
    name: 'MyExercises',
    component: MyExercises
  },
]

const router = new VueRouter({
  routes
})

export default router
