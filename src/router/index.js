// vue
import Vue from 'vue'
import Router from 'vue-router'
//Member
import Login from '@/components/Member/Login'
import Member from '@/components/Member/Member'
import Register from '@/components/Member/Register'
//Cusomter
import Customer from '@/components/Customer/Customer'
//Counter
import Counter from '@/components/Counter/Counter'
//Kitchen
import Kitchen from '@/components/Kitchen/Kitchen'



Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '*',
      redirect: '/',
    },
    {
      path: '/register',
      name: 'Register',
      component: Register
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/member',
      name: 'Member',
      component: Member,
      meta: {
        requiresLogin: true
      }
    },
    {
      path: '/',
      name: 'Customer',
      component: Customer
    },
    {
      path: '/counter',
      name: 'Counter',
      component: Counter
    },
    {
      path: '/kitchen',
      name: 'Kitchen',
      component: Kitchen
    },
  ]
})
