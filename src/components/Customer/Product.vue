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
      <p v-if="isOpenFromCustomer" style="background:red;color:black">1.IsOpen是否營業時間:現在不開放預約</p>
      <header>
        <img src="@/assets/img/logo_nav.png" alt />
      </header>
      <div class="content">
        <swiper :options="swiperOption" ref="mySwiper" @someSwiperEvent="swiper()">
          <swiper-slide v-for="(item,key,index) in banners" :key="index">
            <img :src="BannerimgUrl+item" alt />
          </swiper-slide>
          <div class="swiper-pagination" slot="pagination"></div>
        </swiper>
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
            <a @click.prevent="getProducts(item.Id)">{{item.PCName}}</a>
          </swiper-slide>
        </swiper>

        <div class="product_list">
          <div
            class="col-lg-3 col-md-4 col-6 p-0"
            @click.prevent="getProductDetail(item.Id)"
            v-for="(item,key,index) in products"
            :key="index"
          >
            <div class="item open_popup">
              <div class="p_image">
                <img :src="ProductimgUrl+item.Img" alt />
              </div>
              <div class="p_info">
                <h3>{{item.Name}}</h3>
                <p>NT${{item.Price}}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!---------------------------------------------- 彈跳視窗暫放div page ----------------------------------->
    <div class="popup" :class="{'show':ShowPopup}">
      <a class="icon_close iconfont icon-weibiao45133 popup_close" @click.prevent="ShowPopup=false"></a>
      <div class="popup_content col-12 col-lg-6 col-md-8">
        <div class="p_slider">
          <swiper
            :options="swiperOption"
            class="swiper_product"
            ref="mySwiper"
            @someSwiperEvent="swiper()"
          >
            <swiper-slide v-for="(item,key,index) in banners" :key="index">
              <img :src="ProductimgUrl+productDetail[0].Img" alt />
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
            <div class="p_choose">
              <div class="item" v-for="(item,key,index) in Sides" :key="index">
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
                <!-- <div class="form-check">
                  <input class="form-check-input" type="radio" name="ice" id="inlineRadio2" value />
                  <label for="inlineRadio2">少冰</label>
                </div>-->
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
      Orders: { Qty: 1, Pid: "", Options: "", time: "" },
      Sides: []
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
    getProductDetail(Id, Show = true) {
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetProductDetail/${Id}`;
      vm.ShowPopup = Show;
      this.$http.get(url).then(response => {
        console.log(response);
        if (response.data[0]) {
          vm.Sides = [];
          vm.modalAppear = true;
          vm.productDetail = response.data;
          vm.Orders.Pid = vm.productDetail[0].PCid;
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
    sidesfilter(str) {
      let title = str.split(":")[0];
      console.log(title);
      let like = str.split(":")[1].split("、");
      console.log(like);
      this.Sides.push({ name: title, options: like });
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
      alert("addtocart");
      const vm = this;
      localStorage.setItem("cart", JSON.stringify(this.Orders));
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
    // optionsPlus(str) {
    //   alert("yes");
    //   this.Orders.Options = str;
    // }
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