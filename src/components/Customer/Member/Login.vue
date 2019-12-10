<template>
  <div class="page login">
    <div class="main col-lg-4 col-md-6">
      <div class="content">
        <img src="img/logo.png" alt />
        <ul class="nav_group mb-3">
          <li>
            <a href="login.html" class="active">登入</a>
          </li>
          <li>
            <a href="register.html">註冊</a>
          </li>
        </ul>

        <div class="form-group">
          <label class="sr-only" for="phone">電話</label>
          <span class="iconfont icon-Mobile"></span>
          <input
            class="form-control"
            type="text"
            id="phone"
            placeholder="電話"
            autocomplete="off"
            v-model="loginInfo.Tel"
          />
        </div>
        <div class="form-group mb-4">
          <label class="sr-only" for="phone">密碼</label>
          <span class="iconfont icon-lock"></span>
          <input
            class="form-control"
            type="password"
            id="password"
            placeholder="密碼"
            autocomplete="off"
            v-model="loginInfo.Password"
          />
        </div>
        <a href="index.html" class="btn btn_default mb-2" @click.prevent="login">登入</a>
        <br />
        <small>
          <a href="#">忘記密碼</a>
        </small>
      </div>
    </div>
  </div>
  <!-- <h1>Login</h1>
    <input type="text" placeholder="Tel" v-model="loginInfo.Tel" />
    <input type="text" placeholder="Password" v-model="loginInfo.Password" />
    <button @click.prevent="login">登入</button>
    <button @click.prevent="ReSendSMS" v-if="resendAppear">重新發送認證簡訊</button>
    <div v-if="vertifyAppear">
      <input type="text" placeholder="Vertify" v-model="vertifyCode" />
      <button @click.prevent="vertify(loginInfo.Tel,vertifyCode)">17.Vertify確認驗證碼</button>
  </div>-->
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
        if (response.data !== "已寄發3次驗證碼，請您再次確認電話是否正確") {
          vm.vertifyAppear = true;
        }
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
        if (response.data !== "驗證失敗，請重新輸入") {
          alert("驗證成功");
        } else {
          alert("驗證失敗，請重新輸入");
        }
      });
    }
  }
};
</script>

<style scope>
</style>