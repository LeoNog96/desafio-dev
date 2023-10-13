<template>
  <div>
    <v-row v-if="!selectedStore">
      <v-col>
        <h1>Transações por lojas</h1>
      </v-col>
    </v-row>
    <v-row>
      <v-col v-if="selectedStore">
        <v-btn @click="selectedStore = null">Voltar</v-btn>
      </v-col>
      <v-col>
        <v-btn @click="toUpload">Adicionar Arquivo</v-btn>
      </v-col>
    </v-row>

    <v-row v-if="!selectedStore">
      <v-col v-for="store in stores" :key="store.storeName" cols="12" md="4">
        <v-card @click="selectStore(store)">
          <v-card-title>{{ store.storeName }}</v-card-title>
          <v-card-subtitle>{{ store.storeOwner }}</v-card-subtitle>
        </v-card>
      </v-col>
    </v-row>

    <!-- Transações de uma loja selecionada -->
    <div v-if="selectedStore">
      <v-row>
        <v-col>
          <h2>{{ selectedStore.storeName }} - {{ selectedStore.storeOwner }}</h2>
          <p>Saldo: {{ selectedStore.balance }}</p>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-data-table :headers="headers" :items="selectedStore.transaction"></v-data-table>
        </v-col>
      </v-row>
    </div>
  </div>
</template>
  
<script>
import axios from 'axios';
import router from '@/router';
export default {
  data() {
    return {
      stores: [],
      selectedStore: null,
      headers: [
        { text: 'Tipo', value: 'type' },
        { text: 'Data', value: 'date' },
        { text: 'Valor', value: 'value' },
        { text: 'CPF', value: 'cpf' },
        { text: 'Cartão', value: 'card' },
        { text: 'Enviado por', value: 'uploadedBy' },
        { text: 'Dono da loja', value: 'storeOwner' },
        { text: 'Nome da loja', value: 'storeName' },
      ]
    };
  },
  async mounted() {
    try {
      const response = await axios.get('/v1/transactions/per-store');
      this.stores = response.data;
    } catch (error) {
      console.error('Erro ao buscar dados das lojas:', error);
    }
  },
  methods: {
    selectStore(store) {
      this.selectedStore = store;
    },
    toUpload() {
      router.push("/home/file-upload")
    }
  }
}
</script>
  