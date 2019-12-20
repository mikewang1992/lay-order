<template>
  <div class="main pb-2">
    <header>
      <a href class="icon iconfont icon-left" @click.prevent="goback()"></a>
      <h1>訂單詳情</h1>
    </header>
    <div class="content">
      <div class="cart_list list_sm col-md-6 offset-md-3">
        <ul>
          <li class="item" v-for="(item,index) in OrderDetail" :key="index">
            <div class="p_img">
              <img :src="ProductimgUrl+item.img" alt />
            </div>
            <div class="p_info">
              <div class="p_name">
                <h3>{{item.name}} X {{item.Qty}}</h3>
              </div>
              <div class="p_choose">{{item.options}}</div>
            </div>
            <div class="p_price font_price">${{item.subtotal}}</div>
          </li>
        </ul>
      </div>
      <div class="order_list mb-4 col-md-6 offset-md-3">
        <ul>
          <li class="item total">
            <h4>
              共
              <b class="color_default">{{OrderSummary[0].totalQty}}</b> 份
            </h4>
            <span class="font_en">TOTAL ${{OrderSummary[0].totalAmount}}</span>
          </li>
          <li class="item">
            <h4>電話</h4>
            <span class="font_en">{{OrderSummary[0].tel}}</span>
          </li>
          <li class="item">
            <h4>取餐人</h4>
            <span>{{OrderSummary[0].name}}</span>
          </li>
          <li class="item">
            <h4>取餐時間</h4>
            <span class="font_en">{{getTime(OrderSummary[0].gettime)}}</span>
          </li>
        </ul>
      </div>
      <div class="d-flex text-center pb-5 mb-3 col-md-6 offset-md-3">
        <div class="w-100">
          <a href class="btn btn_lg btn_gray d-block mr-1 ml-1" @click.prevent="goback()">返回列表</a>
        </div>
        <div class="w-100" v-if="!forhere">
          <a :href="`tel:${ShopPhone}`" class="btn btn_lg btn_default d-block ml-1 mr-1">聯絡店家</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      OrderDetail: [],
      OrderSummary: [{ totalQty: 1 }],
      ShopPhone: "",
      ProductimgUrl: "https://lay-order.rocket-coding.com/Img/product/"
    };
  },
  props: ["IdFromOrder","forhere"],
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
    GetTel() {
      const vm = this;
      const url = `${process.env.APIPATH}/Company/GetTel`;
      this.$http.get(url).then(response => {
        console.log(response);
        this.ShopPhone = response.data;
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
    this.ShowOrderDetail(this.IdFromOrder);
    this.ShowOrderSummary(this.IdFromOrder);
    this.GetTel();
  },
  mounted() {}
};
</script>
<style src="../../assets/css/all.css" scoped></style>