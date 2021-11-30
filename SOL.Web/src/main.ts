/**
 * beforeCreate -> setup()
 * created -> setup()
 * beforeMount -> onBeforeMount
 * mounted -> onMounted
 * beforeUpdate -> onBeforeUpdate
 * updated -> onUpdated
 * beforeDestroy -> onBeforeUnmount
 * destroyed -> onUnmounted
 * errorCaptured -> onErrorCaptured
 */
import "reflect-metadata"; // try delete.
import { createApp } from "vue";
import App from "./App.vue";
import { setup } from "./app/ioc/modules";

import router from "./app/router";

const app = createApp(App);

app.use(router);

setup();

router.isReady().then(() => {
  app.mount("#app");
});
