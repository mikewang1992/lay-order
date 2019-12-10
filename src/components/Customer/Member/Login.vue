<template>
  <div>
    <h1>Login</h1>
    <input type="text" placeholder="Tel" v-model="loginInfo.Tel" />
    <input type="text" placeholder="Password" v-model="loginInfo.Password" />
    <button @click.prevent="login">登入</button>
    <button @click.prevent="ReSendSMS" v-if="resendAppear">重新發送認證簡訊</button>
    <div v-if="vertifyAppear">
      <input type="text" placeholder="Vertify" v-model="vertifyCode" />
      <button @click.prevent="vertify(loginInfo.Tel,vertifyCode)">17.Vertify確認驗證碼</button>
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
      if (vm.loginInfo.Tel.length === 10) {
        if (vm.loginInfo.Tel.slice(0, 2) === "09") {
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
        } else {
          alert("請輸入正確手機格式");
        }
      } else {
        alert("手機長度不符");
      }
    },
    ReSendSMS() {
      const vm = this;
      const params = vm.loginInfo.Tel;
      const url = `${process.env.APIPATH}/Accounts/ReSendSMS?Tel=${params}`;
      this.$http.get(url).then(response => {
        console.log(response);
        alert(response.data);
        vm.vertifyAppear = true;
      });
    },
    vertify(tel, vertify) {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Vertify`;
      const data = { Tel: tel, Vertify: vertify };
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      this.$http.post(url, data, config).then(response => {
        console.log(response);
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