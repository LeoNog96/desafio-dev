<template>
  <div>
    <v-row>
      <v-col>
        <h1>Transações gerais</h1>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-btn @click="toHome">Voltar</v-btn>
      </v-col>
      <v-col>
        <v-btn @click="toUpload">Adicionar Arquivo</v-btn>
      </v-col>
    </v-row>
    <v-data-table :headers="headers" :items="transactions" :page.sync="pageNumber" :items-per-page="pageSize"
      :footer-props="{ 'items-per-page-options': [10, 20, 50] }" @update:page="fetchData"></v-data-table>

    <v-pagination v-if="pageTotal > 0" v-model="pageNumber" :length="pageTotal" @input="fetchData"></v-pagination>
  </div>
</template>
  
<script>
import axios from 'axios';
import router from '@/router';
export default {
  data() {
    return {
      transactions: [],
      pageNumber: 1,
      pageSize: 10,
      pageTotal: 0,
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
  async created() {
    await this.fetchData();
  },
  methods: {
    async fetchData() {
      try {
        const response = await axios.get(`/v1/transactions?PageNumber=${this.pageNumber}&PageSize=${this.pageSize}`);
        this.transactions = response.data.data;
        this.pageTotal = response.data.pageTotal;
      } catch (error) {
        console.error('Erro ao buscar transações gerais:', error);
      }
    },
    toUpload() {
      router.push("/home/file-upload")
    },
    toHome() {
      router.push("/home/store-transactions")
    }
  }
}
</script>
  