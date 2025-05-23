import { createApp } from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
import signalr from "./utils/signalR";

createApp(App).use(router).mount("#app");
createApp(App).use(signalr).mount("#app");

