<template>
  <div class="main pb-2">
    <header>
      <h1>點餐成功</h1>
    </header>
    <div class="content">
      <div class="result out">
        <img src="@/assets/img/result_ok.png" alt />
        <div class="number">{{this.OrderCodeFromCart}}</div>
        <p class="text-center mb-4">
          謝謝您的訂餐！
          <br />請於您指定的時間憑
          <span class="color_default">取餐編號</span>來店取餐
          <br />若需調整訂單請撥打電話與我們聯繫！
        </p>
      </div>
      <div class="order_list mb-4">
        <ul>
          <li class="item total">
            <h4>
              共
              <b class="color_default">{{OrderSummary[0].totalQty}}</b> 份
            </h4>
            <span>{{OrderSummary[0].totalAmout}}</span>
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
          <a :href="`tel:${ShopPhone}`" class="btn btn_lg btn_yellow d-block">撥打電話</a>
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
    this.ShowOrderSummary(this.OrderCodeFromCart);
    console.log(this.OrderCodeFromCart);
    this.GetTel();
  }
};
</script>