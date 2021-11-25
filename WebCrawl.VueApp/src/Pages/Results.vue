<template>
  <div>
    <div class="d-flex flex-row">
      <b-form-input
        v-model="inputUrl"
        type="search"
        :state="isValid"
        @change="clearError()"
        class="inputUrl"
        placeholder="Enter the site URL"
      ></b-form-input>
      <div class="d-flex flex-row" v-if="!isParsing">
        <b-button class="m-3 bg-dark" @click="parseUrl()">Parse</b-button>
        <b-button class="m-3 bg-info" @click="getResults()">Refresh</b-button>
      </div>
      <div v-else class="d-flex flex-row align-items-center">
        <b-spinner class="m-3"></b-spinner>
        <span class="m-3">Parsing...</span>
      </div>
    </div>
    <label class="text-danger" v-show="!isValid">{{ errorMessage }}</label>
    <b-overlay :show="isParsing">
      <results-table
        :results="results"
        :totalPages="totalPages"
        :pageSize="pageSize"
        @getResult="getResults"
        @getDetails="getDetails"
      ></results-table>
    </b-overlay>
  </div>
</template>

<script>
import ApiService from "../Services/ApiService.vue";
import Table from "../Components/Tables/ResultsTable.vue";

export default {
  data() {
    return {
      results: [],
      inputUrl: "",
      isLoading: null,
      isParsing: false,
      totalPages: 0,
      pageSize: 10,
      isValid: null,
      errorMessage: ""
    };
  },
  components: {
    resultsTable: Table
  },
  methods: {
    clearError: function() {
      this.isValid = null;
      this.errorMessage = "";
    },

    parseUrl: function() {
      this.isParsing = true;

      ApiService.methods
        .parseUrl(this.inputUrl)
        .then(response => {
          this.getResults(this.currentPage);
          this.isValid = true;
        })
        .catch(error => {
          this.isValid = false;
          this.errorMessage = error.response.data.IncorrectUrl[0];
        })
        .then(() => {
          this.isParsing = false;
        });
    },

    getDetails: function(result) {
      this.$router.push("/Details/" + result.id);
    },

    getResults: function(curPage) {
      this.isLoading = true;
      ApiService.methods
        .getResults(curPage, this.pageSize)
        .then(result => {
          this.results = result.data.results;
          this.totalPages = result.data.totalPages;
        })
        .catch(error => {
          this.makeToast("Server does not response");
        })
        .then(() => {
          this.isLoading = false;
        });
    },

    makeToast(title = "Error", append = false) {
      this.$bvToast.toast(`${title}`, {
        title: "Error",
        autoHideDelay: 5000,
        appendToast: append
      });
    }
  },

  created() {
    this.getResults(1);
  },

  watch: {
    $route(toR, fromR) {
      this.id = toR.params["id"];
    }
  }
};
</script>
