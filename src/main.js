// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.config.productionTip = false
axios.defaults.withCredentials = true;

Vue.use(VueAxios, axios)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})


//router auth
router.beforeEach((to, from, next) => {
  // console.log(to, from, next)
  if (to.meta.requiresLogin) {
    const api = `${process.env.APIPATH}/Accounts/CheckLogin`;
    axios.get(api).then(response => {
      console.log(response.data);
      if (response.data == 'True') {
        next()
      } else {
        alert("請先登入")
        next({ path: '/login' })
      }
    });
  }
  else {
    next();
  }

})