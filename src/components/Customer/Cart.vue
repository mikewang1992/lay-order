<template>
  <div class="main pb-0">
    <router-view :OrderCodeFromCart="OrderCode" v-if="ShowResult"></router-view>
    <!-----------------購物車主體----------------------->
    <header v-if="!ShowResult">
      <a class="icon iconfont icon-left" @click.prevent="$router.go(-1);"></a>
      <h1>點菜單</h1>
    </header>
    <div class="content mb-5" v-if="!ShowResult">
      <!-- 訂單資訊 -->
      <div class="cart_list col-md-6 offset-md-3 p-0">
        <ul>
          <li class="item" v-for="(item,index) in CartFromProduct" :key="index">
            <div class="p_img">
              <img :src="`https://lay-order.rocket-coding.com/Img/product/${item.Img[0]}`" alt>
            </div>
            <div class="p_info">
              <div class="p_name">
                <h3>{{item.Name}}</h3>
              </div>
              <div class="p_choose">
                <span v-for="(inneritem,index) in item.Options" class="mr-2" :key="index">{{inneritem}}</span>
              </div>
              <div class="p_num">
                <div class="btn btn_round btn_white count_box">
                  <a href="#" class="count_dis" @click.prevent="minusQty(item,index)">-</a>
                  <a href="#" class="count_num">{{item.Qty}}</a>
                  <a href="#" class="count_add" @click.prevent="item.Qty++">+</a>
                </div>
                <div class="p_price">${{item.Price*item.Qty}}</div>
              </div>
            </div>
          </li>
        </ul>
      </div>
      <!-- 內用顧客資訊 -->
      <div class="order_list col-md-6 offset-md-3" v-if="forhere">
        <ul>
          <li class="item total pt-0 pb-0">
            <h4>
              共
              <b class="color_default">{{OrderQty}}</b> 份
            </h4>
            <span>${{OrderMoney}}</span>
          </li>
        </ul>
      </div>
      <!-- 外帶顧客資訊 -->
      <div class="order_list col-md-6 offset-md-3" v-if="!forhere">
        <form action>
          <ul>
            <li class="item total">
              <h4>
                共
                <b class="color_default">{{OrderQty}}</b> 份
              </h4>
              <span>${{OrderMoney}}</span>
            </li>
            <li class="item">
              <h4>電話</h4>
              <input
                type="phone"
                v-if="Login == 'False'"
                placeholder="請輸入"
                maxlength="10"
                required
                v-model="loginInfo.Tel"
              >
              <p class="mb-0" v-if="Login == 'True'">{{OrderMemberInfo[0]}}</p>
            </li>
            <li class="item">
              <h4>取餐人</h4>
              <input
                type="text"
                v-if="Login == 'False'"
                placeholder="請輸入"
                v-model="registerInfo.Name"
              >
              <p class="mb-0" v-if="Login == 'True'">{{OrderMemberInfo[1]}}</p>
            </li>
            <li class="item" v-if="!ShowTimeSelect">
              <h4>取餐時間</h4>
              <p class="color_default mb-0">請於 {{yourStringTimeValue}} 後來店取餐</p>
            </li>
            <li class="item" v-if="ShowTimeSelect">
              <h4>取餐時間</h4>
              <p class="color_default mb-0">預定於 {{yourStringTimeValue}} 來店取餐</p>
            </li>
            <li class="item">
              <h4 class="mr-3">
                <input
                  type="checkbox"
                  id="selectTime"
                  class="w-auto d-inline"
                  v-model="ShowTimeSelect"
                  @click="CleanTimeBtn()"
                >
                <label for="selectTime">我要指定取餐時間</label>
              </h4>
              <vue-timepicker
                :hour-range="HourLimit"
                :minute-range="MinutesLimit"
                :format="yourFormat"
                v-model="yourStringTimeValue"
                close-on-complete
                hide-clear-button
                hide-disabled-hours
                hide-disabled-minutes
                hour-label="時"
                minute-label="分"
                @open="resetBussinessHours()"
                v-if="ShowTimeSelect"
              ></vue-timepicker>
            </li>
          </ul>
        </form>
      </div>
      <small
        class="color_red text-center d-block mt-2 mb-3"
        v-if="!forhere"
      >訂單總量超過20份請來電預約<br/>餐點現做，備餐時間約 {{PrepareTime}} min</small>
      <footer class="d-block text-center fixed_bottom">
        <div class="col-md-6 offset-md-3"><a class="btn btn_default d-block btn_lg" @click.prevent="CheckBeforeCreate">確認點餐</a></div>
      </footer>
    </div>

    <!-----------------登入poppup----------------------->
    <div class="popup" :class="{'show':ShowPopup}">
      <a
        href="#"
        class="icon_close iconfont icon-weibiao45133 popup_close"
        @click.prevent="ShowPopup=false"
      ></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="popup_info">
          <img src="@/assets/img/login_img.png" alt>
          <h2>Hello～歡迎回來！請先登入會員</h2>
          <br>
          <div class="form-group">
            <label class="sr-only" for="phone">電話</label>
            <span class="iconfont icon-message"></span>
            <input
              class="form-control"
              type="text"
              placeholder="電話"
              maxlength="10"
              autocomplete="off"
              v-model="loginInfo.Tel"
            >
          </div>
          <div class="form-group">
            <label class="sr-only" for="password">密碼</label>
            <span class="iconfont icon-lock"></span>
            <input
              class="form-control"
              type="password"
              placeholder="密碼"
              autocomplete="off"
              v-model="loginInfo.Password"
            >
          </div>
          <a href="#" class="btn btn_default mb-2" @click.prevent="login()">登入</a>
          <br>
          <small>
            <a @click.prevent="registerAppear=true,ShowPopup=false" class="color_gray">立即註冊</a>
          </small>
        </div>
      </div>
    </div>
    <!-----------------註冊poppup----------------------->
    <div class="popup" :class="{'show': registerAppear}">
      <a
        href="#"
        class="icon_close iconfont icon-weibiao45133 popup_close"
        @click.prevent="registerAppear=false,ShowPopup=false"
      ></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="popup_info">
          <img src="@/assets/img/phone.png" alt>
          <h2>驗證手機，立即加入會員！</h2>
          <p class="mb-0">註冊會員需驗證手機，請輸入正確的手機號碼</p>
          <br>
          <!-- 註冊資訊 -->
          <form v-if="!showVertify" @submit.prevent="register">
            <div class="form-group">
              <label class="sr-only" for="phone">電話</label>
              <span class="iconfont icon-Mobile"></span>
              <input
                type="phone"
                class="form-control"
                placeholder="電話"
                maxlength="10"
                v-model="registerInfo.Tel"
              >
            </div>
            <div class="form-group">
              <label class="sr-only" for="userName">姓名</label>
              <span class="iconfont icon-user"></span>

              <input
                class="form-control"
                type="userName"
                placeholder="請輸入姓名"
                autocomplete="off"
                v-model="registerInfo.Name"
              >
            </div>
            <div class="form-group">
              <label class="sr-only" for="password">密碼</label>
              <span class="iconfont icon-lock"></span>
              <input
                class="form-control"
                type="password"
                placeholder="請設定密碼"
                autocomplete="off"
                v-model="registerInfo.Password"
              >
            </div>
            <button type="submit" class="btn btn_default mb-2">註冊</button>
          </form>
          <!-- 驗證碼 -->
          <form action v-if="showVertify" @submit.prevent="vertify">
            <p>簡訊驗證碼已發送至 {{loginInfo.Tel}}</p>
            <div class="form-group">
              <label class="sr-only" for="phone">簡訊驗證碼</label>
              <span class="iconfont icon-message"></span>
              <input
                class="form-control"
                type="text"
                placeholder="請輸入簡訊驗證碼"
                autocomplete="off"
                v-model="vertifyInfo.Vertify"
              >
            </div>
            <button type="submit" class="btn btn_default mb-2">確認</button>
            <br>
            <a href="#" @click.prevent="ReSendSMS" class="d-block">重新發送驗證碼</a>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import VueTimepicker from "vue2-timepicker/src/vue-timepicker.vue";
export default {
  data() {
    return {
      OrderCode: 1,
      CartFromProduct: [],
      ConfirmedOrder: [],
      PrepareTime: 20,
      OrderMemberInfo: "",
      Login: "",
      ShowPopup: false,
      loginInfo: { Tel: "", Password: "" },
      registerAppear: false,
      showVertify: false,
      registerInfo: { Tel: "", Password: "", Name: "" },
      vertifyInfo: { Tel: "", Vertify: "" },
      ShowResult: false,
      // 時間data
      businesshours: ["00,00", "00,00"],
      HourLimit: [[]],
      yourFormat: "HH:mm",
      yourStringTimeValue: "00:00",
      ShowTimeSelect: false
    };
  },
  props: ["forhere", "isOpenFromCustomer"],
  components: { VueTimepicker },
  computed: {
    FullTimeNow() {
      const date = new Date(Date.now());
      let year = date.getFullYear();
      let mon = date.getMonth() + 1;
      let day = date.getDate();
      let hour = date.getHours();
      let min = date.getMinutes();
      if (min < 10) {
        min = "0" + min;
      }
      const newFullDay = `${year}/${mon}/${day} ${hour}:${min}`;
      return newFullDay;
    },
    OrderMemberInfoSplit() {
      return [
        this.OrderMemberInfo.split(",")[0],
        this.OrderMemberInfo.split(",")[1]
      ];
    },
    OrderQty() {
      let str = 0;
      for (let i = 0; i < this.CartFromProduct.length; i++) {
        str += this.CartFromProduct[i].Qty;
      }
      return str;
    },
    OrderMoney() {
      let str = 0;
      for (let i = 0; i < this.CartFromProduct.length; i++) {
        str += this.CartFromProduct[i].Qty * this.CartFromProduct[i].Price;
      }
      return str;
    },
    MinutesLimit() {
      const selectedhour = this.yourStringTimeValue.split(":")[0];
      const selectedminutes = this.yourStringTimeValue.split(":")[1];
      const starthour = this.businesshours[0].split(":")[0];
      const startminutes = this.businesshours[0].split(":")[1];
      const endhour = this.businesshours[1].split(":")[0];
      const endminutes = this.businesshours[1].split(":")[1];

      //如果所選小時==開始營業小時,回傳開始營業分鐘限制,否則回傳無限制
      if (selectedhour == starthour) {
        return [[startminutes, 59]];
      }
      //如果所選小時==結束營業小時,且分鐘數不存在等於00就回傳無限制,否則回傳限制
      else if (selectedhour == endhour) {
        if (endminutes == "00") {
          return [[0, 59]];
        } else {
          return [[0, endminutes]];
        }
      } else {
        return [[0, 59]];
      }
    },
    PreTime() {
      const vm = this;
      const url = `${process.env.APIPATH}/Company/PreTime`;
      this.$http.get(url).then(response => {
        // console.log('餐點準備時間：',response.data);
        this.PrepareTime = response.data;
      });
    }
  },
  watch: {},
  methods: {
    getCart() {
      const totalcart = JSON.parse(localStorage.getItem("totalcart"));
      // console.log('取得點菜單產品：',totalcart);
      console.log("營業時間", this.isOpenFromCustomer);
      if (this.isOpenFromCustomer == false) {
        this.$swal({
            title: "目前非營業時間",
            text: "不開放點餐唷",
            type: "warning",
            showCancelButton: false,
            confirmButtonText: "看看菜單",
            reverseButtons: true
          }).then(result => {
            // this.$router.push({ name: "Product" });
          });
      } else {
        if (totalcart === null || totalcart.length === 0) {
          localStorage.setItem("totalcart", JSON.stringify([]));
          this.$swal({
            title: "點菜單沒有東西唷",
            text: "請至菜單選擇商品^__^",
            type: "warning",
            showCancelButton: false,
            confirmButtonText: "回到菜單",
            reverseButtons: true
          }).then(result => {
            this.$router.push({ name: "Product" });
          });
        } else {
          this.CartFromProduct = totalcart;
        }
      }
    },
    getTime(time) {
      const date = new Date(time);
      let m = date.getMinutes();
      if (m < 10) {
        m = "0" + m;
      }
      const newTime = `${date.getHours()}:${m}`;
      return newTime;
    },
    getFullTime(time) {
      const date = new Date(time);
      let year = date.getFullYear();
      let mon = date.getMonth() + 1;
      let day = date.getDate();
      let hour = date.getHours();
      let min = date.getMinutes();
      if (min < 10) {
        min = "0" + min;
      }
      const newFullDay = `${year}/${mon}/${day} ${hour}:${min}`;
      return newFullDay;
    },
    CheckLogin() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/CheckLogin`;
      this.$http.get(url).then(response => {
        // console.log('是否為登入狀態：',response.data);
        if (response.data === "True") {
          this.Login = response.data;
          this.OrderMember();
        } else if (response.data === "False") {
          this.Login = response.data;
        }
      });
    },
    OrderMember() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/OrderMember`;
      this.$http.get(url).then(response => {
        // console.log('取得會員資訊',response);
        vm.OrderMemberInfo = response.data.split(",");
        vm.loginInfo.Tel = vm.OrderMemberInfo[0];
      });
    },
    CheckBeforeCreate() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Login`;
      const data = vm.loginInfo;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      if (this.Login === "True") {
        this.CreateOrder();
      } else if (this.Login === "False") {
        if (vm.loginInfo.Tel.length === 10) {
          if (vm.loginInfo.Tel.slice(0, 2) === "09") {
            this.$http.post(url, data, config).then(response => {
              if (response.data == "登入失敗") {
                // console.log("還沒登入，顯示 Login popup");
                this.ShowPopup = true;
              } else {
                this.$swal({
                  title: "請先註冊會員才能點餐唷",
                  type: "warning",
                  showCancelButton: true,
                  confirmButtonText: "立即註冊",
                  cancelButtonText: "使用其他帳號"
                }).then(result => {
                  if (result.value) {
                    console.log("打開註冊 popup");
                    this.registerInfo.Tel = this.loginInfo.Tel;
                    this.registerAppear = true;
                    this.ShowPopup = false;
                  }
                });
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
      }
    },
    CreateOrder() {
      const vm = this;
      const url = `${process.env.APIPATH}/Order/Create`;
      const data = [];

      for (let i = 0; i < vm.CartFromProduct.length; i++) {
        const predata = {
          Pid: vm.CartFromProduct[i].Pid.toString(),
          Options: vm.CartFromProduct[i].Options,
          Qty: vm.CartFromProduct[i].Qty.toString(),
          orderTime: vm.FullTimeNow,
          getTime: vm.yourStringTimeValue
        };
        let str = "";
        for (let j = 0; j < vm.CartFromProduct[i].Options.length; j++) {
          str = str + vm.CartFromProduct[i].Options[j] + ",";
        }
        if (str.substr(-1) === ",") {
          str = str.substring(0, str.length - 1);
        }
        predata.Options = str;
        data.push(predata);
      }
      // console.log('POST 餐點資訊：',data);
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      // this.$http.post(url, data, config).then(response => {
      //   if (response == "fail") {
      //     this.$swal(response, "", "info");
      //   } else {
      //     this.$swal("訂餐成功", "", "success");
      //     vm.OrderCode = response.data;
      //     localStorage.setItem("totalcart", JSON.stringify([]));
      //     const url = `${process.env.APIPATH}/Accounts/IsTable`;
      //     this.$http.get(url).then(response => {
      //       vm.ShowResult = true;
      //       if (response.data === "外帶") {
      //         this.$router.push({ name: "ResultOut" });
      //       } else {
      //         this.$router.push({ name: "ResultIn" });
      //       }
      //     });
      //   }
      // });
    },
    minusQty(item, index) {
      if (item.Qty > 1) {
        return item.Qty--;
      } else {
        this.$swal({
          title: "你要移除這個產品嗎?",
          text: "你怎麼不要我了Q__Q",
          type: "warning",
          showCancelButton: true,
          confirmButtonText: "確認",
          cancelButtonText: "取消"
        }).then(result => {
          if (result.value) {
            this.$swal({
              toast: true,
              position: "top-end",
              showConfirmButton: false,
              timer: 3000,
              type: "success",
              title: "你怎麼這樣~~Q____Q"
            });
            // console.log(index);
            const totalcart = JSON.parse(localStorage.getItem("totalcart"));
            totalcart.splice(index, 1);
            localStorage.setItem("totalcart", JSON.stringify(totalcart));
            this.getCart();
          }
        });
      }
    },
    // 登入註冊
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
              this.$swal("登入成功", "", "success");
              vm.ShowPopup = false;
              vm.CheckLogin();
            } else if (response.data === "此電話號碼尚未進行驗證") {
              this.$swal(response.data, "", "warning");
              this.registerAppear = true;
              this.reInfo = false;
              this.reVertify = true;
              this.vertifyInfo.Tel = this.loginInfo.Tel;
            } else {
              this.$swal(response.data, "", "warning");
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
    register() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Create`;
      const data = vm.registerInfo;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };

      if (data.Tel !== "" && data.Name !== "" && data.Password !== "") {
        // console.log("送出註冊資訊");
        this.vertifyInfo.Tel = data.Tel;
        this.$http.post(url, data, config).then(response => {
          console.log("註冊結果：", response.data);
          if (response.data === "success") {
            vm.showVertify = true;
            this.vertifyCodes.Tel = vm.registerInfo.Tel;
            this.$swal({
              toast: true,
              position: "top-end",
              showConfirmButton: false,
              timer: 10000,
              type: "success",
              title: "簡訊驗證碼已送出，請查看並進行驗證！"
            });
          } else if (response.data === "此電話已存在，請勿重複申請") {
            this.$swal("此電話已存在，請勿重複申請", "再給你個機會", "warning");
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
          title: "請輸入所有內容"
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
            "已寄發3次驗證碼，請您再次確認電話是否正確",
            "若電話錯誤請您重新註冊",
            "warning"
          );
          vm.showVertify = false;
          vm.registerAppear = false;
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
    // 營業時間
    getBusinessHours() {
      const vm = this;
      const url = `${process.env.APIPATH}/Company/BusinessHours`;
      this.$http.get(url).then(response => {
        console.log(response.data);
        // vm.businesshours = response.data;
        vm.businesshours[0] = vm.getTime(vm.TimeWithPrepare(vm.PrepareTime));
        vm.businesshours[1] = response.data[1];
        vm.yourStringTimeValue = vm.businesshours[0];
        //  vm.HourLimit = [
        //   [vm.businesshours[0].split(":")[0], vm.businesshours[1].split(":")[0]]
        // ];
        //
        if (vm.businesshours[1].split(":")[1] == "00") {
          vm.HourLimit = [
            [
              vm.businesshours[0].split(":")[0],
              vm.businesshours[1].split(":")[0] - 1
            ]
          ];
        } else {
          vm.HourLimit = [
            [
              vm.businesshours[0].split(":")[0],
              vm.businesshours[1].split(":")[0]
            ]
          ];
        }
      });
    },
    // 也許尚未完善?
    resetBussinessHours() {
      const vm = this;
      vm.businesshours[0] = vm.getTime(vm.TimeWithPrepare(vm.PrepareTime));
      if (vm.businesshours[1].split(":")[1] == "00") {
        vm.HourLimit = [
          [
            vm.businesshours[0].split(":")[0],
            vm.businesshours[1].split(":")[0] - 1
          ]
        ];
      } else {
        vm.HourLimit = [
          [vm.businesshours[0].split(":")[0], vm.businesshours[1].split(":")[0]]
        ];
      }
    },
    TimeWithPrepare(prepare) {
      var d = new Date();
      var year = d.getFullYear();
      var month = d.getMonth();
      var day = d.getDate();
      var hour = d.getHours();
      var min = d.getMinutes();
      var s = d.getSeconds();
      d = new Date(year, month, day, hour, min + prepare, s);
      return d;
    },
    CleanTimeBtn() {
      this.yourStringTimeValue = this.businesshours[0];
    }
  },
  created() {
    this.getCart();
    this.CheckLogin();
    this.getBusinessHours();
  },
  mounted() {
    document.querySelector("footer ul").classList.add("d-none");
  }
};
</script>

<style src="../../assets/css/all.css" scoped></style>