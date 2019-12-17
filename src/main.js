// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
////axios
import axios from 'axios'
import VueAxios from 'vue-axios'
//Swiper 
import VueAwesomeSwiper from 'vue-awesome-swiper'
import 'swiper/dist/css/swiper.css'
//sweetalert2
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

const options = {
  confirmButtonColor: '#72BCB3',
  cancelButtonColor: '#9B9B9B',
};

Vue.config.productionTip = false
axios.defaults.withCredentials = true;

Vue.use(VueAxios, axios)
Vue.use(VueAwesomeSwiper)
Vue.use(VueSweetalert2,options)

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
        Vue.swal({
          toast: true,
          position: "top-end",
          showConfirmButton: false,
          timer: 3000,
          type: "info",
          title: "請先登入唷"
        });
        next({ path: '/login' })
      }
    });
  }
  else {
    next();
  }

})