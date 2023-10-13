<template>
    <v-container fluid>
        <v-app-bar app color="primary" dark>
            <v-btn icon @click="drawer = !drawer">
            <v-icon>mdi-menu</v-icon>
           </v-btn>
          <v-toolbar-title>Extrato Cnab</v-toolbar-title>
      
        </v-app-bar>
  
      <v-navigation-drawer v-model="drawer" app>
        <v-list dense>
          <v-list-item-group v-model="selected">
            <v-list-item to="/home/store-transactions">
              <v-list-item-content>
                <v-list-item-title>Transações por loja</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
  
            <v-list-item to="/home/general-transactions">
              <v-list-item-content>
                <v-list-item-title>Transações Geral</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
  
            <v-list-item to="/home/file-upload">
              <v-list-item-content>
                <v-list-item-title>Upload de Arquivo</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-item-group>
        </v-list>
      </v-navigation-drawer>
  
      <v-main>
        <router-view></router-view>
      </v-main>
    </v-container>
  </template>
  
  <script>
  export default {
    data() {
      return {
        selected: null,
        drawer: false 
      };
    },
    methods: {
    
    }
  };
  </script>
  