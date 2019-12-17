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
          <span class="label" v-if="footerNumber<20">{{footerNumber}}</span>
        </router-link>
      </li>
      <li :class="{'active':pageActive=='order'}">
        <router-link to="/order">
          <img src="@/assets/img/icon_footer03.png" alt>
          <p>訂單狀態</p>
        </router-link>
      </li>
      <li :class="{'active':pageActive=='member'}">
        <router-link to="/member">
          <img src="@/assets/img/icon_footer04.png" alt>
          <p>會員中心</p>
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
  methods: {
    footerActive() {
      // 判斷目前頁面
      let vm = this;
      let nowURL = location.href.split("#/")[1];
      this.pageActive = nowURL;
      if (nowURL == "") {
        nowURL = "home";
        vm.pageActive = nowURL;
      } else {
        vm.pageActive = nowURL;
      }
    },
    checkFooterCart() {
      // console.log("確認點菜單數量");
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
      }
    },
    checkTable() {
      const url = `${process.env.APIPATH}/Accounts/IsTable`;
      this.$http.get(url).then(response => {
        if (response.data === "外帶") {
          console.log("外帶");
        } else {
          console.log("內用");
        }
      });
    },
    changePageClass() {
      // console.log("切換頁面class");
      if (this.pageActive == "login" || this.pageActive == "member") {
        console.log("login新增css");
        document.querySelector(".page").classList.add("login");
      } else {
        document.querySelector(".page").classList.remove("login");
      }
      if (this.pageActive == "cart"|| this.pageActive == "Cart/ResultOut"|| this.pageActive == "Cart/ResultIn") {
        document.querySelector("footer ul").classList.add("d-none");
      } else {
        document.querySelector("footer ul").classList.add("d-flex");
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
    this.checkTable();
  },
  mounted() {
    this.changePageClass();
  }
};
</script>