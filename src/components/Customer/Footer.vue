<template>
  <footer>
    <div class="nav_logo">
      <img src="@/assets/img/logo_nav.png" alt />
    </div>
    <ul>
      <li :class="{'active':footerActive == 'pageHome'}">
        <router-link to="/Product">
          <img src="@/assets/img/icon_footer01.png" alt />
          <p>菜單</p>
        </router-link>
      </li>
      <li :class="{'active':footerActive=='pageOrder'}">
        <router-link to="/cart">
          <img src="@/assets/img/icon_footer02.png" alt />
          <p>點菜單</p>
        </router-link>
      </li>
      <li :class="{'active':footerActive=='pageStatus'}">
        <router-link to="/order">
          <img src="@/assets/img/icon_footer03.png" alt />
          <p>訂單狀態</p>
        </router-link>
      </li>
      <li :class="{'active':footerActive=='pageMember'}">
        <router-link to="/member">
          <img src="@/assets/img/icon_footer04.png" alt />
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
      footerActive: ''
    };
  },
  methods: {
    urltest() {
      console.log();
    }
  },
  created() {
    this.urltest();
  }
};
</script>