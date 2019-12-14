<template>
  <div class="login_page">
    <div class="form_content">
      <img class="mb-2" src="@/assets/img/logo_nav.png" alt>
      <div class="form-group">
        <label class="sr-only" for="userName">帳號</label>
        <span class="iconfont icon-user"></span>
        <input
          class="form-control"
          type="text"
          placeholder="請輸入帳號"
          autocomplete="off"
          id="userName"
          v-model="loginInfo.id"
        >
      </div>
      <div class="form-group mb-3">
        <label class="sr-only" for="password">密碼</label>
        <span class="iconfont icon-lock"></span>
        <input
          class="form-control"
          type="password"
          placeholder="請輸入密碼"
          autocomplete="off"
          id="password"
          v-model="loginInfo.Password"
        >
      </div>
      <a href="#" class="btn btn_default mb-2" @click.prevent="login">登入</a>
    </div>
  </div>
</template>
<style scope>
@import "../assets/css/store.css";
</style>

<script>
export default {
  data() {
    return {
      loginInfo: {id:'cynthia',Password:'cynthia'}
    };
  },
  methods: {
    login() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/EmployeeLogin`;
      const data = vm.loginInfo;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      this.$http.post(url, data, config).then(response => {
        console.log(response.data);
        if (response.data == "success") {
          alert('登入成功！');
          this.$router.push({ name: "Counter" });
        }else {
          alert('登入失敗，請重新輸入！');
        }
      });
    }
  }
};
</script>