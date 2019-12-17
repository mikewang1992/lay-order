<template>
  <div class="main member login_content col-md-6 col-xl-4">
    <header>
      <h1 class="title">會員資訊</h1>
    </header>
    <div class="content">
      <div class="row">
        <div class="col-sm-12">
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
</template>
<script>
export default {
  data() {
    return {
      Vouchers: [],
      footerNumber: 0
    };
  },
  methods: {
    getVouchers() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Voucher`;
      this.$http.get(url).then(response => {
        console.log(response);
        if (response.data.length === 0) {
          // this.$swal("目前沒有任何優惠券", "", "warning");
          this.$swal({
            title: "目前沒有任何優惠券",
            text: "聯繫小編拿更多優惠券",
            type: "warning",
            showCancelButton: false,
            confirmButtonText: "個人資訊",
            reverseButtons: true
          }).then(result => {
            this.$router.push({ name: "Member" });
          });
        } else {
          vm.Vouchers = response.data;
        }
      });
    },
    checkFooterCart() {
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
      }
    }
  },
  created() {
    this.getVouchers();
    this.checkFooterCart();
  }
};
</script>