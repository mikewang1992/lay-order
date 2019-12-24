<template>
  <div class="login_page">
    <div class="form_content">
      <img class="mb-2" src="@/assets/img/logo.png" alt />
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
        />
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
        />
      </div>
      <a href="#" class="btn btn_default mb-2" @click.prevent="recaptcha()">登入</a>
      <br />
      <small>
        <a href="#" class="color_gray" @click.prevent="employeeLogout()">登出</a>
      </small>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loginInfo: { id: "cynthia", Password: "cynthia" }
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
          this.$swal("登入成功", "Hello！今天也要認真上班ㄛ！", "success");
          this.$router.push({ name: "Counter" });
        } else {
          this.$swal("登入失敗", "哇勒！請重新輸入一次帳密！", "error");
        }
      });
    },
    employeeLogout() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/EmployeeLogout`;
      this.$http.get(url).then(response => {
        console.log(response);
        if (response.data === "success") {
          this.$swal("登出成功", "", "success");
          this.$router.push({ name: "EmployeeLogin" });
        }
      });
    },
    async recaptcha() {
      await this.$recaptchaLoaded();
      const token = await this.$recaptcha("login");
      console.log(token);
      const url = `${process.env.APIPATH}/Accounts/Robot`;
      const data = { hiddenToken: token };
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      this.$http.post(url, data, config).then(response => {
        console.log(response.data);
        if (response.data == "success") {
          this.login();
        } else {
          alert(response.data);
        }
      });
    }
  }
};
</script>
<style src="../assets/css/store.css" scoped></style>