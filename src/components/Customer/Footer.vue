<template>
  <footer>
    <div class="nav_logo">
      <img src="@/assets/img/logo_nav.png" alt>
    </div>
    <ul>
      <li :class="{'active':pageActive=='home'}">
        <router-link to="/Product">
          <img src="@/assets/img/icon_footer01.png" alt>
          <p>菜單</p>
        </router-link>
      </li>
      <li :class="{'active':pageActive=='cart'}" class="footer_cart">
        <router-link to="/cart">
          <img src="@/assets/img/icon_footer02.png" alt>
          <p>點菜單</p>
          <span class="label font_price" v-if="footerNumber<20">{{footerNumber}}</span>
        </router-link>
      </li>
      <li :class="{'active':pageActive=='order'}">
        <router-link to="/order">
          <img src="@/assets/img/icon_footer03.png" alt>
          <p>訂單狀態</p>
        </router-link>
      </li>
      <li :class="{'active':pageActive=='member'|| pageActive=='coupon'}" v-if="!forhere">
        <router-link to="/member">
          <img src="@/assets/img/icon_footer04.png" alt>
          <p>會員中心</p>
        </router-link>
      </li>
      <li class="forhere" v-if="forhere">
        <router-link to="/">
          <h3 class="fz_en">{{tableNum}}</h3>
          <p>內用桌號</p>
        </router-link>
      </li>
    </ul>
  </footer>
</template>

<script>
export default {
  data() {
    return {
      pageActive: "",
      footerNumber: ""
    };
  },
  props: ["forhere", "tableNum"],
  methods: {
    footerActive() {
      // 判斷目前頁面
      let vm = this;
      let nowURL = location.href;
      var index = nowURL.lastIndexOf("/");
      nowURL = nowURL.substring(index + 1, nowURL.length);
      console.log('目前頁面：',nowURL)
      if (nowURL == "") {
        nowURL = "home";
        vm.pageActive = nowURL;
      } else {
        vm.pageActive = nowURL;
      }
    },
    checkFooterCart() {
      // console.log("Footer確認點菜單數量");
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
      } else {
        this.footerNumber = 0;
      }
    },
    changePageClass() {
      console.log("切換頁面class，現在是", this.pageActive);
      if (
        this.pageActive == "login" ||
        this.pageActive == "member" ||
        this.pageActive == "coupon" ||
        this.pageActive == "register"
      ) {
        console.log("login新增css");
        document.querySelector(".page").classList.add("login");
      } else {
        document.querySelector(".page").classList.remove("login");
      }
      if (
        this.pageActive == "cart" ||
        this.pageActive == "Cart/ResultOut" ||
        this.pageActive == "Cart/ResultIn"
      ) {
        // console.log('隱藏footer')
        document.querySelector("footer ul").classList.remove("d-flex");
        document.querySelector("footer ul").classList.add("d-none");
      } else {
        // console.log('顯示footer')
        document.querySelector("footer ul").classList.add("d-flex");
        document.querySelector("footer ul").classList.remove("d-none");
      }
    }
  },
  watch: {
    $route(to, from) {
      this.footerActive();
      this.checkFooterCart();
      this.changePageClass();
    }
  },
  created() {
    this.footerActive();
    this.checkFooterCart();
  },
  mounted() {
    this.changePageClass();
  }
};
</script>
<style src="../../assets/css/all.css" scoped></style>