<template>
  <div>
    <div class="p-3">
      <h2 class="text-center">
        Urls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:
      </h2>
      <details-table
        :results="crawlerLinks"
        :pageSize="pageSize"
      ></details-table>
    </div>
    <hr />
    <div class="p-3">
      <h2 class="text-center">
        Urls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:
      </h2>
      <details-table
        :results="sitemapLinks"
        :pageSize="pageSize"
      ></details-table>
    </div>
    <hr />
    <div class="p-3">
      <h2 class="text-center">Timing:</h2>
      <details-table
        :results="parsedUrlList"
        :pageSize="pageSize"
      ></details-table>
    </div>
    <div class="mt-5"></div>
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
      pageSize: 8
    };
  },
  methods: {
    goBackToCars() {
      this.$router.push("/");
    }
  },
  watch: {
    $route(toR, fromR) {
      this.id = toR.params["id"];
    }
  },
  components: {
    detailsTable: Table
  },
  computed: {
    sitemapLinks() {
      var result = this.parsedUrlList.filter(element => {
        console.log(element);
        if (element.isCrawlingLink == false) {
          return true;
        }
      });

      return result;
    },

    crawlerLinks() {
      var result = this.parsedUrlList.filter(element => {
        if (element.isSitemapLink == false) return true;
      });

      return result;
    }
  },
  created() {
    ApiService.methods.getDetails(this.id).then(response => {
      this.parsedUrlList = response.data.pages;
    });
  }
};
</script>

<style scoped>
.not-found {
  padding-top: 30px !important;
}
</style>
