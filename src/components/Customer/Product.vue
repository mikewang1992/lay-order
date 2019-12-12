<template>
  <div class="page">
    <Footer></Footer>
    <!-- <footer>
            <ul>
                <li class="active">
                    <a href="index.html">
                        <img src="img/icon_footer01.png" alt="">
                        <p>菜單</p>
                    </a>
                </li>
                <li>
                    <a href="cart.html">
                        <span class="label">5</span>
                        <img src="img/icon_footer02.png" alt="">
                        <p>點菜單</p>
                    </a>
                </li>
                <li>
                    <a href="order.html">
                        <img src="img/icon_footer03.png" alt="">
                        <p>訂單狀態</p>
                    </a>
                </li>
                <li>
                    <a href="member_sale.html">
                        <img src="img/icon_footer04.png" alt="">
                        <p>會員資訊</p>
                    </a>
                </li>
            </ul>
    </footer>-->
    <div class="main">
      <header>
        <img src="@/assets/img/logo_nav.png" alt />
      </header>
      <div class="content">
        <swiper :options="swiperOption" ref="mySwiper" @someSwiperEvent="swiper()">
          <!-- slides -->
          <swiper-slide>
            <img src="@/assets/img/banner.jpg" alt />
          </swiper-slide>
          <swiper-slide>
            <img src="@/assets/img/banner.jpg" alt />
          </swiper-slide>
          <swiper-slide>
            <img src="@/assets/img/banner.jpg" alt />
          </swiper-slide>
          <swiper-slide>
            <img src="@/assets/img/banner.jpg" alt />
          </swiper-slide>
          <!-- Optional controls -->
          <div class="swiper-pagination" slot="pagination"></div>
        </swiper>
        <swiper
          class="swiper_nav"
          :options="swiperOption2"
          ref="mySwiper"
          @someSwiperEvent="swiper()"
        >
          <!-- slides -->
          <swiper-slide>分類1</swiper-slide>
          <swiper-slide>分類2</swiper-slide>
          <swiper-slide>分類3</swiper-slide>
          <swiper-slide>分類4</swiper-slide>
        </swiper>
        <!-- <div class="swiper-container swiper_banner">
          <div class="swiper-wrapper">
            <div class="swiper-slide">
              <img src="@/assets/img/banner.jpg" alt />
            </div>
            <div class="swiper-slide">
              <img src="@/assets/img/banner.jpg" alt />
            </div>
            <div class="swiper-slide">
              <img src="@/assets/img/banner.jpg" alt />
            </div>
            <div class="swiper-slide">
              <img src="@/assets/img/banner.jpg" alt />
            </div>
          </div>
          <div class="swiper-pagination"></div>
        </div>
        
        <div class="swiper-container swiper_nav">
          <div class="swiper-wrapper">
            <div class="swiper-slide">分類 1</div>
            <div class="swiper-slide">分類 2</div>
            <div class="swiper-slide">分類 3</div>
            <div class="swiper-slide">分類 4</div>
            <div class="swiper-slide">分類 5</div>
            <div class="swiper-slide">分類 6</div>
            <div class="swiper-slide">分類 7</div>
            <div class="swiper-slide">分類 8</div>
          </div>
        </div>-->
        <div class="product_list">
          <div class="col-lg-3 col-md-4 col-6 p-0" @click.prevent="openPopup()">
            <div class="item open_popup">
              <div class="p_image">
                <img src="@/assets/img/product/1.png" alt />
              </div>
              <div class="p_info">
                <h3>產品名稱</h3>
                <p>NT$50</p>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-md-4 col-6 p-0">
            <div class="item open_popup">
              <div class="p_image">
                <img src="@/assets/img/product/1.png" alt />
              </div>
              <div class="p_info">
                <h3>產品名稱</h3>
                <p>NT$50</p>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-md-4 col-6 p-0">
            <div class="item open_popup">
              <div class="p_image">
                <img src="@/assets/img/product/1.png" alt />
              </div>
              <div class="p_info">
                <h3>產品名稱</h3>
                <p>NT$50</p>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-md-4 col-6 p-0">
            <div class="item open_popup">
              <div class="p_image">
                <img src="@/assets/img/product/1.png" alt />
              </div>
              <div class="p_info">
                <h3>產品名稱</h3>
                <p>NT$50</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!---------------------------------------------- 彈跳視窗暫放div page ----------------------------------->
    <div class="popup">
      <a
        href="#"
        class="icon_close iconfont icon-weibiao45133 popup_close"
        @click.prevent="hidePopup()"
      ></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="p_slider">
          <swiper
            :options="swiperOption"
            class="swiper_product"
            ref="mySwiper"
            @someSwiperEvent="swiper()"
          >
            <!-- slides -->
            <swiper-slide>
              <img src="@/assets/img/product/1.png" alt />
            </swiper-slide>
            <swiper-slide>
              <img src="@/assets/img/product/2.png" alt />
            </swiper-slide>
            <swiper-slide>
              <img src="@/assets/img/product/3.png" alt />
            </swiper-slide>
            <swiper-slide>
              <img src="@/assets/img/product/4.png" alt />
            </swiper-slide>
            <!-- Optional controls -->
            <div class="swiper-pagination" slot="pagination"></div>
          </swiper>
          <!-- <div class="swiper-container swiper_product">
            <div class="swiper-wrapper">
              <div class="swiper-slide">
                <img src="img/product/1.png" alt />
              </div>
              <div class="swiper-slide">
                <img src="img/product/2.png" alt />
              </div>
              <div class="swiper-slide">
                <img src="img/product/3.png" alt />
              </div>
              <div class="swiper-slide">
                <img src="img/product/4.png" alt />
              </div>
            </div>
            <div class="swiper-pagination"></div>
          </div>-->
        </div>
        <div class="p_content">
          <div class="p_info p-3">
            <h3>看起來就很甜的甜甜圈</h3>
            <small>訂單總量超過 20 份請來電預約</small>
            <hr />
            <div class="p_text">
              <p>雖然很甜但是很好吃，而且長得很可愛，建議你可以吃吃看或買來裝可愛也不錯唷！</p>
            </div>
            <div class="p_choose">
              <div class="item">
                <h5>冰塊</h5>
                <div class="form-check">
                  <input class="form-check-input" type="radio" name="ice" id="inlineRadio1" value />
                  <label for="inlineRadio1">微冰</label>
                </div>
                <div class="form-check">
                  <input class="form-check-input" type="radio" name="ice" id="inlineRadio2" value />
                  <label for="inlineRadio2">少冰</label>
                </div>
              </div>
              <div class="item">
                <h5>甜度</h5>
                <div class="form-check">
                  <input class="form-check-input" type="radio" name="sugar" id="inlineRadio3" value />
                  <label for="inlineRadio3">微糖</label>
                </div>
                <div class="form-check">
                  <input class="form-check-input" type="radio" name="sugar" id="inlineRadio4" value />
                  <label for="inlineRadio4">少糖</label>
                </div>
                <div class="form-check">
                  <input class="form-check-input" type="radio" name="sugar" id="inlineRadio5" value />
                  <label for="inlineRadio5">半糖</label>
                </div>
              </div>
            </div>
          </div>
          <div class="popup_footer">
            <div class="btn btn_round btn_white count_box">
              <a href="#" class="count_dis">-</a>
              <a href="#" class="count_num">2</a>
              <a href="#" class="count_add">+</a>
            </div>
            <a href="#" class="btn btn_round btn_default">
              <span>$120</span>｜加入點菜單
            </a>
          </div>
        </div>
      </div>
    </div>
    <!---------------------------------------------- 彈跳視窗暫放div page ----------------------------------->
  </div>
  <!-- <div>
    <p v-if="isOpenFromCustomer" style="background:green">1.IsOpen是否營業時間:現在不開放預約</p>
    <h2>product</h2>
    <h3>banners</h3>
    <ul>
      <li v-for="(item,key,index) in banners" :key="index">{{item}}</li>
    </ul>
    <h3>categorys</h3>
    <ul>
      <li @click.prevent="getProducts()">全部</li>
      <li
        v-for="(item,key,index) in categorys"
        :key="index"
        @click.prevent="getProducts(item.Id)"
      >{{item.PCName}}</li>
    </ul>
    <h3>products</h3>
    <ul>
      <li
        v-for="(item,key,index) in products"
        :key="index"
        @click.prevent="getProductDetail(item.Id)"
      >{{item}}</li>
    </ul>
    <div v-if="modalAppear" style="background:grey">
      <button @click.prevent="modalAppear=false">關掉</button>
      <p v-for="(item,key,index) in productDetail" :key="index">{{item}}</p>
      <p v-if="isOpenFromCustomer==false" style="background:blue">加入購物車</p>
    </div>
  </div>-->
</template>
<script>
import Footer from "./Footer";
import "swiper/dist/css/swiper.css";
import { swiper, swiperSlide } from "vue-awesome-swiper";
export default {
  data() {
    return {
      banners: {},
      products: {},
      modalAppear: false,
      productDetail: {},
      categorys: {},
      swiperOption: {
        loop: true,
        pagination: {
          el: ".swiper-pagination"
        }
      },
      swiperOption2: {
        loop: false,
        slidesPerView: 4,
        centeredSlides: true,
        centeredSlidesBounds: true,
        slideToClickedSlide: true,
        freeMode: true
      }
    };
  },
  components: {
    Footer
  },
  computed: {
    swiper() {
      return this.$refs.mySwiper.swiper;
    }
  },
  props: ["isOpenFromCustomer"],
  methods: {
    getBanners() {
      const vm = this;
      const url = `${process.env.APIPATH}/Banners/GetBanners`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.banners = response.data;
      });
    },
    getProducts(PCid = "") {
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetProduct?PCid=${PCid}`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.products = response.data;
      });
    },
    getProductDetail(Id) {
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetProductDetail/${Id}`;
      this.$http.get(url).then(response => {
        console.log(response);
        if (response.data[0]) {
          vm.modalAppear = true;
          vm.productDetail = response.data;
        }
      });
    },
    getCategory() {
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetCategory`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.categorys = response.data;
      });
    },
    openPopup() {
      let popup = document.querySelector(".popup");
      popup.style.visibility = "visible";
      popup.style.opacity = "1";
      popup.style.transform = "scale(1)";
      document.querySelector("body").style.overflow = "hidden";
    },
    hidePopup() {
      let popup = document.querySelector(".popup");
      popup.style.visibility = "hidden";
      popup.style.opacity = "0";
      popup.style.transform = "scale(0)";
      document.querySelector("body").style.overflow = "scroll";
    }
  },
  created() {
    this.getBanners();
    this.getProducts();
    this.getCategory();
    console.log(this.isOpenFromCustomer);
  },
  mounted() {
    // current swiper instance
    // 然后你就可以使用当前上下文内的swiper对象去做你想做的事了
    console.log("this is current swiper instance object", this.swiper);
    this.swiper.slideTo(3, 1000, false);
  }
};
</script>
<style >
</style>