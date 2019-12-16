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
          <img src="@/assets/img/icon_kit01.png" alt>
          <h3>所有</h3>
        </a>
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.type == 'forhere'}"
          @click.prevent="getOrderList('forhere','')"
        >
          <img src="@/assets/img/icon_kit01.png" alt>
          <h3>內用</h3>
        </a>
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.type == 'togo'}"
          @click.prevent="getOrderList('togo','')"
        >
          <img src="@/assets/img/icon_kit02.png" alt>
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
          <img src="@/assets/img/icon_kit04.png" alt>
          <h3>已完成</h3>
        </a>
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.status == 'cancel'}"
          @click.prevent="getOrderList('','cancel')"
        >
          <img src="@/assets/img/icon_kit05.png" alt>
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
              <!-- 外帶 -->
              <em class="num font_en">{{item.orderid}}.{{item.customer}}</em>
            </h3>
            <span class="time">{{getTime(item.gettime)}}</span>
          </div>
          <ul>
            <li v-for="(pItem,key,index) in item.product" :key="index">
              <h4>{{pItem.ProductName}}</h4>
              <span class="label">{{pItem.option}}</span>
            </li>
          </ul>
          <a href="#" class="btn btn_green btn_lg">準備完成</a>
        </div>
      </div>
      <footer>
        <div class="pages">
          <span class="iconfont icon-fanhui"></span>
          <span class="active">1</span>
          <span>2</span>
          <span>3</span>
          <span>4</span>
          <span>5</span>
          <span class="iconfont icon-youjiantou"></span>
        </div>
        <div class="thisTime">
          <span>11</span>
          <span>30</span>
        </div>
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
      orderList: ""
    };
  },
  methods: {
    getOrderList(type, status, pages = "1") {
      console.log("123");
      const vm = this;
      this.filterMenu.type = type;
      this.filterMenu.status = status;
      const url = `${
        process.env.APIPATH
      }/Kitchen/ShowOrderList?type=${type}&status=${status}&page=${pages}`;
      this.$http.get(url).then(response => {
        vm.orderList = response.data;
        console.log(response.data);
      });
      // this.getPages();
    },
    getTime(time) {
      const date = new Date(time);
      let m = date.getMinutes();
      if (m < 10) {
        m = "0" + m;
      }
      const newTime = `${date.getHours()}:${m}`;
      return newTime;
    }
  },
  created() {
    this.getOrderList("forhere", "");
  }
};
</script>

<style scope>
@import "../../assets/css/store.css";
</style>