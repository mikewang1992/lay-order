<template>
  <div>

    <router-view :isOpenFromCustomer="isOpen"></router-view>
  </div>
</template>

<script>

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
        console.log(response);
        vm.isOpen = response.data;
        if (response.data == "no") {
          vm.isOpen = true;
        }
      });
    }
  },
  created() {
    this.checkOpen();
  }
};
</script>

<style scope>
  @import "../../assets/css/all.css";
</style>