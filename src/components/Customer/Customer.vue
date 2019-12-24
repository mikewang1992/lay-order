<template>
  <div class="page">
    <Footer :forhere="forhere" :tableNum="tableNum"></Footer>
    <router-view :forhere="forhere" :isOpenFromCustomer="isOpen"></router-view>
  </div>
</template>

<script>
import Footer from "./Footer";
export default {
  data() {
    return {
      isOpen: true,
      forhere: false,
      tableNum: ""
    };
  },
  methods: {
    checkOpen() {
      const vm = this;
      const url = `${process.env.APIPATH}/Company/IsOpen`;
      this.$http.get(url).then(response => {
        // console.log('是否為營業時間',response.data);s
        vm.isOpen = response.data;
        if (response.data == "no") {
          vm.isOpen = false;
        } else {
          vm.isOpen = true;
        }
      });
    },
    checkTable() {
      const url = `${process.env.APIPATH}/Accounts/IsTable`;
      this.$http.get(url).then(response => {
        // console.log("確認內用還外帶：", response.data);
        if (response.data === "內用") {
          this.forhere = true;
          const vm = this;
          const url = `${process.env.APIPATH}/Accounts/OrderMember`;
          this.$http.get(url).then(response => {
            this.tableNum = response.data.split(",")[1][0];
          });
        } else {
          this.forhere = false;
        }
      });
    }
  },
  watch: {
    $route(to, from) {
      this.checkTable();
    }
  },
  created() {
    this.checkOpen();
    this.checkTable();
  },
  components: {
    Footer
  }
};
</script>

<style src="../../assets/css/all.css" scope></style>