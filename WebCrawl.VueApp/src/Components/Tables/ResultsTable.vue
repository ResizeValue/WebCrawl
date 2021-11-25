<template>
  <div>
    <b-table
      :fields="fields"
      @row-clicked="getDetails($event)"
      :items="results"
      :tbody-tr-class="rowClass"
    >
    </b-table>
    <div class="mt-3 d-flex justify-content-center">
      <b-pagination
        aria-controls="results"
        @change="getResults($event)"
        v-model="currentPage"
        :total-rows="totalRows"
        :per-page="pageSize"
        first-number
        last-number
      >
      </b-pagination>
    </div>
  </div>
</template>

<script>
import { DateTime } from "luxon";

export default {
  props: ["results", "totalPages", "pageSize"],
  data() {
    return {
      currentPage: 1,
      fields: [
        {
          key: "basePage"
        },
        {
          key: "date",
          formatter: this.formatDate
        },
        {
          key: "pagesCount"
        }
      ]
    };
  },
  computed: {
    totalRows() {
      return this.pageSize * this.totalPages;
    }
  },
  methods: {
    rowClass() {
      return "table-row-active";
    },

    getResults(curPage) {
      console.log(curPage);
      this.$emit("getResult", curPage);
    },

    getDetails(result) {
      this.$emit("getDetails", result);
    },

    formatDate(value) {
      const formattedDate = DateTime.fromISO(value).toLocaleString(
        DateTime.DATETIME_SHORT
      );
      return formattedDate;
    }
  }
};
</script>
