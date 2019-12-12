<template>
  <div class="main">
    <div class="menu">
      <a href="#" class="menu_btn" :class="{'active':type == 'forhere'}" @click.prevent="type = 'forhere'">
          <img src="@/assets/img/icon_kit01.png" alt>
          <h3>內用</h3>
        </a>
        <a href="#" class="menu_btn" :class="{'active':type == 'togo'}" @click.prevent="type = 'togo'">
          <img src="@/assets/img/icon_kit02.png" alt>
          <h3>外帶</h3>
        </a>
        <a href="#" class="menu_btn">
          <img src="@/assets/img/icon_kit03.png" alt>
          <h3>大單</h3>
        </a>
        <a href="#" class="menu_btn">
          <img src="@/assets/img/icon_kit04.png" alt>
          <h3>已完成</h3>
        </a>
        <a href="#" class="menu_btn">
          <img src="@/assets/img/icon_kit05.png" alt>
          <h3>已取消</h3>
        </a>
    </div>
    <div class="content">
      <div class="status">
        <ul>
          <li :class="{'active':status == ''}" @click="status = ''">All</li>
          <li :class="{'active':status == 'prepare'}" @click="status = 'prepare'">Prepare</li>
          <li :class="{'active':status == 'done'}" @click="status = 'done'">Done</li>
        </ul>
      </div>
      <div class="order">
        <div class="list">
          <!-- 元件放這 -->
          <div class="item" :class="item.status" v-for="(item,key,index) in productList" :key="index" @click="showDetail(item.Orderid)">
            <div class="info">
              <h3>
                <span class="num">{{item.Orderid}}.</span> {{item.customer}}
              </h3>
              <ul>
                <li>
                  <span class="iconfont icon-phone"/>
                  <p>{{item.tel}}</p>
                </li>
                <li>
                  <span class="iconfont icon-icon-time"/>
                  <p>{{getTime(item.gettime)}} 取餐</p>
                </li>
                <li>
                  <span class="iconfont icon-dollar"/>
                  <p>{{item.total}}</p>
                </li>
              </ul>
            </div>
            <div class="order_status">{{item.status}}</div>
          </div>
        </div>
        <div class="pages">
          <span class="iconfont icon-fanhui"></span>
          <span class="active">1</span>
          <span>2</span>
          <span>3</span>
          <span>4</span>
          <span>5</span>
          <span class="iconfont icon-youjiantou"></span>
        </div>
      </div>
    </div>
    <div class="detail">
      <div class="title">
        <h2>
          <span>002.</span>訂單資訊
        </h2>
      </div>
      <div class="customer">
        <ul v-for="(item,key,index) in customer" :key="index">
          <li>
            <h4>取餐人</h4>
            <p>{{item.name}} {{item.tel}}</p>
          </li>
          <li>
            <h4>訂餐時間</h4>
            <p>{{item.ordertime}}</p>
          </li>
          <li>
            <h4>取餐時間</h4>
            <p>{{item.gettime}}</p>
          </li>
        </ul>
      </div>
      <div class="order_info">
        <div class="cart_list">
          <ul>
            <li class="item" v-for="(item,key,index) in detail" :key="index">
              <div class="p_img">
                <img :src="'https://lay-order.rocket-coding.com/Img/product/' + 'beefrice.jpg' " alt>
              </div>
              <div class="p_info">
                <div class="p_name">
                  <h3>{{item.name}} X{{item.Qty}}</h3>
                </div>
                <div class="p_choose">{{item.options}}</div>
              </div>
              <div class="p_price">${{item.subtotal}}</div>
            </li>
          </ul>
        </div>
      </div>
      <div class="total">
        <ul v-for="(item,key,index) in customer" :key="index">
          <li>
            <h4>數量</h4>
            <p>{{item.totalQty}}</p>
          </li>
          <li>
            <h4>總計</h4>
            <p class="color_red">${{item.totalAmount}}</p>
          </li>
        </ul>
        <div class="checkout">
          <div class="btn btn_lg btn_gray mr-2">取消</div>
          <div class="btn btn_lg btn_green">結帳</div>
        </div>
      </div>
    </div>
  </div>
</template>



<script>
export default {
  data() {
    return {
      status:'',
      type:'forhere',
      productList:{},
      pages:'',
      customer:'',
      detail:'',
    }
  },
  methods: {
    getProduct(type='',status='') {
      const vm = this;
      const url = `${process.env.APIPATH}/Counter/ShowOrderList?type=${type}&status=${status}&page=1`;
      this.$http.get(url).then(response => {
        vm.productList = response.data;
      });
    },
    getTime(time){
      const date = new Date(time);
      let m = date.getMinutes();
      if(m<10) {
        m = '0'+m;
      };
      const newTime = `${date.getHours()}:${m}`;
      return newTime;
    },
    showDetail(id){
      const vm = this;
      this.$http.get(`${process.env.APIPATH}/Order/ShowOrderSummary/${id}`).then(response => {
        console.log(response.data)
        vm.customer = response.data;
      });
      this.$http.get(`${process.env.APIPATH}/Order/ShowOrderDetail/${id}`).then(response => {
        console.log(response.data)
        vm.detail = response.data;
      });
    },
  },
  computed: {
    filterList: function() {
      let vm = this;
      const url = `${process.env.APIPATH}/Counter/ShowOrderList?type=${vm.type}&status=${vm.status}&page=1`;
      axios.get(url).then(response => {
        vm.productList = response.data;
        return vm.productList;
      });
    }
  },
  created() {
    this.getProduct();
  },
}
</script>
<style scope>
@import "../../assets/css/store.css";
</style>