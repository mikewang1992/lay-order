<template>
  <div class="page member">
    <!-- <footer>
      <ul>
        <li>
          <a href="index.html">
            <img src="@/assets/img/icon_footer01.png" alt />
            <p>菜單</p>
          </a>
        </li>
        <li>
          <a href="cart.html">
            <img src="@/assets/img/icon_footer02.png" alt />
            <p>點菜單</p>
          </a>
        </li>
        <li>
          <a href="order.html">
            <img src="@/assets/img/icon_footer03.png" alt />
            <p>訂單狀態</p>
          </a>
        </li>
        <li class="active">
          <a href="member_sale.html">
            <img src="@/assets/img/icon_footer04.png" alt />
            <p>會員資訊</p>
          </a>
        </li>
      </ul>
    </footer>-->
    <div class="main col-md-6 col-xl-4">
      <header>
        <h1 class="title">會員資訊</h1>
      </header>
      <div class="content">
        <div class="row">
          <div class="col-sm-8 offset-sm-2">
            <ul class="nav_group mb-3">
              <li>
                <router-link to="/coupon" class="active">我的優惠券</router-link>
              </li>
              <li>
                <router-link to="/member">個人資訊</router-link>
              </li>
            </ul>
          </div>
        </div>
        <div class="card_list">
          <div class="item" v-for="(item,key,index) in Vouchers" :key="index">
            <h2>{{item.Title}}</h2>
            <p>{{item.Content}}</p>
            <p>{{item.StartTime}}</p>
            <p>{{item.EndTime}}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      Vouchers: {}
    };
  },
  methods: {
    getVouchers() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Voucher`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.Vouchers = response.data;
      });
    }
  },
  created() {
    this.getVouchers();
  }
};
</script>