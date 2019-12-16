<template>
  <div class="page">
    <Footer></Footer>
    <router-view :isOpenFromCustomer="isOpen"></router-view>
  </div>
</template>

<script>
import Footer from './Footer';
export default {
  data() {
    return {
      isOpen: false
    };
  },
  methods: {
    checkOpen() {
      const vm = this;
      const url = `${process.env.APIPATH}/Company/IsOpen`;
      this.$http.get(url).then(response => {
        console.log('是否為營業時間',response);
        vm.isOpen = response.data;
        if (response.data == "no") {
          vm.isOpen = true;
        }
      });
    }
  },
  created() {
    this.checkOpen();
  },
  components: {
    Footer
  }
};
</script>

<style scope>
@import "../../assets/css/all.css";
</style>