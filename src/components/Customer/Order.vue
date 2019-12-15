<template>
  <div class="page">
    <router-view :IdFromOrder="Id"></router-view>
    <footer>
      <div class="nav_logo">
        <img src="@/assets/img/logo_nav.png" alt />
      </div>
      <ul>
        <!-- <li :class="{'active':footerActive}"> -->
        <li>
          <router-link to="/">
            <img src="@/assets/img/icon_footer01.png" alt />
            <p>菜單</p>
          </router-link>
        </li>
        <li>
          <router-link to="/cart">
            <img src="@/assets/img/icon_footer02.png" alt />
            <p>
              點菜單
              <span v-if="footerNumber>0">:{{footerNumber}}項</span>
            </p>
          </router-link>
        </li>
        <li>
          <router-link to="/order" class="active">
            <img src="@/assets/img/icon_footer03.png" alt />
            <p>訂單狀態</p>
          </router-link>
        </li>
        <li>
          <router-link to="/member">
            <img src="@/assets/img/icon_footer04.png" alt />
            <p>會員資訊</p>
          </router-link>
        </li>
      </ul>
    </footer>
    <div class="main pb-2">
      <header>
        <h1>訂單狀態</h1>
      </header>
      <div class="content">
        <div class="cart_list status_list">
          <ul>
            <a>
              <li class="item">
                <div class="p_status bg_default">
                  <h4>製作中</h4>
                </div>
                <div class="p_info">
                  <div class="p_date">
                    <h3>2019/12/01 12:00 取餐</h3>
                  </div>
                  <div class="p_price">$500</div>
                </div>
                <div class="icon_right iconfont icon-right"></div>
              </li>
            </a>
            <a>
              <li class="item">
                <div class="p_status bg_yellow">
                  <h4>待取餐</h4>
                </div>
                <div class="p_info">
                  <div class="p_date">
                    <h3>2019/12/01 12:00 取餐</h3>
                  </div>
                  <div class="p_price">$500</div>
                </div>
                <div class="icon_right iconfont icon-right"></div>
              </li>
            </a>
            <a>
              <li class="item">
                <div class="p_status bg_gray">
                  <h4>已完成</h4>
                </div>
                <div class="p_info">
                  <div class="p_date">
                    <h3>2019/12/01 12:00 取餐</h3>
                  </div>
                  <div class="p_price">$500</div>
                </div>
                <div class="icon_right iconfont icon-right"></div>
              </li>
            </a>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  data() {
    return {
      Id: 1,
      footerNumber: 0,
      OrderStatus: {}
    };
  },
  methods: {
    checkFooterCart() {
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
      }
    },
    ShowOrderStatus() {
      const vm = this;
      const url = `${process.env.APIPATH}/Order/ShowOrderStatus`;
      this.$http.get(url).then(response => {
        console.log(response);
        this.OrderStatus = response.data;
      });
    }
  },
  created() {
    this.checkFooterCart;
  }
};
</script>