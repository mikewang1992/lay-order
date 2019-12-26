<template>
  <div class="main pb-2">
    <OrderInfo :forhere="forhere" :IdFromOrder="Id" v-if="ShowOrderInfo" @passevent="takeevent"></OrderInfo>
    <header v-if="!ShowOrderInfo">
      <h1>訂單狀態</h1>
    </header>
    <div class="content pb-5 mb-3" v-if="!ShowOrderInfo">
      <div class="cart_list status_list">
        <ul>
          <a
            v-for="(item,index) in OrderStatus"
            :key="item.id"
            @click.prevent="GoOrderInfo(item.id)"
          >
            <li class="item">
              <div
                class="p_status"
                :class="{'bg_default':item.status==='prepare'||item.status==='ready','bg_yellow':item.status==='finish'||item.status==='done','bg_gray':item.status==='paid'||item.status==='cancel'}"
              >
                <h4 class="text-center">
                  <span class="font_lg font_price">{{item.id}}.</span>
                  <br>
                  {{filterTranslate(item.status)}}
                </h4>
              </div>
              <div class="p_info">
                <div class="p_date">
                  <h3>{{getFullTime(item.time)}} 取餐</h3>
                </div>
                <div class="p_price font_price">${{item.total}}</div>
              </div>
              <div class="icon_right iconfont icon-right"></div>
            </li>
          </a>
        </ul>
      </div>
    </div>
  </div>
</template>


<script>
import signalR from "../../assets/js/jquery.signalR-2.4.1.min.js";
import hub from "../../assets/js/hubs.js";
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
  props: ["forhere"],
  methods: {
    checkFooterCart() {
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
      }
    },
    ShowOrderStatus(forhere) {
      const vm = this;
      const url = `${process.env.APIPATH}/Order/ShowOrderStatus`;
      this.$http.get(url).then(response => {
        console.log(response);
        if (forhere == true) {
          this.OrderStatus = response.data.filter(function(item) {
            return item.status != "paid" && item.status != "cancel";
          });
        } else {
          this.OrderStatus = response.data;
        }
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
    },
    websocketListen() {
      const vm = this;
      $.connection.chatHub.client.addNewMessageToPage = function(message) {
        console.log("websocket已收到", message);
        if (
          message == "cart送出訂單" ||
          message == "counter單品送餐完成" ||
          message == "counter訂單送餐完成" ||
          message == "counter訂單結帳" ||
          message == "counter訂單取消" ||
          message == "counter恢復取消單" ||
          message == "counter恢復完成單" ||
          message == "kitchen單品備餐完成" ||
          message == "kitchen單品備餐還原" ||
          message == "kitchen訂單備餐完成"
        ) {
          vm.ShowOrderStatus(vm.forhere);
        }
      };
      $.connection.hub.start().done();
    }
  },
  created() {
    this.websocketListen();
    this.checkFooterCart();
    this.ShowOrderStatus(this.forhere);
  }
};
</script>
<style src="../../assets/css/all.css" scoped></style>