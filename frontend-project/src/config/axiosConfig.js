import axios from 'axios';
import router from '@/router';
import Swal from 'sweetalert2'

axios.defaults.baseURL = process.env.VUE_APP_API_BASE_URL;

// Interceptor de Request
axios.interceptors.request.use(config => {
  const token = localStorage.getItem('jwt_token');
  if (token) {
    config.headers['Authorization'] = 'Bearer ' + token;
  }
  return config;
}, error => {
  return Promise.reject(error);
});

// Interceptor de Response
axios.interceptors.response.use(response => {
  return response;
}, error => {
  if (error.response.status === 401) {
    localStorage.removeItem('jwt_token');
    Swal.fire({
      title: 'Falha na operação',
      text: "Você precisa estar logado!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ir para o login',
      reverseButtons: true
  }).then((result) => {
      if (result.isConfirmed) {
        router.push('/login');
      }
  });
  } else if (error.response.status === 400) {
    console.log(error)
    const notifications = error.response.data.notifications;
    notifications.forEach(notification => {
      Swal.fire({
        title: notification.key,
        text: notification.message,
        icon: 'error',
        timer: 900
      })
    });
  }
  return Promise.reject(error);
});

export default axios;
