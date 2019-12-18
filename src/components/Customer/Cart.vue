<template>
  <div class="main pb-0">
    <router-view :OrderCodeFromCart="OrderCode" v-if="ShowResult"></router-view>
    <!-----------------購物車主體----------------------->
    <header v-if="!ShowResult">
      <a class="icon iconfont icon-left" @click.prevent="$router.go(-1);"></a>
      <h1>點菜單</h1>
    </header>
    <div class="content mb-5" v-if="!ShowResult">
      <div class="cart_list">
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
                <span v-for="(inneritem,index) in item.Options" :key="index">{{inneritem}},</span>
              </div>
              <div class="p_num">
                <div class="btn btn_round btn_white count_box">
                  <a href="#" class="count_dis" @click.prevent="minusQty(item,index)">-</a>
                  <a href="#" class="count_num">{{item.Qty}}</a>
                  <a href="#" class="count_add" @click.prevent="addQty(item)">+</a>
                </div>
                <div class="p_price">${{item.Price*item.Qty}}</div>
              </div>
            </div>
          </li>
        </ul>
      </div>
      <div class="order_list">
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
            <!-- <input type="text" placeholder="請輸入" :value="OrderMemberInfoSplit[0]" /> -->
            <input type="phone" placeholder="請輸入" maxlength="10" required v-model="loginInfo.Tel">
          </li>
          <li class="item">
            <h4>取餐人</h4>
            <input type="text" placeholder="請輸入" :value="OrderMemberInfo[1]">
          </li>
          <li class="item">
            <h4>取餐時間</h4>
            <small class="color_default">製作時間約{{PrepareTime}}分，請於{{timeNow}}後來店取餐</small>
            <!-- <small class="color_default">我要指定於今天來店取餐</small> -->
          </li>
          <li class="item">
            <h4>
              <input type="checkbox" id="selectTime" class="w-auto d-inline">
              <label for="selectTime">我要指定取餐時間</label>
            </h4>
            <div class="form-check">
              <select name id>
                <option value="123" selected hidden>請選擇</option>
                <option value="123">12:20</option>
                <option value="123">12:40</option>
                <option value="123">12:50</option>
              </select>
            </div>
          </li>
        </ul>
      </div>
      <small class="color_red text-center d-block mt-2 mb-3">訂單總量超過20份請來電預約,餐點現做，製作時間約 25 min</small>
      <footer class="d-block text-center fixed_bottom">
        <a class="btn btn_default d-block btn_lg" @click.prevent="CheckBeforeCreate">確認點餐</a>
        <!-- <a href="#" class="btn btn_default d-block btn_lg open_popup">確認點餐(test)</a> -->
      </footer>
    </div>

    <!-----------------登入poppup----------------------->
    <div class="popup" :class="{'show':ShowPopup}">
      <a
        href="#"
        class="icon_close iconfont icon-weibiao45133 popup_close"
        @click="ShowPopup=false"
      ></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="popup_info">
          <img src="@/assets/img/login_img.png" alt>
          <h2>請先登入會員</h2>
          <br>
          <div class="form-group">
            <label class="sr-only" for="phone">電話</label>
            <span class="iconfont icon-message"></span>
            <input
              class="form-control"
              type="text"
              id
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
              id
              placeholder="密碼"
              autocomplete="off"
              v-model="loginInfo.Password"
            >
          </div>
          <a href="#" class="btn btn_default mb-2" @click="login()">登入</a>
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
        @click="registerAppear=false,ShowPopup=false"
      ></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="popup_info">
          <img src="@/assets/img/phone.png" alt>
          <h2>驗證手機，立即加入會員！</h2>
          <br>
          <div v-if="reInfo">
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
                id
                placeholder="請設定密碼"
                autocomplete="off"
                v-model="registerInfo.Password"
              >
            </div>
            <a class="btn btn_default mb-2" @click="register()">確認</a>
          </div>
          <div class="mr-4 ml-4" v-if="reVertify">
            <p>簡訊驗證碼已發送至 {{loginInfo.Tel}}</p>
            <!-- <p>簡訊驗證碼已發送至 {{registerInfo.Tel}}</p> -->
            <div class="form-group">
              <label class="sr-only" for="phone">簡訊驗證碼</label>
              <span class="iconfont icon-message"></span>
              <input
                class="form-control"
                type="text"
                id
                placeholder="請輸入簡訊驗證碼"
                autocomplete="off"
                v-model="vertifyInfo.Vertify"
              >
            </div>
            <div class="d-flex">
              <!-- <a class="btn d-block w-100 btn_yellow mb-2 mr-1" @click="ReSendSMS()">重新發送</a> -->
              <a class="btn btn_default w-100 mb-2 ml-1" @click="vertify()">確認</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  data() {
    return {
      OrderCode: 1,
      CartFromProduct: [],
      OrderPreStorage: [],
      ConfirmedOrder: [],
      PrepareTime: 20,
      OrderMemberInfo: "",
      Login: "",
      ShowPopup: false,
      loginInfo: { Tel: "", Password: "" },
      registerAppear: false,
      reInfo: true,
      reVertify: false,
      registerInfo: { Tel: "", Password: "", Name: "" },
      vertifyInfo: { Tel: "", Vertify: "" },
      ShowResult: false
    };
  },
  computed: {
    timeNow() {
      const date = new Date(Date.now());
      let m = date.getMinutes() + this.PrepareTime;
      if (m < 10) {
        m = "0" + m;
      }
      const newTime = `${date.getHours()}:${m}`;
      return newTime;
    },
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
    }
  },
  methods: {
    getCart() {
      const totalcart = JSON.parse(localStorage.getItem("totalcart"));
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
    OrderMember() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/OrderMember`;
      this.$http.get(url).then(response => {
        // console.log(response);
        vm.OrderMemberInfo = response.data.split(",");
        vm.loginInfo.Tel = vm.OrderMemberInfo[0];

        // this.OrderMemberInfo.split(",")[0],
        // this.OrderMemberInfo.split(",")[1]
      });
    },
    CheckLogin() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/CheckLogin`;
      this.$http.get(url).then(response => {
        // console.log(response);
        if (response.data === "True") {
          this.Login = response.data;
          this.OrderMember();
        } else if (response.data === "False") {
          this.Login = response.data;
          // alert("請先登入");
          // this.$router.push({ name: "Login" });
        }
      });
    },
    PreTime() {
      const vm = this;
      const url = `${process.env.APIPATH}/Company/PreTime`;
      this.$http.get(url).then(response => {
        // console.log(response);
        this.PrepareTime = response.data;
      });
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
          time: this.getFullTime(Date.now())
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
      // console.log(data);
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      this.$http.post(url, data, config).then(response => {
        if (response == "fail") {
          this.$swal(response, "", "info");
        } else {
          this.$swal("訂餐成功", "", "success");
          vm.OrderCode = response.data;
          localStorage.setItem("totalcart", JSON.stringify([]));
          const url = `${process.env.APIPATH}/Accounts/IsTable`;
          this.$http.get(url).then(response => {
            vm.ShowResult = true;
            if (response.data === "外帶") {
              this.$router.push({ name: "ResultOut" });
            } else {
              this.$router.push({ name: "ResultIn" });
            }
          });
        }
      });
    },
    addQty(item) {
      return item.Qty++;
    },
    minusQty(item, index) {
      if (item.Qty > 1) {
        return item.Qty--;
      } else {
        this.$swal({
          title: "確定刪除這個產品嗎?",
          text: "你怎麼不要我了Q__Q",
          type: "warning",
          showCancelButton: true,
          confirmButtonText: "確認",
          cancelButtonText: "取消"
        }).then(result => {
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
        console.log("送出");

        this.vertifyInfo.Tel = data.Tel;
        this.$http.post(url, data, config).then(response => {
          console.log(response);
          if (response.data === "success") {
            this.reInfo = false;
            this.reVertify = true;
            this.$swal({
              toast: true,
              position: "top-end",
              showConfirmButton: false,
              timer: 3000,
              type: "success",
              title: "註冊成功，請驗證您的手機號碼"
            });
          } else if (response.data === "此電話已存在，請勿重複申請") {
            this.$swal("此電話已存在，請勿重複申請", "再給你個機會", "warning");
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
          title: "請輸入所有內容"
        });
      };
    },
    ReSendSMS() {
      const vm = this;
      const params = vm.loginInfo.Tel;
      const url = `${process.env.APIPATH}/Accounts/ReSendSMS?Tel=${params}`;
      this.$http.get(url).then(response => {
        console.log(response);
        if (response.data == "已寄發3次驗證碼，請您再次確認電話是否正確") {
          vm.vertifyAppear = true;
          this.$swal(
            "哇哩咧！",
            "已寄發3次驗證碼，請您再次確認電話是否正確",
            "warning"
          );
        } else {
          console.log(response);
          this.$swal(response.data, "", "info");
        }
      });
    },
    vertify() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Vertify`;
      const data = vm.vertifyInfo;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      this.$http.post(url, data, config).then(response => {
        // console.log(response);
        if (response.data !== "驗證失敗，請重新輸入") {
          this.$swal("驗證成功", "觀迎繼續點餐", "success");
          registerAppear = false;
          ShowPopup = false;
          reInfo = true;
          reVertify = false;
          this.OrderMemberInfo[0] = this.vertifyInfo.Tel;
          this.OrderMemberInfo[1] = this.registerInfo.Name;
        } else {
          this.$swal("驗證失敗", "請重新進行簡訊驗證", "error");
        }
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
        // console.log("未登入");
        if (vm.loginInfo.Tel.length === 10) {
          if (vm.loginInfo.Tel.slice(0, 2) === "09") {
            this.$http.post(url, data, config).then(response => {
              console.log(response);
              if (response.data == "登入失敗") {
                this.ShowPopup = true;
                // vm.loginInfo.Tel;
                console.log("你還沒登入，showLogin");
              } else {
                this.$swal({
                  title: response.data,
                  type: "warning",
                  showCancelButton: false,
                  confirmButtonText: "立即註冊",
                  cancelButtonText: "使用其他帳號"
                }).then(result => {
                  console.log("打開註冊popup");
                  this.registerInfo.Tel = this.loginInfo.Tel;
                  this.registerAppear = true;
                  this.ShowPopup = false;
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
    }
  },
  created() {
    this.getCart();
    this.CheckLogin();
    this.PreTime();
  },
  mounted() {
    document.querySelector("footer ul").classList.add("d-none");
  }
};
</script>

<style src="../../assets/css/all.css" scoped></style>