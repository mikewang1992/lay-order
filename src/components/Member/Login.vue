<template>
  <div>
    <h1>Login</h1>
    <input type="text" placeholder="Tel" v-model="loginInfo.Tel" />
    <input type="text" placeholder="Password" v-model="loginInfo.Password" />
    <button @click.prevent="login">登入</button>
  </div>
</template>
<script>
export default {
  data() {
    return {
      loginInfo: {}
    };
  },
  methods: {
    login() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Login`;
      const data = vm.loginInfo;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      this.$http.post(url, data, config).then(response => {
        console.log(response);
        if (response.data == "success") {
          alert(response.data);
          this.$router.push({ name: "Member" });
        } else {
          alert(response.data);
        }
      });
    }
  }
};
</script>

<style scope>
#app > div {
  background: red;
}
</style>