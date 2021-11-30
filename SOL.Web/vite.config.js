import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  base: "./", // Base address.
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"), // for @
    },
  },
  server: {
    port: 4000, // Default port.
    open: true,
    proxy: {
      "/api/v1": "http://123.56.85.24:5000", // Web api.
    },
    cors: true,
  },
});
