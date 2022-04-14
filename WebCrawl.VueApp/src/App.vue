<template>
  <div>
    <vue-header> </vue-header>
    <div class="container">
      <router-view></router-view>
    </div>
    <vue-footer> </vue-footer>
  </div>
</template>

<script>
import Header from "./Components/Header.vue";
import Footer from "./Components/Footer.vue";

export default {
  name: "app",
  data() {
    return {};
  },
  components: {
    vueHeader: Header,
    vueFooter: Footer
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

.table-row-active {
  cursor: pointer;
  transition: 0.4s !important;
}
.table-row-active:hover {
  background-color: rgb(44, 44, 44, 25%) !important;
  transition: 0.4s !important;
}

h1,
h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}

tbody,
td {
  padding: 15px !important;
}
.page-link {
  background-color: rgb(32, 32, 32) !important;
  color: white;
}

.inputUrl {
  height: 40px;
  align-self: center;
}

.page-item.active .page-link {
  z-index: 3;
  color: rgb(255, 255, 255) !important;
  background-color: #41768b !important;
  border-color: #494949 !important;
}

.page-link:focus {
  z-index: 3;
  color: #1b1b1b !important;
  background-color: #41768b !important;
  outline: 0;
  box-shadow: 0 0 0 0.25rem rgb(44, 44, 44, 25%) !important;
}
</style>
