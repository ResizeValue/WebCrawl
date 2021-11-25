<script>
import axios from "axios";

export default {
  methods: {
    parseUrl: function(inputUrl) {
      return axios.post(
        "https://localhost:44390/api/Results?inputUrl=" + inputUrl
      );
    },

    getDetails: function(id) {
      return axios.get("https://localhost:44390/api/Results/Details?id=" + id);
    },

    getResults: function(curPage, pageSize) {
      return axios.get(
        "https://localhost:44390/api/Results?curPage=" +
          curPage +
          "&pageSize=" +
          pageSize
      );
    }
  }
};
</script>
