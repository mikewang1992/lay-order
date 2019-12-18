<template>
  <div class="main login_content col-lg-4 col-md-6">
    <div class="content">
      <img src="@/assets/img/logo.png" alt />
      <ul class="nav_group mb-3">
        <li>
          <router-link to="/login" class="active">登入</router-link>
        </li>
        <li>
          <router-link to="/register">註冊</router-link>
        </li>
      </ul>

      <div class="form-group" v-if="!vertifyAppear">
        <label class="sr-only" for="phone">電話</label>
        <span class="iconfont icon-Mobile"></span>
        <input
          class="form-control"
          type="text"
          id="phone"
          placeholder="電話"
          autocomplete="off"
          v-model="loginInfo.Tel"
          maxlength="10"
        />
      </div>
      <div class="form-group mb-4" v-if="!vertifyAppear">
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
      <a
        href="index.html"
        class="btn btn_default mb-2"
        @click.prevent="login"
        v-if="!vertifyAppear"
      >登入</a>
      <!--  -->
      <div class="input-group" v-if="vertifyAppear">
        <input
          type="text"
          class="form-control"
          placeholder="電話"
          v-model="loginInfo.Tel"
          maxlength="10"
        />
        <span class="iconfont icon-Mobile"></span>
        <div class="input-group-append">
          <a href="#" class="btn" id @click.prevent="ReSendSMS()">重寄驗證碼</a>
        </div>
      </div>
      <div class="form-group" v-if="vertifyAppear">
        <label class="sr-only" for="phone">重寄驗證碼簡訊</label>
        <span class="iconfont icon-message"></span>
        <input
          class="form-control"
          type="text"
          id
          placeholder="請輸入簡訊驗證碼"
          autocomplete="off"
          maxlength="6"
          v-model="vertifyCode"
        />
      </div>
      <a class="btn btn_default mb-2" @click.prevent="vertify()" v-if="vertifyAppear">驗證手機</a>
      <!--  -->
      <br />
      <small>
        <a href="#">忘記密碼</a>
      </small>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      loginInfo: {},
      vertifyAppear: false,
      vertifyCode: "",
      footerNumber: 0
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
              this.$swal("登入成功", "歡迎一起躺著點！", "success");
              this.$router.push({ name: "Member" });
            } else if (response.data == "此電話號碼尚未進行驗證") {
              this.$swal(response.data, "請驗證您的電話號碼！", "warning");
              vm.vertifyAppear = true;
            } else {
              this.$swal("哎呦！登入失敗", response.data, "warning");
            }
          });
        } else {
          this.$swal("哎呀！請輸入正確手機格式", "", "warning");
        }
      } else {
        this.$swal("哎呀！手機長度不符", "", "warning");
      }
    },
    ReSendSMS() {
      const vm = this;
      const params = vm.loginInfo.Tel;
      const url = `${process.env.APIPATH}/Accounts/ReSendSMS?Tel=${params}`;
      this.$http.get(url).then(response => {
        console.log(response);
        if (response.data === "已寄發3次驗證碼，請您再次確認電話是否正確") {
          this.$swal(response.data, "", "warning");
        } else if (response.data === "success") {
          this.$swal(response.data, "", "info");
        }
      });
    },
    vertify() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Vertify`;
      const data = { Tel: vm.loginInfo.Tel, Vertify: vm.vertifyCode };
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      this.$http.post(url, data, config).then(response => {
        console.log(response);
        if (
          response.data === "驗證碼輸入失敗3次，請重新取得驗證碼" ||
          response.data === "驗證失敗，請重新輸入"
        ) {
          this.$swal("驗證失敗，請重新輸入", "QQ", "warning");
          vm.vertifyCode = "";
        } else {
          this.$swal("驗證成功", "YA", "success");
          vm.vertifyAppear = false;
        }
      });
    },
    checkFooterCart() {
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
      }
    }
  },
  created() {
    this.checkFooterCart();
  }
};
</script>

<style src="../../../assets/css/all.css" scoped></style>