<template>
  <div class="main">
    <div v-if="isOpenFromCustomer" class="open_notice">Sorry！本時段不開放預約！</div>
    <header class="hide_lg">
      <img src="@/assets/img/logo_nav.png" alt>
    </header>
    <!-- banners -->
    <div class="content">
      <swiper :options="swiperOption" ref="mySwiper" @someSwiperEvent="swiper()">
        <swiper-slide v-for="(item,key,index) in banners" :key="index">
          <img :src="BannerimgUrl+item" alt>
        </swiper-slide>
        <div class="swiper-pagination" slot="pagination"></div>
      </swiper>
      <!-- category -->
      <swiper
        class="swiper_nav"
        :options="swiperOption2"
        ref="mySwiper"
        @someSwiperEvent="swiper()"
      >
        <swiper-slide>
          <a @click.prevent="getProducts()">全部</a>
        </swiper-slide>
        <swiper-slide v-for="(item,key,index) in categorys" :key="index">
          <a @click.prevent="getProducts(item.PCid)">{{item.PCName}}</a>
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
              <img :src="ProductimgUrl+item.Img[0]" alt>
            </div>
            <div class="p_info">
              <h3>{{item.Name}}</h3>
              <p>NT${{item.Price}}</p>
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
            :options="swiperOption"
            class="swiper_product"
            ref="mySwiper"
            @someSwiperEvent="swiper()"
          >
            <swiper-slide v-for="(item,index) in productDetail[0].Img" :key="index">
              <img :src="ProductimgUrl+item" alt>
            </swiper-slide>
            <div class="swiper-pagination" slot="pagination"></div>
          </swiper>
        </div>
        <div class="p_content">
          <div class="p_info p-3">
            <h3>{{productDetail[0].Name}}</h3>
            <small>訂單總量超過 20 份請來電預約</small>
            <hr>
            <div class="p_text">
              <p>{{productDetail[0].Description}}</p>
            </div>
            <div class="p_choose">
              <!-- <div class="item" v-for="(item,key,index) in Sides" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`inlineRadio${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options"
                  />
                  <label :for="`inlineRadio${key+1}`">{{inneritem}}</label>
                </div>
              </div>-->
              <div class="item" v-for="(item,key,index) in Sides.slice(0,1)" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`inlineRadio${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options[0]"
                  >
                  <label :for="`inlineRadio${key+1}`">{{inneritem}}</label>
                </div>
              </div>
              <div class="item" v-for="(item,key,index) in Sides.slice(1,2)" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`inlineRadio${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options[1]"
                  >
                  <label :for="`inlineRadio${key+1}`">{{inneritem}}</label>
                </div>
              </div>
              <div class="item" v-for="(item,key,index) in Sides.slice(2,3)" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`inlineRadio${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options[2]"
                  >
                  <label :for="`inlineRadio${key+1}`">{{inneritem}}</label>
                </div>
              </div>
              <div class="item" v-for="(item,key,index) in Sides.slice(3,4)" :key="index">
                <h5>{{item.name}}</h5>
                <div class="form-check" v-for="(inneritem,key,index) in item.options" :key="index">
                  <input
                    class="form-check-input"
                    type="radio"
                    :name="item.name"
                    :id="`inlineRadio${key+1}`"
                    :value="inneritem"
                    v-model="Orders.Options[3]"
                  >
                  <label :for="`inlineRadio${key+1}`">{{inneritem}}</label>
                </div>
              </div>
              <!-- <div class="item" v-if="productDetail[0].Sides2">
                <h5>{{productDetail[0].Sides2}}</h5>
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
              <div class="item" v-if="productDetail[0].Sides3">
                <h5>{{productDetail[0].Sides3}}</h5>
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
              <div class="item" v-if="productDetail[0].Sides4">
                <h5>{{productDetail[0].Sides4}}</h5>
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
              </div>-->
            </div>
          </div>
          <div class="popup_footer">
            <div class="btn btn_round btn_white count_box">
              <a href="#" class="count_dis" @click="minusQty()">-</a>
              <a href="#" class="count_num">{{Orders.Qty}}</a>
              <a href="#" class="count_add" @click="addQty()">+</a>
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
import "swiper/dist/css/swiper.css";
import { swiper, swiperSlide } from "vue-awesome-swiper";
export default {
  data() {
    return {
      // footerActive: false,
      footerNumber: 0,
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
      },
      ProductimgUrl: "https://lay-order.rocket-coding.com/Img/product/",
      BannerimgUrl: "https://lay-order.rocket-coding.com/Img/BannerImg/",
      ShowPopup: false,
      Orders: {
        Qty: 1,
        Pid: "",
        Options: [],
        time: "",
        Img: "",
        Name: "",
        Price: ""
      },
      Cart: [],
      Sides: []
    };
  },
  // components: {
  //   Footer
  // },
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
    getImg(imgArr) {
      const firstImg = imgArr.split(",")[0];
      return firstImg;
    },
    getImgFull(imgArr) {
      imgArr = [];
      const firstImg = imgArr.split(",")[0];
      return firstImg;
    },
    getProductDetail(Id, Show = true) {
      console.log(Id);
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetProductDetail/${Id}`;
      vm.ShowPopup = Show;
      this.$http.get(url).then(response => {
        console.log(response);
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
      });
    },
    ClosePopup() {
      this.ShowPopup = false;
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
      // console.log(title);
      let strright = str.split(":")[1].split("、");
      // console.log(like);
      this.Sides.push({ name: strleft, options: strright });
    },
    getCategory() {
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetCategory`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.categorys = response.data;
      });
    },
    addToCart() {
      alert("已加入購物車");
      const vm = this;
      vm.Cart.push(vm.Orders);
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
    },
    checkFooterCart() {
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
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
    this.getProductDetail(2, false);
    this.checkFooterCart();
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