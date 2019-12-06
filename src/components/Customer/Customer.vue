<template>
  <div>
    <h1>Customer</h1>
  </div>
</template>
<script>
export default {
  data() {
    return {
      isOpen: ""
    };
  },
  methods: {
    checkOpen() {
      const vm = this;
      const url = `${process.env.APIPATH}/Company/IsOpen`;
      this.$http.get(url).then(response => {
        console.log(response);
      });
    }
    // editMemberInfo() {
    //   const vm = this;
    //   const url = `${process.env.APIPATH}/Accounts/Edit`;
    //   const data = vm.memberInfo;
    //   const config = {
    //     headers: {
    //       "Content-Type": "application/json"
    //     }
    //   };
    //   console.log(data);
    //   this.$http.post(url, data, config).then(response => {
    //     console.log(response);
    //   });
    // }
  },
  created() {
    this.checkOpen();
  }
};
</script>

<style scope>
#app > div {
  background: red;
}
</style>