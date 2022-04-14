<template>
  <div>
    <div class="p-3">
      <b-button v-b-toggle="'crawl'" class="text-center h2 toggle-btn"
        >Urls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml</b-button
      >
      <b-collapse id="crawl">
        <details-table :results="crawlerLinks" :pageSize="pageSize"></details-table>
      </b-collapse>
    </div>
    <hr />
    <div class="p-3">
      <b-button v-b-toggle="'sitemap'" class="text-center h2 toggle-btn"
        >Urls FOUNDED IN SITEMAP.XML but not founded after crawling a web site</b-button
      >
      <b-collapse id="sitemap">
        <details-table :results="sitemapLinks" :pageSize="pageSize"></details-table>
      </b-collapse>
    </div>
    <hr />
    <div class="p-3">
      <b-button v-b-toggle="'time'" class="text-center h2 toggle-btn">Timing</b-button>
      <b-collapse id="time">
        <details-table :results="parsedUrlList" :pageSize="pageSize"></details-table>
      </b-collapse>
    </div>
    <div class="mt-5">
      <span>{{ parsedUrlList.length - sitemapLinks.length }}</span>
      <span>{{ parsedUrlList.length - crawlerLinks.length }}</span>
    </div>
  </div>
</template>

<script>
import Table from "../Components/Tables/DetailsTable.vue";
import ApiService from "../Services/ApiService.vue";

export default {
  data() {
    return {
      id: this.$route.params["id"],
      parsedUrlList: [],
      isLoading: true,
      pageSize: 8,
    };
  },
  methods: {
    goBackToCars() {
      this.$router.push("/");
    },
    sortByResponse(a, b) {
      if (a.responseTime.totalMilliseconds < b.responseTime.totalMilliseconds) {
        return -1;
      } else if (a.responseTime.totalMilliseconds > b.responseTime.totalMilliseconds) {
        return 1;
      }
      return 0;
    },
  },
  watch: {
    $route(toR, fromR) {
      this.id = toR.params["id"];
    },
  },
  components: {
    detailsTable: Table,
  },
  computed: {
    sitemapLinks() {
      var result = this.parsedUrlList.filter((element) => {
        if (element.isCrawlingLink == false) {
          return true;
        }
      });

      return result;
    },

    crawlerLinks() {
      var result = this.parsedUrlList.filter((element) => {
        if (element.isSitemapLink == false) return true;
      });

      return result;
    },
  },
  created() {
    ApiService.methods.getDetails(this.id).then((response) => {
      this.parsedUrlList = response.data.pages.sort(this.sortByResponse);
    });
  },
};
</script>

<style scoped>
.not-found {
  padding-top: 30px !important;
}

.toggle-btn {
  width: 100%;
}
</style>
