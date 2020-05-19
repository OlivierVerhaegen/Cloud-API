import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Exercises from '../views/Exercises.vue';
import BodyParts from '../views/BodyParts.vue';
import MyExercises from '../views/MyExercises.vue';
import MyBodyParts from '../views/MyBodyParts.vue';
import CaloriesFinder from '../views/CaloriesFinder.vue';

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
  {
    path: '/mybodyparts',
    name: 'MyBodyParts',
    component: MyBodyParts
  },
  {
    path: '/calories-finder',
    name: 'Calories Finder',
    component: CaloriesFinder
  }
]

const router = new VueRouter({
  routes
})

export default router
