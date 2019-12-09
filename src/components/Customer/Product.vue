<template>
  <div>
    <p v-if="isOpenFromCustomer" style="background:green">1.IsOpen是否營業時間:現在不開放預約</p>
    <h2>product</h2>
    <h3>banners</h3>
    <ul>
      <li v-for="(item,key,index) in banners" :key="index">{{item}}</li>
    </ul>
    <h3>categorys</h3>
    <ul>
      <li @click.prevent="getProducts()">全部</li>
      <li v-for="(item,key,index) in categorys" :key="index" @click.prevent="getProducts()">{{item}}</li>
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
  </div>
</template>

<script>
export default {
  data() {
    return {
      banners: {},
      products: {},
      modalAppear: false,
      productDetail: {},
      categorys: {}
    };
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
    getProducts() {
      const vm = this;
      const url = `${process.env.APIPATH}/Product/GetProduct?Type=all`;
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
    }
  },
  created() {
    this.getBanners();
    this.getProducts();
    this.getCategory();
    console.log(this.isOpenFromCustomer);
  }
};
</script>
<style >
#app > div > div {
  background: yellow;
}
</style>