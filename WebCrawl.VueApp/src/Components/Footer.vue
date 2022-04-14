<template>
  <footer>
    <span class="text-light copy">© {{ copy }}</span>
  </footer>
</template>

<script>
export default {
  data() {
    return {
      copy: "Webcrawler 2021 - Vue"
    };
  }
};
</script>

<style scoped>
.copy {
  font-size: 16px;
}

footer {
  z-index: 50;
  display: flex;
  padding-left: 40px !important;
  padding: 10px;
  align-items: center;
  position: fixed !important;
  left: 0 !important;
  right: 0 !important;
  bottom: 0 !important;
  background-color: #202020;
}
</style>
