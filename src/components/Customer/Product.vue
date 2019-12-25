<template>
  <div class="main">
    <div v-if="isOpenFromCustomer==false" class="open_notice">Sorry！本時段不開放預約！</div>
    <header class="hide_lg">
      <img src="@/assets/img/logo_nav.png" alt />
    </header>
    <!-- banners -->
    <div class="content">
      <swiper :options="swiperSingle" id="swiperSingle" ref="mySwiper" @someSwiperEvent="swiper()">
        <swiper-slide v-for="(item,key,index) in banners" :key="index">
          <img :src="BannerimgUrl+item" alt />
        </swiper-slide>
        <div class="swiper-pagination" slot="pagination"></div>
      </swiper>
      <!-- category -->
      <swiper
        class="swiper_nav"
        :options="swiperCategory"
        ref="mySwiper"
        @someSwiperEvent="swiper()"
        @transitionStart="cateChange"
      >
        <swiper-slide>
          <a id @click.prevent="getProducts()">全部</a>
        </swiper-slide>
        <swiper-slide v-for="(item,key,index) in categorys" :key="index">
          <a @click.prevent="getProducts(item.PCid)" :id="item.PCid">{{item.PCName}}</a>
        </swiper-slide>
      </swiper>
      <!-- products -->
      <div class="product_list">
        <div
          class="col-lg-3 col-md-4 col-6 p-0"
          @click.prevent="getProductDetail(item.Pid)"
          v-for="(item,key,index) in products"
          :key="index"
        >
          <div class="item open_popup">
            <div class="p_image">
              <img :src="ProductimgUrl+item.Img[0]" alt />
            </div>
            <div class="p_info">
              <h3>{{item.Name}}</h3>
              <p class="font_price">NT${{item.Price}}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- productdetail popup -->
    <div class="popup" :class="{'show':ShowPopup}">
      <a class="icon_close iconfont icon-weibiao45133 popup_close" @click.prevent="ClosePopup()"></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="p_slider">
          <swiper
            :options="swiperSingle"
            id="swiperSingle"
            class="swiper_product"
            ref="mySwiper"
            @someSwiperEvent="swiper()"
          >
            <swiper-slide v-for="(item,index) in productDetail[0].Img" :key="index">
              <div class="p_img_slier">
                <img :src="ProductimgUrl+item" alt />
              </div>
            </swiper-slide>
            <div class="swiper-pagination" slot="pagination"></div>
          </swiper>
        </div>
        <div class="p_content">
          <div class="p_info p-3">
            <h3>{{productDetail[0].Name}}</h3>
            <small>訂單總量超過 20 份請來電預約</small>
            <hr />
            <div class="p_text">
              <p>{{productDetail[0].Description}}</p>
            </div>
            <div class="p_choose" v-if="isOpenFromCustomer==true">
              <div class="item" v-for="(item,key,index) in Sides.slice(0,1)" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`option1-${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options[0]"
                  />
                  <span class="radiobtn"></span>
                  <label :for="`option1-${key+1}`">{{inneritem}}</label>
                </div>
              </div>
              <div class="item" v-for="(item,key,index) in Sides.slice(1,2)" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`inlineRadio2-${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options[1]"
                  />
                  <label :for="`inlineRadio2-${key+1}`">{{inneritem}}</label>
                </div>
              </div>
              <div class="item" v-for="(item,key,index) in Sides.slice(2,3)" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`inlineRadio3-${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options[2]"
                  />
                  <label :for="`inlineRadio3-${key+1}`">{{inneritem}}</label>
                </div>
              </div>
              <div class="item" v-for="(item,key,index) in Sides.slice(3,4)" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`inlineRadio4-${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options[3]"
                  />
                  <label :for="`inlineRadio4-${key+1}`">{{inneritem}}</label>
                </div>
              </div>
            </div>
          </div>
          <div class="popup_footer" v-if="isOpenFromCustomer==true">
            <div class="col-4 btn btn_round btn_white count_box">
              <a href="#" class="count_dis" @click.prevent="minusQty()">-</a>
              <a href="#" class="count_num">{{Orders.Qty}}</a>
              <a href="#" class="count_add" @click.prevent="addQty()">+</a>
            </div>
            <a class="btn btn_round btn_default" @click.prevent="addToCart()">
              <span>${{productDetail[0].Price*Orders.Qty}}</span>｜加入點菜單
            </a>
          </div>
        </div>
      </div>
    </div>
    <!---------------------------------------------- 彈跳視窗暫放div page ----------------------------------->
  </div>
</template>


<script>
// import Footer from "./Footer";
// import "swiper/dist/css/swiper.css";
import { swiper, swiperSlide } from "vue-awesome-swiper";
export default {
  data() {
    return {
      // footerActive: false,
      banners: {},
      products: {},
      modalAppear: false,
      productDetail: {
        0: {
          Description: "",
          Id: "",
          Img: "",
          Name: "",
          PCid: "",
          Price: "",
          Slide1: "",
          Slide2: "",
          Slide3: "",
          Slide4: ""
        }
      },
      categorys: {},
      swiperSingle: {
        loop: true,
        pagination: {
          el: ".swiper-pagination"
        }
      },
      swiperCategory: {
        loop: false,
        slidesPerView: 4,
        centeredSlides: true,
        centeredSlidesBounds: true,
        slideToClickedSlide: true
        // freeMode: true
      },
      ProductimgUrl: "https://lay-order.rocket-coding.com/Img/product/",
      BannerimgUrl: "https://lay-order.rocket-coding.com/Img/BannerImg/",
      ShowPopup: false,
      Orders: {
        Qty: 1,
        Pid: "",
        Options: [null, null, null, null],
        time: "",
        Img: "",
        Name: "",
        Price: ""
      },
      Cart: [],
      Sides: []
    };
  },
  computed: {
    swiper() {
      return this.$refs.mySwiper.swiper;
    }
  },
  watch: {
    ShowPopup: function() {
      if (this.ShowPopup == true) {
        document.querySelector("body").classList.add("overflow_hidden");
      } else {
        document.querySelector("body").classList.remove("overflow_hidden");
      }
    }
  },
  props: ["isOpenFromCustomer"],
  methods: {
    getBanners() {
      const vm = this;
      const url = `${process.env.APIPATH}/Banners/GetBanners`;
      this.$http.get(url).then(response => {
        // console.log("所有banner", response.data);
        vm.banners = response.data;
      });
    },
    getProducts(PCid = "") {
      let loader = this.$loading.show();
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetProduct?PCid=${PCid}`;
      this.$http.get(url).then(response => {
        // console.log('產品列表',response);
        vm.products = response.data;
        loader.hide();
      });
    },
    getProductDetail(Id, Show = true) {
      let loader = this.$loading.show();
      // console.log('產品細節ID',Id);
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetProductDetail/${Id}`;
      vm.ShowPopup = Show;
      this.$http.get(url).then(response => {
        // console.log("產品細節", response.data);
        if (response.data[0]) {
          vm.Sides = [];
          vm.modalAppear = true;
          vm.productDetail = response.data;
          vm.Orders.Pid = vm.productDetail[0].Pid;
          vm.Orders.Img = vm.productDetail[0].Img;
          vm.Orders.Name = vm.productDetail[0].Name;
          vm.Orders.Price = vm.productDetail[0].Price;
          if (vm.productDetail[0].Sides1 !== null) {
            vm.sidesfilter(vm.productDetail[0].Sides1);
          }
          if (vm.productDetail[0].Sides2 !== null) {
            vm.sidesfilter(vm.productDetail[0].Sides2);
          }
          if (vm.productDetail[0].Sides3 !== null) {
            vm.sidesfilter(vm.productDetail[0].Sides3);
          }
          if (vm.productDetail[0].Sides4 !== null) {
            vm.sidesfilter(vm.productDetail[0].Sides4);
          }
        }
        loader.hide();
      });
    },
    ClosePopup() {
      this.ShowPopup = false;
      this.productDetail = {
        0: {
          Description: "",
          Id: "",
          Img: "",
          Name: "",
          PCid: "",
          Price: "",
          Slide1: "",
          Slide2: "",
          Slide3: "",
          Slide4: ""
        }
      };
      this.Orders = {
        Qty: 1,
        Pid: "",
        Options: [],
        time: "",
        Img: "",
        Name: "",
        Price: ""
      };
    },
    sidesfilter(str) {
      let strleft = str.split(":")[0];
      let strright = str.split(":")[1].split("、");
      this.Sides.push({ name: strleft, options: strright });
    },
    getCategory() {
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetCategory`;
      this.$http.get(url).then(response => {
        // console.log("分類列表", response.data);
        vm.categorys = response.data;
      });
    },
    cateChange() {
      let nowCategoryID = document.querySelector(".swiper-slide-active a").id;
      // console.log('分類id',nowCategoryID);
      this.getProducts(nowCategoryID);
    },
    addToCart() {
      this.$swal({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: 3000,
        type: "success",
        title: "成功加入點菜單"
      });
      const vm = this;
      vm.Cart.push(vm.Orders);
      for (let i = 0; i < vm.Cart.length; i++) {
        vm.Cart[i].Options = vm.Cart[i].Options.filter(function(item) {
          return item != null;
        });
      }
      localStorage.setItem("totalcart", JSON.stringify(this.Cart));
      vm.Orders = {
        Qty: 1,
        Pid: "",
        Options: [],
        time: "",
        Img: "",
        Name: "",
        Price: ""
      };
      vm.footerNumber = JSON.parse(localStorage.getItem("totalcart")).length;
      vm.ShowPopup = false;
      this.checkFooterCart();
    },
    checkFooterCart() {
      // console.log("確認點菜單數量");
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        let cartNum = JSON.parse(localStorage.getItem("totalcart")).length;
        document.querySelector(
          "footer .footer_cart .label"
        ).innerHTML = cartNum;
      }
    },
    addQty() {
      this.Orders.Qty++;
    },
    minusQty() {
      if (this.Orders.Qty > 1) {
        this.Orders.Qty--;
      }
    },
    imgForceFirst(imgStr) {
      const firstImg = imgStr.split(",")[0];
      return firstImg;
    },
    checkCart() {
      const vm = this;
      if (JSON.parse(localStorage.getItem("totalcart"))) {
        vm.Cart = JSON.parse(localStorage.getItem("totalcart"));
      }
    }
  },
  Filters: {
    imgForceFirst(imgStr) {
      const firstImg = imgStr.split(",")[0];
      return firstImg;
    }
  },
  created() {
    this.getBanners();
    this.getProducts();
    this.getCategory();
    this.checkCart();
    // console.log('是否為開放時間',this.isOpenFromCustomer);
  },
  mounted() {
    // console.log("查看所有 swiper 方法", this.swiper);
    this.swiper.slideTo(3, 1000, false);
  }
};
</script>
<style src="../../assets/css/all.css" scope>