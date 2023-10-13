<template>
  <div>
    <v-row>
      <v-col>
        <h1>Upload de arquivo CNAB</h1>
      </v-col>
    </v-row>
    <v-row>
      <v-form @submit.prevent="uploadFile">
        <v-file-input v-model="file" label="Upload de Arquivo"></v-file-input>
        <v-btn type="submit">Upload</v-btn>
      </v-form>
    </v-row>
  </div>
</template>
  
<script>
import axios from 'axios';
import router from '@/router';

export default {
  data() {
    return {
      file: null
    };
  },
  methods: {
    async uploadFile() {
      let formData = new FormData();
      formData.append('file', this.file);
      try {
        await axios.post('/v1/cnab', formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        });
        router.push('/home/store-transactions');
      } catch (error) {
        console.error('Erro ao fazer upload:', error);
      }
    }
  }
}
</script>
  