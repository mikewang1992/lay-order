<template>
  <div class="main kitchen">
    <div class="menu">
      <div class="menu_lg">
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.type == ''}"
          @click.prevent="getOrderList('','')"
        >
          <img src="@/assets/img/icon_kit00.png" alt />
          <h3>所有</h3>
        </a>
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.type == 'forhere'}"
          @click.prevent="getOrderList('forhere','')"
        >
          <img src="@/assets/img/icon_kit01.png" alt />
          <h3>內用</h3>
        </a>
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.type == 'togo'}"
          @click.prevent="getOrderList('togo','')"
        >
          <img src="@/assets/img/icon_kit02.png" alt />
          <h3>外帶</h3>
        </a>
      </div>
      <div class="menu_sm">
        <!-- <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.status == 'big'}"
          @click.prevent="getOrderList('','paid')"
        >
          <img src="@/assets/img/icon_kit03.png" alt>
          <h3>大單</h3>
        </a>-->
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.status == 'paid'}"
          @click.prevent="getOrderList('','paid')"
        >
          <img src="@/assets/img/icon_kit04.png" alt />
          <h3>已完成</h3>
        </a>
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.status == 'cancel'}"
          @click.prevent="getOrderList('','cancel')"
        >
          <img src="@/assets/img/icon_kit05.png" alt />
          <h3>已取消</h3>
        </a>
      </div>
    </div>
    <div class="kitchen_list">
      <div class="order_list">
        <div
          class="item"
          v-for="(item,key,index) in orderList"
          :id="item.orderid"
          :class="{'togo':item.isTable}"
          :key="index"
        >
          <div class="title">
            <h3>
              <em class="num font_en">{{item.orderid}}.{{item.customer}}</em>
            </h3>
            <span class="time">{{getTime(item.gettime)}}</span>
          </div>
          <ul>
            <li
              v-for="(pItem,key,index) in item.product"
              :class="pItem.status"
              :key="index"
              @click="completeOrderItem(item.orderid,pItem.Id)"
            >
              <h4>{{pItem.ProductName}}</h4>
              <span class="label">{{pItem.option}}</span>
            </li>
          </ul>
          <a
            href="#"
            class="btn btn_green btn_lg"
            v-if="filterMenu.status !== 'cancel'"
            v-show="filterMenu.status !== 'paid'"
            @click="completeOrder(item.orderid)"
          >準備完成</a>
        </div>
      </div>
      <footer>
        <!-- 分頁 -->
        <div class="pages">
          <span
            class="iconfont page_last icon-fanhui"
            :class="{'none':pages.curPage==1}"
            @click="pages.curPage-=1,getOrderList(filterMenu.type, filterMenu.status, pages.curPage)"
          ></span>

          <span
            class="font_price"
            v-for="item in pages.sum"
            :key="item"
            :class="{'active':item == pages.curPage}"
            @click="pages.curPage = item,getOrderList(filterMenu.type, filterMenu.status, pages.curPage)"
          >{{item}}</span>

          <span
            class="iconfont page_next icon-youjiantou"
            :class="{'none':pages.curPage==pages.sum}"
            @click="pages.curPage+=1,getOrderList(filterMenu.type, filterMenu.status, pages.curPage)"
          ></span>
        </div>
        <!-- 時間 -->
        <!-- <div class="thisTime">
          <span>11</span>
          <span>30</span>
        </div>-->
      </footer>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      filterMenu: {
        type: "",
        status: ""
      },
      pages: {
        curPage: 1,
        sum: ""
      },
      orderList: "",
      thisOrderId: ""
    };
  },
  methods: {
    getOrderList(type, status, pages = "1") {
      let loader = this.$loading.show();
      const vm = this;
      this.filterMenu.type = type;
      this.filterMenu.status = status;
      const url = `${process.env.APIPATH}/Kitchen/ShowOrderList?type=${type}&status=${status}&page=${pages}`;
      this.$http.get(url).then(response => {
        loader.hide();
        vm.orderList = response.data;
        // console.log("產品列表", response.data);
        if (response.data == "") {
          this.$swal({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            type: "info",
            title: "本分類目前沒產品唷"
          });
        }
      });
      this.getPages();
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
    getPages() {
      // console.log("頁數");
      const vm = this;
      const url = `${process.env.APIPATH}/Kitchen/TotalPage?type=${this.filterMenu.type}&status=${this.filterMenu.status}`;
      this.$http.get(url).then(response => {
        this.pages.sum = response.data;
      });
    },
    completeOrderItem(Oid, Pid) {
      console.log("單品備餐：", "產品ID", Oid, "產品ID", Pid);
      this.$http
        .get(
          `${process.env.APIPATH}/Kitchen/CompleteOrderItem?Oid=${Oid}&id=${Pid}`
        )
        .then(response => {
          console.log(response.data);
          this.$swal({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            type: "success",
            title: "本產品備餐完成"
          });
          this.getOrderList(this.filterMenu.type, this.filterMenu.status);
        });
    },
    completeOrder(Oid) {
      console.log("整單備餐完成：", Oid);
      this.$http
        .get(`${process.env.APIPATH}/Kitchen/CompleteOrder/${Oid}`)
        .then(response => {
          console.log(response.data);
          this.$swal({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            type: "success",
            title: "本產品備餐完成"
          });
          this.getOrderList(this.filterMenu.type, this.filterMenu.status);
        });
    },
    websocketbtn(par = "kitchen") {
      console.log("websocket送出", par);
      $.connection.chatHub.server.send(par);
    },
    websocketListen() {
      const vm = this;
      $.connection.chatHub.client.addNewMessageToPage = function(message) {
        console.log("websocket已收到", message);
        console.log(vm.filterMenu.type);
        console.log(vm.filterMenu.status);
        console.log(vm.pages.curPage);
        vm.getOrderList(
          vm.filterMenu.type,
          vm.filterMenu.status,
          vm.pages.curPage
        );
      };
      $.connection.hub.start().done();
    }
  },
  created() {
    this.getOrderList("", "");
    this.websocketListen();
  }
};
</script>

<style src="../../assets/css/store.css" scoped></style>