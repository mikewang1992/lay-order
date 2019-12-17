<template>
  <div class="main pb-2">
    <header>
      <h1>點餐成功</h1>
    </header>
    <div class="content">
      <div class="result">
        <img src="@/assets/img/result_in.svg" alt />
        <div class="number in">{{OrderSummary[0].name}}</div>
        <p class="text-center mb-4">
          <b class="color_default">點餐完成，我們將盡快製作您的餐點</b>
          <br />用餐完請至櫃檯結帳^＿^
          <br />邀請您加入線上點餐系統
          <br />節省預約時間還可以取得特別優惠歐！
        </p>
      </div>
      <div class="order_list mb-4">
        <ul>
          <li class="item total p-0 align-items-center">
            <h4>
              共
              <b class="color_default">{{OrderSummary[0].totalQty}}</b> 份
            </h4>
            <span>${{OrderSummary[0].totalAmount}}</span>
          </li>
        </ul>
      </div>
      <div class="d-flex text-center">
        <div class="col-6">
          <a :href="`tel:${ShopPhone}`" class="btn btn_yellow btn_lg d-block">撥打電話</a>
        </div>
        <div class="col-6">
          <router-link to="/order" class="btn btn_lg btn_default d-block">訂單狀態</router-link>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  data() {
    return {
      OrderSummary: {},
      ShopPhone: ""
    };
  },
  props: ["OrderCodeFromCart"],
  methods: {
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
    }
  },
  created() {
    this.ShowOrderSummary(this.OrderCodeFromCart);
    console.log(this.OrderCodeFromCart);
    this.GetTel();
  }
};
</script>