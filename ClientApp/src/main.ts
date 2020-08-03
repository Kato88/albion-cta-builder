import Vue from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
import store from "./store";
import * as signalR from "@microsoft/signalr";
import vuetify from "./plugins/vuetify";
import axios from 'axios'

Vue.config.productionTip = false;

new Vue({
  router,
  store: store.original,
  vuetify,
  render: h => h(App)
}).$mount("#app");
