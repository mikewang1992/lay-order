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
// VueTimepicker
import VueTimepicker from 'vue2-timepicker'
import 'vue2-timepicker/dist/VueTimepicker.css'
// jquery
import $ from 'jquery'
//
import VueLoading from 'vue-loading-overlay'
import 'vue-loading-overlay/dist/vue-loading.css'
// import 'signalr'
const options = {
    confirmButtonColor: '#72BCB3',
    cancelButtonColor: '#9B9B9B',
};

Vue.config.productionTip = false
axios.defaults.withCredentials = true;

Vue.use(VueAxios, axios)
Vue.use(VueAwesomeSwiper)
Vue.use(VueSweetalert2, options)
Vue.use(VueLoading, {
    isFullPage: true,
    canCancel: false,
    loader: 'dots', //spinner/dots/bars
    color: '#72bcb3',
    backgroundColor: '#ffffff',
    opacity: 0.4,
    width: 48,
    height: 48
});

/* eslint-disable no-new */



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
    } else if (to.meta.requiresEmployeeLogin) {
        const api = `${process.env.APIPATH}/Kitchen/ShowOrderList?type=&&status=&&page=1`;
        axios.get(api).then(response => {
            console.log(response);
            console.log(response.data);
            if (response.data == '未登入') {
                Vue.swal({
                    toast: true,
                    position: "top-end",
                    showConfirmButton: false,
                    timer: 3000,
                    type: "info",
                    title: "請先登入唷"
                });
                next({ path: '/EmployeeLogin' })
            } else {
                next()
            }
        });
    } else {
        next();
    }

})

new Vue({
    el: '#app',
    router,
    components: { App },
    template: '<App/>'
})