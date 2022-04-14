import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import router from './routes'
import Axios from 'axios'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

Axios.defaults.baseURL = "https://localhost:44390/"

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(VueRouter)
Vue.use(Axios)

new Vue({
  el: '#app',
  render: h => h(App),
  router,

})
