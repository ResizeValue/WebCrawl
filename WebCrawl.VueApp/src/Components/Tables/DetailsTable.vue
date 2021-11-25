<template>
  <div>
    <div v-if="results.length > 0">
      <b-table
        :fields="fields"
        :busy="isLoading"
        bordered
        :items="currentPageReuslts"
        @row-clicked="rowClicked($event)"
        :tbody-tr-class="rowClass"
      >
        <template #table-busy>
          <div class="text-center text-dark my-2 m-5">
            <b-spinner class="align-middle"></b-spinner>
            <strong>Loading...</strong>
          </div>
        </template>
      </b-table>
      <div class="mt-3 d-flex justify-content-center">
        <b-pagination
          aria-controls="results"
          @change="getPage($event)"
          v-model="currentPage"
          :total-rows="totalRows"
          :per-page="pageSize"
          first-number
          last-number
        >
        </b-pagination>
      </div>
    </div>
    <div v-else class="text-center text-dark h3 mt-5 mb-5">Nothing found</div>
  </div>
</template>

<script>
export default {
  props: ["results", "pageSize"],

  data() {
    return {
      currentPage: 1,
      currentPageReuslts: [],
      totalPages: 1,
      isLoading: true,
      fields: [
        {
          key: "url"
        },
        {
          key: "responseTime.milliseconds",
          label: "Response time"
        }
      ]
    };
  },
  methods: {
    getPage: function(curPage) {
      var startIndex = (curPage - 1) * this.pageSize;
      var endIndex = startIndex + this.pageSize;

      this.currentPageReuslts = this.results.slice(startIndex, endIndex);
      this.isLoading = false;
    },
    rowClass() {
      return "table-row-active";
    },
    rowClicked() {
      this.$emit("rowClicked");
    }
  },
  computed: {
    totalRows() {
      return this.pageSize * this.totalPages;
    },
    totalRows() {
      return this.results.length;
    }
  },
  watch: {
    results: function(newVal, oldVal) {
      console.log(newVal);
      this.getPage(1);
    }
  }
};
</script>
