<template>
  <div class="page">
    <OrderInfo :IdFromOrder="Id" v-if="ShowOrderInfo" @passevent="takeevent"></OrderInfo>
    <!-- <router-view :IdFromOrder="Id" v-if="ShowOrderInfo"></router-view> -->
    <footer>
      <div class="nav_logo">
        <img src="@/assets/img/logo_nav.png" alt />
      </div>
      <ul>
        <!-- <li :class="{'active':footerActive}"> -->
        <li>
          <router-link to="/">
            <img src="@/assets/img/icon_footer01.png" alt />
            <p>菜單</p>
          </router-link>
        </li>
        <li>
          <router-link to="/cart">
            <img src="@/assets/img/icon_footer02.png" alt />
            <p>
              點菜單
              <span v-if="footerNumber>0">:{{footerNumber}}項</span>
            </p>
          </router-link>
        </li>
        <li class="active">
          <router-link to="/order">
            <img src="@/assets/img/icon_footer03.png" alt />
            <p>訂單狀態</p>
          </router-link>
        </li>
        <li>
          <router-link to="/member">
            <img src="@/assets/img/icon_footer04.png" alt />
            <p>會員資訊</p>
          </router-link>
        </li>
      </ul>
    </footer>
    <div class="main pb-2" v-if="!ShowOrderInfo">
      <header>
        <h1>訂單狀態</h1>
      </header>
      <div class="content">
        <div class="cart_list status_list">
          <ul>
            <a v-for="(item,index) in OrderStatus" :key="index">
              <li class="item">
                <div
                  class="p_status"
                  :class="{'bg_default':item.status=='prepare','bg_yellow':item.status=='done','bg_gray':item.status=='paid'}"
                >
                  <h4>{{filterTranslate(item.status)}}</h4>
                </div>
                <div class="p_info">
                  <div class="p_date">
                    <h3>{{getFullTime(item.time)}} 取餐</h3>
                  </div>
                  <div class="p_price">${{item.total}}</div>
                </div>
                <div class="icon_right iconfont icon-right" @click.prevent="GoOrderInfo(item.id)"></div>
              </li>
            </a>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import OrderInfo from "./OrderInfo";
export default {
  data() {
    return {
      Id: 1,
      footerNumber: 0,
      OrderStatus: {},
      ShowOrderInfo: false
    };
  },
  components: { OrderInfo },
  methods: {
    checkFooterCart() {
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
      }
    },
    ShowOrderStatus() {
      const vm = this;
      const url = `${process.env.APIPATH}/Order/ShowOrderStatus`;
      this.$http.get(url).then(response => {
        console.log(response);
        this.OrderStatus = response.data;
      });
    },
    filterTranslate(str) {
      if (str === "prepare") {
        return "準備中";
      }
      if (str === "prepare") {
        return "準備中";
      }
      if (str === "prepare") {
        return "準備中";
      }
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
    GoOrderInfo(id) {
      this.Id = id;
      this.ShowOrderInfo = true;
      // this.$router.push({ name: "OrderInfo" });
    },
    takeevent() {
      this.ShowOrderInfo = false;
    }
  },
  created() {
    this.checkFooterCart();
    this.ShowOrderStatus();
  }
};
</script>