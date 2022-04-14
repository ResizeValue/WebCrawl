<template>
  <header>
    <b-navbar variant="dark" type="dark">
      <div class="container">
        <div class="head">
          <b-navbar-brand tag="h1" class="p-3 text-light">
            <router-link class="text-light" exact to="/">{{
              title
            }}</router-link>
          </b-navbar-brand>
          <b-nav-item class="text-light">
            <router-link exact to="/">Results</router-link>
          </b-nav-item>
        </div>
      </div>
    </b-navbar>
  </header>
</template>

<script>
export default {
  data() {
    return {
      title: "WebCrawler"
    };
  }
};
</script>

<style scoped>
.head {
  position: sticky !important;
  top: 0 !important;
  left: 0;
  right: 0;
}

a {
  color: rgb(107, 189, 178);
  text-decoration: unset;
  transition: 0.4s;
}

a:hover {
  color: rgb(177, 189, 107);
  transition: 0.4s;
}
</style>
