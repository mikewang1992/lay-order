<template>
  <div>
    <h1>Login</h1>
    <input type="text" placeholder="Tel" v-model="loginInfo.Tel" />
    <input type="text" placeholder="Password" v-model="loginInfo.Password" />
    <button @click.prevent="login">登入</button>
    <button @click.prevent="ReSendSMS" v-if="resendAppear">重新發送認證簡訊</button>
    <div v-if="vertifyAppear">
      <input type="text" placeholder="Vertify" v-model="vertifyCode" />
      <button @click.prevent="vertify">17.Vertify確認驗證碼</button>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      loginInfo: {},
      resendAppear: false,
      vertifyAppear: false,
      vertifyCode: ""
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
        } else if (response.data == "此電話號碼尚未進行驗證") {
          alert(response.data);
          vm.resendAppear = true;
        } else {
          alert(response.data);
        }
      });
    },
    ReSendSMS() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/ReSendSMS`;
      this.$http.get(url).then(response => {
        console.log(response);
        alert(response.data);
        vm.vertifyAppear = true;
      });
    },
    vertify() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Vertify`;
      const data = vm.vertifyCode;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      this.$http.post(url, data, config).then(response => {
        console.log(response);
        alert(response.data);
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