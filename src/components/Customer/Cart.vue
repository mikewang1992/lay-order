<template>
  <div class="main pb-5">
    <router-view :OrderCodeFromCart="OrderCode" v-if="ShowResult"></router-view>
    <header v-if="!ShowResult">
      <a class="icon iconfont icon-left" @click.prevent="$router.go(-1);"></a>
      <h1>點菜單</h1>
    </header>
    <div class="content" v-if="!ShowResult">
      <div class="cart_list">
        <ul>
          <li class="item" v-for="(item,index) in CartFromProduct" :key="index">
            <div class="p_img">
              <img :src="`https://lay-order.rocket-coding.com/Img/product/${item.Img[0]}`" alt />
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
            <input type="text" placeholder="請輸入" :value="OrderMemberInfoSplit[0]" />
            <!-- <input type="text" placeholder="請輸入" v-model="loginInfo.Tel"/> -->
          </li>
          <li class="item">
            <h4>取餐人</h4>
            <input type="text" placeholder="請輸入" :value="OrderMemberInfoSplit[1]" />
          </li>
          <li class="item">
            <h4>取餐時間</h4>
            <small class="color_default">製作時間約{{PrepareTime}}分，請於{{timeNow}}後來店取餐</small>
          </li>
          <li class="item">
            <h4>
              <input type="checkbox" id="selectTime" class="w-auto d-inline" />
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

    <!---------------------------------------------- 彈跳視窗暫放div page ----------------------------------->
    <div class="popup" :class="{'show':ShowPopup}">
      <a
        href="#"
        class="icon_close iconfont icon-weibiao45133 popup_close"
        @click="ShowPopup=false"
      ></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="popup_info">
          <img src="@/assets/img/phone.png" alt />
          <h2>尚未登入</h2>
          <br />
          <div class="form-group">
            <label class="sr-only" for="phone">電話</label>
            <span class="iconfont icon-message"></span>
            <input
              class="form-control"
              type="text"
              id
              placeholder="電話"
              autocomplete="off"
              v-model="loginInfo.Tel"
            />
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
            />
          </div>
          <a href="#" class="btn btn_default mb-2" @click="login()">登入</a>
        </div>
      </div>
    </div>
    <!--  -->
    <div class="popup" :class="{'show': resendAppear}">
      <a
        href="#"
        class="icon_close iconfont icon-weibiao45133 popup_close"
        @click="resendAppear=false,ShowPopup=false"
      ></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="popup_info">
          <img src="@/assets/img/phone.png" alt />
          <h2>手機尚未通過簡訊驗證</h2>
          <br />
          <div class="input-group">
            <input type="text" class="form-control" placeholder="電話" v-model="vertifyInfo.Tel" />
            <span class="iconfont icon-Mobile"></span>
            <div class="input-group-append" @click="ReSendSMS()">
              <a href="#" class="btn" id>重寄驗證碼</a>
            </div>
          </div>
          <div class="form-group">
            <label class="sr-only" for="phone">重寄驗證碼簡訊</label>
            <span class="iconfont icon-message"></span>
            <input
              class="form-control"
              type="text"
              id
              placeholder="請輸入簡訊驗證碼"
              autocomplete="off"
              v-model="vertifyInfo.Vertify"
            />
          </div>
          <a class="btn btn_default mb-2" @click="vertify()">驗證手機</a>
        </div>
      </div>
    </div>
    <!---------------------------------------------- 彈跳視窗暫放div page ----------------------------------->
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
      resendAppear: false,
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
      let mon = date.getMonth();
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
        this.$swal("點菜單沒有東西唷", "", "warning");
        this.$router.push({ name: "Product" });
      }
      this.CartFromProduct = totalcart;
    },
    sendCart() {
      const vm = this;
      const url = `${process.env.APIPATH}/Order/Create`;
      const data = {};
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      this.$http.post(url, data, config).then(response => {
        console.log(response);
        this.$swal("驗證成功", "", "success");
      });
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
      let mon = date.getMonth();
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
        console.log(response);
        this.OrderMemberInfo = response.data;
      });
    },
    CheckLogin() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/CheckLogin`;
      this.$http.get(url).then(response => {
        console.log(response);
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
        console.log(response);
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
      console.log(data);
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
          localStorage.removeItem("totalcart");
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
          toast: true,
          position: "top-end",
          showConfirmButton: false,
          timer: 3000,
          type: "success",
          title: "你怎麼不要我了Q__Q"
        });
        console.log(index);
        const totalcart = JSON.parse(localStorage.getItem("totalcart"));
        totalcart.splice(index, 1);
        localStorage.setItem("totalcart", JSON.stringify(totalcart));
        this.getCart();
      }
    },
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
            } else if (response.data == "此電話號碼尚未進行驗證") {
              this.$swal("此電話號碼尚未進行驗證", "", "warning");
              vm.resendAppear = true;
            } else {
              this.$swal(response.data, "", "info");
              this.$router.push({ name: "Register" });
            }
          });
        } else {
          this.$swal("請輸入正確手機格式", "", "warning");
        }
      } else {
        this.$swal("手機長度不符", "", "warning");
      }
    },
    ReSendSMS() {
      const vm = this;
      const params = vm.loginInfo.Tel;
      const url = `${process.env.APIPATH}/Accounts/ReSendSMS?Tel=${params}`;
      this.$http.get(url).then(response => {
        console.log(response);
        if (response.data !== "已寄發3次驗證碼，請您再次確認電話是否正確") {
          vm.vertifyAppear = true;
          this.$swal(
            "哇哩咧！",
            "已寄發3次驗證碼，請您再次確認電話是否正確",
            "warning"
          );
        } else {
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
        console.log(response);
        if (response.data !== "驗證失敗，請重新輸入") {
          this.$swal("驗證成功", "", "success");
          this.CheckLogin();
          resendAppear = false;
        } else {
          this.$swal("驗證失敗，請重新輸入", "", "error");
        }
      });
    },
    CheckBeforeCreate() {
      if (this.Login === "True") {
        this.CreateOrder();
      } else if (this.Login === "False") {
        this.ShowPopup = true;
      }
    }
  },
  created() {
    document.querySelector('footer ul').classList.add('d-none');
    this.getCart();
    this.CheckLogin();
    this.PreTime();
    console.log(OrderQty);
  }
};
</script>

<style scope lang="scss">

</style>
