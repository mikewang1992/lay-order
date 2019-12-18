<template>
  <div class="main pb-2">
    <OrderInfo :IdFromOrder="Id" v-if="ShowOrderInfo" @passevent="takeevent"></OrderInfo>
    <!-- <router-view :IdFromOrder="Id" v-if="ShowOrderInfo"></router-view> -->
    <header v-if="!ShowOrderInfo">
      <h1>訂單狀態</h1>
    </header>
    <div class="content" v-if="!ShowOrderInfo">
      <div class="cart_list status_list">
        <ul>
          <a v-for="(item,index) in OrderStatus" :key="index">
            <li class="item">
              <div
                class="p_status"
                :class="{'bg_default':item.status==='prepare'||item.status==='ready','bg_yellow':item.status==='finish'||item.status==='done','bg_gray':item.status==='paid'||item.status==='cancel'}"
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
      } else if (str === "ready") {
        return "準備中";
      } else if (str === "finish") {
        return "待取餐";
      } else if (str === "done") {
        return "待取餐";
      } else if (str === "paid") {
        return "已付款";
      } else if (str === "cancel") {
        return "已取消";
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
<style src="../../assets/css/all.css" scoped></style>