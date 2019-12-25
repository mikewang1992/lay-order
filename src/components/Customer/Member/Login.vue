<template>
  <div class="main login_content col-lg-4 col-md-6">
    <div class="content">
      <img src="@/assets/img/logo.png" alt />
      <ul class="nav_group mb-3">
        <li @click="vertifyAppear = false">
          <router-link to="/login" class="active">登入</router-link>
        </li>
        <li>
          <router-link to="/register">註冊</router-link>
        </li>
      </ul>
      <form action v-if="!vertifyAppear">
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
            maxlength="10"
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
        <a href="#" class="btn btn_default mb-2" @click.prevent="login()">登入</a>
        <br />
        <small>
          <a href="#" @click.prevent="vertifyAppear = true,FromChangePS = true">忘記密碼</a>
        </small>
      </form>
      <input type="hidden" name="hiddenToken" id="hiddenToken" />
      <!-- 忘記密碼 -->
      <form action v-if="vertifyAppear" v-show="!changePasswordAppear">
        <h3 class="mb-3 mt-2">請輸入您的手機並重新發送驗證碼進行驗證</h3>
        <div class="input-group">
          <input type="text" class="form-control mb-2" v-model="loginInfo.Tel" placeholder="電話" />
          <span class="iconfont icon-Mobile"></span>
          <div class="input-group-append">
            <a href="#" @click.prevent="ReSendSMS" class="btn" id>送出驗證碼</a>
          </div>
        </div>

        <div class="form-group">
          <span class="iconfont icon-message"></span>
          <input
            class="form-control"
            type="number"
            placeholder="請輸入簡訊驗證碼"
            autocomplete="off"
            maxlength="6"
            v-model="vertifyCode"
          />
        </div>
        <a class="btn btn_default mb-2" @click.prevent="vertify()">確認</a>
      </form>
      <!-- 新密碼 -->
      <form action v-if="changePasswordAppear">
        <h3 class="mb-3 mt-2">您的登入帳號 {{loginInfo.Tel}} 請設定新密碼</h3>
        <div class="form-group">
          <span class="iconfont icon-lock"></span>
          <input
            class="form-control"
            type="number"
            placeholder="請輸入新密碼"
            autocomplete="off"
            v-model="loginInfo.Password"
          />
        </div>
        <a class="btn btn_default mb-2" @click.prevent="changePassword()">確認</a>
      </form>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      loginInfo: { Tel: "", Password: "" },
      vertifyAppear: false,
      changePasswordAppear: false,
      vertifyCode: "",
      changePasswordID: "",
      FromChangePS: false
    };
  },
  methods: {
    login() {
      let loader = this.$loading.show();
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
            loader.hide();
            // console.log(response);
            if (response.data == "success") {
              this.$swal("登入成功", "歡迎一起躺著點！", "success");
              this.$router.push({ name: "Product" });
            } else if (response.data == "此電話號碼尚未進行驗證") {
              this.$swal(response.data, "請驗證您的電話號碼！", "warning");
              vm.vertifyAppear = true;
            } else {
              this.$swal("哎呦！登入失敗", response.data, "warning");
            }
          });
        } else {
          loader.hide();
          this.$swal("哎呀！請輸入正確手機格式", "", "warning");
        }
      } else {
        loader.hide();
        this.$swal("哎呀！手機長度不符", "", "warning");
      }
    },
    // 忘記密碼
    ReSendSMS() {
      let loader = this.$loading.show();
      const vm = this;
      const params = vm.loginInfo.Tel;
      const url = `${process.env.APIPATH}/Accounts/ReSendSMS?Tel=${params}`;
      this.$http.get(url).then(response => {
        loader.hide();
        // console.log("重新發送驗證碼結果", response);
        if (response.data == "已寄發3次驗證碼，請您再次確認電話是否正確") {
          this.$swal(
            "已寄發3次驗證碼，請您再次確認電話是否正確",
            "若電話錯誤請您重新註冊",
            "warning"
          );
          vm.showVertify = false;
        } else if (response.data == "fail") {
          this.$swal(
            "發送錯誤，請確認您的電話號碼",
            "若電話錯誤請您重新註冊",
            "warning"
          );
        } else if (response.data == "success") {
          this.$swal(
            "簡訊驗證碼發送成功！",
            "請查看手機簡訊，並輸入驗證碼進行驗證",
            "success"
          );
        }
      });
    },
    vertify() {
      let loader = this.$loading.show();
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Vertify`;
      const data = { Vertify: vm.vertifyCode, Tel: vm.loginInfo.Tel };
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      this.$http.post(url, data, config).then(response => {
        loader.hide();
        console.log("驗證簡訊結果", response);
        if (typeof response.data == "number") {
          this.$swal("驗證簡訊成功", "歡迎一起躺著點！", "success");
          vm.changePasswordID = response.data;
          if (vm.FromChangePS == true) {
            vm.changePasswordAppear = true;
          } else {
            this.$router.push({ name: "Product" });
          }
        } else {
          this.$swal("驗證失敗", "請重新輸入驗證碼", "warning");
        }
      });
    },
    changePassword() {
      let loader = this.$loading.show();
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/EditPassword`;
      const data = {
        Id: vm.changePasswordID,
        Password: vm.loginInfo.Password
      };
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      this.$http.post(url, data, config).then(response => {
        loader.hide();
        console.log("修改密碼結果", response.data);
        if (response.data == "succcess") {
          this.$swal("修改成功", "歡迎一起躺著點！", "success");
          this.$router.push({ name: "Product" });
        } else {
          this.$swal("QQ 失敗", response.data, "warning");
        }
      });
    },
    // 自動登入
    CheckLogin() {
      const vm = this;
      let url = `${process.env.APIPATH}/Accounts/CheckLogin`;
      this.$http.get(url).then(response => {
        console.log("是否為登入狀態：", response.data);
        if (response.data === "False") {
          let href = location.href.split("login")[1];
          console.log(href);
          if (href !== "") {
            const vm = this;
            const url = `${process.env.APIPATH}/Accounts/Login`;
            const data = {
              Tel: href.split("?")[1].split("&")[1],
              Password: href.split("?")[1].split("&")[0]
            };
            console.log(href.split("?")[1].split("&")[0]);
            const config = {
              headers: {
                "Content-Type": "application/json"
              }
            };
            this.$http.post(url, data, config).then(response => {
              console.log(response.data);
              if (response.data == "success") {
                this.$swal(
                  " 歡迎使用自助點餐工具 ^__^ ",
                  "選擇喜歡的餐點加入點菜單吧！",
                  "success"
                );
                this.$router.push({ name: "Product" });
              } else {
                this.$swal(
                  "Sorry >__<，連結失敗",
                  "請洽櫃檯人員處理，謝謝您",
                  "warning"
                );
              }
            });
          }
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
        if (response.data != "success") {
          this.$router.push({ name: "Product" });
        }
      });
    }
  },
  created() {
    this.CheckLogin();
    this.recaptcha();
  }
};
</script>

<style src="../../../assets/css/all.css" scoped></style>