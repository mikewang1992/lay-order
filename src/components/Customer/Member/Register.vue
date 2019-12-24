<template>
  <div class="main col-lg-4 col-md-6 login_content">
    <div class="content">
      <img src="@/assets/img/logo.png" alt />
      <ul class="nav_group mb-3">
        <li>
          <router-link to="/login">登入</router-link>
        </li>
        <li @click="showVertify = false">
          <router-link to="/register" class="active">註冊</router-link>
        </li>
      </ul>
      <!-- 註冊 div -->
      <form v-if="!showVertify" @submit.prevent="recaptcha()">
        <div class="form-group mb-1">
          <label class="sr-only" for="phone">電話</label>
          <span class="iconfont icon-Mobile"></span>
          <input
            id="phone"
            class="form-control mb-0"
            type="phone"
            placeholder="電話"
            autocomplete="off"
            v-model="registerInfo.Tel"
            required
            maxlength="10"
          />
        </div>
        <small class="pb-2 d-block text-left color_gray pl-3">註冊後將發送簡訊驗證碼，請進行驗證</small>
        <div class="form-group">
          <label class="sr-only" for="name">姓名</label>
          <span class="iconfont icon-user"></span>
          <input
            id="name"
            class="form-control"
            type="text"
            placeholder="姓名"
            autocomplete="off"
            v-model="registerInfo.Name"
            required
            oninvalid="setCustomValidity('請輸入您的姓名');"
            oninput="this.setCustomValidity('')"
          />
        </div>
        <div class="form-group">
          <label class="sr-only" for="password">密碼</label>
          <span class="iconfont icon-lock"></span>
          <input
            id="password"
            class="form-control"
            type="password"
            placeholder="密碼"
            autocomplete="off"
            v-model="registerInfo.Password"
            required
            oninvalid="setCustomValidity('請輸入您的密碼');"
            oninput="this.setCustomValidity('')"
          />
        </div>
        <div class="form-group">
          <label class="sr-only" for="birth">生日</label>
          <span class="iconfont icon-birthday-cake"></span>
          <input
            id="birth"
            class="form-control"
            type="date"
            placeholder="生日"
            autocomplete="off"
            v-model="registerInfo.Birth"
            ref="inputDate"
          />
        </div>
        <div class="form-group mb-4">
          <label class="sr-only" for="county">居住地</label>
          <span class="iconfont icon-location"></span>
          <div class="d-flex">
            <select
              id="county"
              class="form-control mr-1"
              v-model="registerInfo.County"
              @change="getTown(registerInfo.County)"
            >
              <option value="城市" hidden selected>城市</option>
              <option v-for="(item,key,index) in Counties" :key="index" :value="item">{{item}}</option>
            </select>
            <select class="form-control" v-model="registerInfo.Dist" id="town">
              <option value="區域" hidden selected>區域</option>
              <option v-for="(item,key,index) in TownShips" :key="index" :value="item">{{item}}</option>
            </select>
          </div>
        </div>
        <button type="submit" class="btn btn_default mb-2">註冊</button>
      </form>
      <!-- 輸入驗證碼 div -->
      <form action v-if="showVertify" @submit.prevent="vertify">
        <h2 class="mb-2 color_default">請進行手機驗證</h2>
        <div class="ml-auto mr-auto mb-3">
          簡訊驗證碼已送至
          <span class="mb-1 font_price">{{registerInfo.Tel}}</span>
        </div>
        <div class="form-group mb-4">
          <label class="sr-only" for="phone">輸入簡訊驗證碼</label>
          <span class="iconfont icon-message"></span>
          <input
            class="form-control"
            type="text"
            id
            placeholder="輸入簡訊驗證碼"
            autocomplete="off"
            v-model="vertifyCodes.Vertify"
            maxlength="6"
          />
        </div>
        <button type="submit" class="btn btn_default mb-2">確認</button>
        <a href="#" @click.prevent="ReSendSMS" class="d-block">重新發送驗證碼</a>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      registerInfo: { County: "城市", Dist: "區域" },
      vertifyCodes: { Tel: "" },
      Counties: {},
      TownShips: {},
      showVertify: false
    };
  },
  methods: {
    register() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Create`;
      const data = vm.registerInfo;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      if (vm.registerInfo.Tel.length === 10) {
        if (vm.registerInfo.Tel.slice(0, 2) === "09") {
          // console.log("註冊資訊 registerInfo", data);
          vm.vertifyCodes.Tel = this.registerInfo.Tel;
          this.$http.post(url, data, config).then(response => {
            console.log("註冊回傳", response);
            if (response.data === "success") {
              vm.showVertify = true;
              this.$swal({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 10000,
                type: "success",
                title: "簡訊驗證碼已送出，請查看並進行驗證！"
              });
            } else if (response.data === "此電話已存在，請勿重複申請") {
              this.$swal(
                "此電話已存在，請勿重複申請",
                "再給你個機會",
                "warning"
              );
            } else {
              this.$swal("註冊失敗", "請重新確認您的登入資訊", "warning");
            }
          });
        } else {
          this.$swal({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            type: "warning",
            title: "請輸入正確手機格式"
          });
        }
      } else {
        this.$swal({
          toast: true,
          position: "top-end",
          showConfirmButton: false,
          timer: 3000,
          type: "warning",
          title: "手機長度不符"
        });
      }
    },
    vertify() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Vertify`;
      const data = vm.vertifyCodes;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      this.$http.post(url, data, config).then(response => {
        console.log("驗證簡訊結果", response);
        if (typeof response.data == "number") {
          this.$swal("手機驗證成功", "可以來點菜嚕！", "success");
          this.$router.push({ name: "Product" });
        } else {
          this.$swal("驗證失敗", "請重新輸入驗證碼", "warning");
        }
      });
    },
    ReSendSMS() {
      const vm = this;
      const params = vm.vertifyCodes.Tel;
      const url = `${process.env.APIPATH}/Accounts/ReSendSMS?Tel=${params}`;
      this.$http.get(url).then(response => {
        console.log("重新發送驗證碼結果", response);
        if (response.data == "已寄發3次驗證碼，請您再次確認電話是否正確") {
          this.$swal(
            "已寄發3次驗證碼",
            "無法發送驗證碼囉QQ，請聯繫店家以維護您的權利",
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
    getCounty() {
      const vm = this;
      const url = `${process.env.APIPATH}/Areas/County`;
      this.$http.get(url).then(response => {
        // console.log(response);
        vm.Counties = response.data;
      });
    },
    getTown(County) {
      // // console.log(county);
      const vm = this;
      const url = `${process.env.APIPATH}/Areas/Town?county=${County}`;
      this.$http.get(url).then(response => {
        // console.log(response);
        vm.TownShips = response.data;
      });
    },
    async recaptcha() {
      // (optional) Wait until recaptcha has been loaded.
      await this.$recaptchaLoaded();
      // Execute reCAPTCHA with action "login".
      const token = await this.$recaptcha("login");
      console.log(token);
      // Do stuff with the received token.
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
          this.register();
        } else {
          alert(response.data);
        }
      });
    }
  },
  created() {
    this.getCounty();
  }
};
</script>

<style src="../../../assets/css/all.css" scoped></style>