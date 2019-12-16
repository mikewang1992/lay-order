<template>
  <div class="main pb-2">
    <header>
      <a href class="icon iconfont icon-left" @click.prevent="goback()"></a>
      <!-- <router-link to="/Order" class="icon iconfont icon-left"></router-link> -->
      <h1>訂單詳情</h1>
    </header>
    <div class="content">
      <div class="cart_list list_sm">
        <ul>
          <li class="item" v-for="(item,index) in OrderDetail" :key="index">
            <div class="p_img">
              <img :src="ProductimgUrl+item.img" alt>
            </div>
            <div class="p_info">
              <div class="p_name">
                <h3>{{item.name}}</h3>
              </div>
              <div class="p_choose">{{item.options}}</div>
              <div class="p_choose">數量:{{item.Qty}}</div>
            </div>
            <div class="p_price">{{item.subtotal}}</div>
          </li>
          <!-- <li class="item">
              <div class="p_img">
                <img src="@/assets/img/product/1.png" alt />
              </div>
              <div class="p_info">
                <div class="p_name">
                  <h3>感覺就很甜的甜甜圈</h3>
                </div>
                <div class="p_choose">不辣,加蒜</div>
              </div>
              <div class="p_price">$500</div>
            </li>
            <li class="item">
              <div class="p_img">
                <img src="@/assets/img/product/1.png" alt />
              </div>
              <div class="p_info">
                <div class="p_name">
                  <h3>感覺就很甜的甜甜圈</h3>
                </div>
                <div class="p_choose">不辣,加蒜</div>
              </div>
              <div class="p_price">$500</div>
          </li>-->
        </ul>
      </div>
      <div class="order_list mb-4">
        <ul>
          <li class="item total">
            <h4>
              共
              <b class="color_default">{{OrderSummary[0].totalQty}}</b> 份
            </h4>
            <span>總價格: {{OrderSummary[0].totalAmount}}</span>
          </li>
          <li class="item">
            <h4>電話</h4>
            <span>{{OrderSummary[0].tel}}</span>
          </li>
          <li class="item">
            <h4>取餐人</h4>
            <span>{{OrderSummary[0].name}}</span>
          </li>
          <li class="item">
            <h4>取餐時間</h4>
            <span>{{getTime(OrderSummary[0].gettime)}}</span>
          </li>
        </ul>
      </div>
      <div class="d-flex text-center">
        <div class="col-6">
          <router-link to="/Order" class="btn btn_lg btn_gray d-block">返回列表</router-link>
        </div>
        <div class="col-6">
          <a href="#" class="btn btn_lg btn_default d-block">聯絡店家</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      OrderDetail: {},
      OrderSummary: {},
      ProductimgUrl: "https://lay-order.rocket-coding.com/Img/product/"
    };
  },
  props: ["IdFromOrder"],
  methods: {
    ShowOrderDetail(num) {
      const vm = this;
      const url = `${process.env.APIPATH}/Order/ShowOrderDetail/${num}`;
      this.$http.get(url).then(response => {
        console.log(response);
        this.OrderDetail = response.data;
      });
    },
    ShowOrderSummary(num) {
      const vm = this;
      const url = `${process.env.APIPATH}/Order/ShowOrderSummary/${num}`;
      this.$http.get(url).then(response => {
        console.log(response);
        this.OrderSummary = response.data;
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
    goback() {
      this.$emit("passevent");
    }
  },
  created() {
    console.log(this.IdFromOrder);
    this.ShowOrderSummary(this.IdFromOrder);
    this.ShowOrderDetail(this.IdFromOrder);
  }
};
</script>