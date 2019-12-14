<template>
  <div class="page">
    <div class="main pb-5">
      <header>
        <a class="icon iconfont icon-left" @click.prevent="$router.go(-1);"></a>
        <h1>點菜單</h1>
      </header>
      <div class="content">
        <div class="cart_list">
          <ul>
            <li class="item" v-for="(item,index) in CartFromProduct" :key="index">
              <div class="p_img">
                <img :src="`https://lay-order.rocket-coding.com/Img/product/${item.Img}`" alt />
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
          <a
            href="result_out.html"
            class="btn btn_default d-block btn_lg"
            @click.prevent="Create"
          >確認點餐</a>
          <a href="#" class="btn btn_default d-block btn_lg open_popup">確認點餐(test)</a>
        </footer>
      </div>
    </div>
    <!---------------------------------------------- 彈跳視窗暫放div page ----------------------------------->
    <div class="popup">
      <a href="#" class="icon_close iconfont icon-weibiao45133 popup_close"></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="popup_info">
          <img src="@/assets/img/phone.png" alt />
          <h2>此電話未驗證</h2>
          <p>
            驗證電話加入會員^_^
            <br />第一次登入送驚喜優惠
          </p>
          <div class="input-group">
            <input type="text" class="form-control" placeholder="電話" />
            <span class="iconfont icon-Mobile"></span>
            <div class="input-group-append">
              <a href="#" class="btn" id>送出驗證碼</a>
            </div>
          </div>
          <div class="form-group">
            <label class="sr-only" for="phone">請輸入簡訊驗證碼</label>
            <span class="iconfont icon-message"></span>
            <input class="form-control" type="text" id placeholder="請輸入簡訊驗證碼" autocomplete="off" />
          </div>
          <div class="form-group">
            <label class="sr-only" for="password">密碼</label>
            <span class="iconfont icon-lock"></span>
            <input class="form-control" type="password" id placeholder="密碼" autocomplete="off" />
          </div>
          <a href="#" class="btn d-block btn_lg btn_default">確認</a>
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
      CartFromProduct: [],
      OrderPreStorage: [],
      // OrderPreStorage: {
      //   Pid: "",
      //   Options: "",
      //   Qty: "",
      //   time: ""
      // },
      ConfirmedOrder: [],
      PrepareTime: 20,
      OrderMemberInfo: "",
      Login: ""
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
      this.CartFromProduct = totalcart;
      if (this.CartFromProduct == 0) {
        alert("點菜單為空");
        this.$router.push({ name: "Product" });
      }
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
        alert("驗證成功");
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
          alert("請先登入");
          this.$router.push({ name: "Login" });
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
    Create() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Create`;
      const data = [];
      const predata = {
        Pid: "",
        Options: "",
        Qty: "",
        time: this.getFullTime(Date.now())
      };
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      // this.$http.post(url, data, config).then(response => {
      //   console.log(response);
      // });
    },
    addQty(item) {
      return item.Qty++;
    },
    minusQty(item, index) {
      if (item.Qty > 1) {
        return item.Qty--;
      } else {
        alert("刪除此商品:" + item.Name);
        console.log(index);
        const totalcart = JSON.parse(localStorage.getItem("totalcart"));
        totalcart.splice(index, 1);
        localStorage.setItem("totalcart", JSON.stringify(totalcart));
        this.getCart();
      }
    }
  },
  created() {
    this.CheckLogin();
    this.getCart();
    this.PreTime();
  }
};
</script>