<template>
  <v-container fluid class="fill-height" justify-center align-center>
    <v-col cols="12" sm="8" md="4">
      <v-card>
        <v-card-title>Login</v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="valid" lazy-validation>
            <v-text-field label="Usuário" v-model="username" :rules="nameRules" required></v-text-field>

            <v-text-field label="Senha" type="password" v-model="password" :rules="nameRules" required></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn @click="handleLogin" :disabled="!valid">Entrar</v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-container>
</template>
  
<script>
import axios from 'axios';
import router from '@/router'; // Ajuste o caminho conforme sua configuração

export default {
  data() {
    return {
      valid: true,
      username: '',
      password: '',
      nameRules: [
        v => !!v || 'Campo obrigatório',
        v => (v && v.length >= 3) || 'Mínimo 3 caracteres',
      ]
    };
  },
  methods: {
    async handleLogin() {
      if (this.$refs.form.validate()) {
        try {
          const response = await axios.post('/v1/auth', {
            login: this.username,
            password: this.password
          });

          localStorage.setItem('jwt_token', response.data.token);
          router.push('/home/store-transactions');
        } catch (error) {
          console.error('Erro ao tentar logar:', error);
        }
      }
    }
  }
};
</script>
  