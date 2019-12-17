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
      <li :class="{'active':pageActive=='cart'}">
        <router-link to="/cart">
          <img src="@/assets/img/icon_footer02.png" alt>
          <p>
            點菜單
            <!-- <span v-if="footerNumber>0">:{{footerNumber}}項</span> -->
          </p>
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
      pageActive: ""
    };
  },
  methods: {
    footerActive() {
      let vm = this;
      let nowURL = location.href.split("#/")[1];
      this.pageActive = nowURL;
      if (nowURL == "") {
        nowURL = "home";
        vm.pageActive = nowURL;
        console.log(nowURL);
      } else {
        vm.pageActive = nowURL;
        console.log(nowURL);
      }
    },
    checkFooterCart() {
      // console.log('123')
      // if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
      //   this.footerNumber = JSON.parse(
      //     localStorage.getItem("totalcart")
      //   ).length;
      // }
    }
  },
  watch: {
    '$route' (to, from) {
      this.footerActive()
    }
  },
  created() {
    this.footerActive();
    this.checkFooterCart();
  }
};
</script>