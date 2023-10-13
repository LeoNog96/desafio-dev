// src/router.js

import Vue from 'vue';
import Router from 'vue-router';
import LoginPage from '@/views/LoginPage.vue';
import HomePage from '@/views/HomePage.vue';
import StoreTransactions from '@/components/StoreTransactions.vue';
import GeneralTransactions from '@/components/GeneralTransactions.vue';
import UploadFile from '@/components/UploadFile.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      redirect: '/login'
    },
    {
      path: '/login',
      component: LoginPage
    },
    {
      path: '/home',
      component: HomePage,
      children: [
        {
          path: 'store-transactions',
          component: StoreTransactions
        },
        {
          path: 'general-transactions',
          component: GeneralTransactions
        },
        {
          path: 'file-upload',
          component: UploadFile
        }
      ]
    }
  ]
});
