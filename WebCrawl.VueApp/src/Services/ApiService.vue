<script>
import axios from "axios";

export default {
  methods: {
    parseUrl: function(inputUrl) {
      return axios.post(
        axios.defaults.baseURL + "api/Results?inputUrl=" + inputUrl
      );
    },

    getDetails: function(id) {
      return axios.get(axios.defaults.baseURL + "api/Results/Details?id=" + id);
    },

    getResults: function(curPage, pageSize) {
      return axios.get(
        axios.defaults.baseURL +
          "api/Results?curPage=" +
          curPage +
          "&pageSize=" +
          pageSize
      );
    }
  }
};
</script>
