<template>
  <div class="main">
    <!-- 左選單 -->
    <div class="menu">
      <div class="menu_lg">
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.type == 'forhere'}"
          @click.prevent="getProduct('forhere','')"
        >
          <img src="@/assets/img/icon_kit01.png" alt />
          <h3>內用</h3>
        </a>
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.type == 'togo'}"
          @click.prevent="getProduct('togo','')"
        >
          <img src="@/assets/img/icon_kit02.png" alt />
          <h3>外帶</h3>
        </a>
      </div>
      <div class="menu_sm">
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.status == 'paid'}"
          @click.prevent="getProduct('','paid')"
        >
          <img src="@/assets/img/icon_kit04.png" alt />
          <h3>已完成</h3>
        </a>
        <a
          href="#"
          class="menu_btn"
          :class="{'active':filterMenu.status == 'cancel'}"
          @click.prevent="getProduct('','cancel')"
        >
          <img src="@/assets/img/icon_kit05.png" alt />
          <h3>已取消</h3>
        </a>
      </div>
    </div>
    <!-- 產品列表 content -->
    <div class="content">
      <!-- 狀態篩選 -->
      <div class="status">
        <ul v-if="filterMenu.status !== 'cancel'" v-show="filterMenu.status !== 'paid'">
          <li
            class="font_en all"
            :class="{'active':filterMenu.status == ''}"
            @click="filterMenu.status = ''"
          >All</li>
          <li class="prepare"
            :class="{'active':filterMenu.status == 'prepare'}"
            @click="filterMenu.status = 'prepare'"
          >
            準備中
            <span class="iconfont icon-hourglass"></span>
          </li>
          <li class="togo"
            v-if="filterMenu.type == 'togo'"
            :class="{'active':filterMenu.status == 'ready'}"
            @click="filterMenu.status = 'ready'"
          >
            待取餐
            <span class="iconfont icon-bellringoutline"></span>
          </li>
          <li class="ready"
            v-if="filterMenu.type == 'forhere'"
            :class="{'active':filterMenu.status == 'ready'}"
            @click="filterMenu.status = 'ready'"
          >
            待出餐
            <span class="iconfont icon-bellringoutline"></span>
          </li>
          <li class="done" :class="{'active':filterMenu.status == 'done'}" @click="filterMenu.status = 'done'">
            待收款
            <span class="iconfont icon-dollar-"></span>
          </li>
        </ul>
      </div>
      <div class="order">
        <!-- 訂單列表 -->
        <div class="list">
          <div
            class="item"
            :id="item.Orderid"
            :class="item.status"
            v-for="(item,key,index) in productList"
            :key="index"
            @click="showDetail(item.Orderid)"
          >
            <div class="info">
              <h3>
                <span class="num">{{item.Orderid}}.</span>
                {{item.customer}}
              </h3>
              <ul>
                <li>
                  <span class="iconfont icon-phone" />
                  <p class="font_en">{{item.tel}}</p>
                </li>
                <li>
                  <span class="iconfont icon-icon-time" />
                  <p v-show="filterMenu.type == 'togo'||filterMenu.type == 'forhere'">
                    <b class="font_en">{{getTime(item.gettime)}}</b> 取餐
                  </p>
                  <p v-show="filterMenu.status == 'paid'||filterMenu.status == 'cancel'">
                    <span>{{getTheDay(item.gettime)}}</span>
                  </p>
                </li>
                <li>
                  <span class="iconfont icon-dollar" />
                  <p class="font_en">{{item.total}}</p>
                </li>
              </ul>
            </div>
          </div>
        </div>
        <!-- 分頁 -->
        <div class="pages">
          <span
            class="iconfont page_last icon-fanhui"
            :class="{'none':pages.curPage==1}"
            @click="pages.curPage-=1,getProduct(filterMenu.type, filterMenu.status, pages.curPage)"
          ></span>

          <span
            class="font_price"
            v-for="item in pages.sum"
            :key="item"
            :class="{'active':item == pages.curPage}"
            @click="pages.curPage = item,getProduct(filterMenu.type, filterMenu.status, pages.curPage)"
          >{{item}}</span>

          <span
            class="iconfont page_next icon-youjiantou"
            :class="{'none':pages.curPage==pages.sum}"
            @click="pages.curPage+=1,getProduct(filterMenu.type, filterMenu.status, pages.curPage)"
          ></span>
        </div>
      </div>
    </div>
    <!-- 訂單資訊 content -->
    <div class="detail">
      <!-- 詳情標題 -->
      <div class="title">
        <h2>
          <span></span>訂單資訊
        </h2>
      </div>
      <!-- 顧客詳情 -->
      <div class="customer" v-show="detail !== ''">
        <ul v-for="(item,key,index) in customer" :key="index">
          <li>
            <h4>取餐人</h4>
            <p>
              {{item.name}}
              <span class="font_en">{{item.tel}}</span>
            </p>
          </li>
          <li>
            <h4>訂餐時間</h4>
            <p class="font_en">{{getFullTime(item.ordertime)}}</p>
          </li>
          <li>
            <h4>取餐時間</h4>
            <p class="font_en">{{getTime(item.gettime)}}</p>
          </li>
        </ul>
        <!-- 所有訂單完成 -->
        <div
          class="all_done"
          v-if="filterMenu.status !== 'paid'"
          v-show="filterMenu.status !== 'cancel'"
          @click="orderdelivered(thisOrderID)"
        >
          <p>
            <span class="iconfont icon-check"></span> 全部出餐完畢
          </p>
        </div>
      </div>
      <!-- 產品列表 -->
      <div class="order_info" v-show="detail !== ''">
        <div class="cart_list">
          <ul>
            <li
              class="item"
              :class="item.status"
              v-for="(item,key,index) in detail"
              :key="index"
              @click="itemDelivered(thisOrderID,item.pid)"
            >
              <div class="p_img" :id="item.pid">
                <img
                  :src="'https://lay-order.rocket-coding.com/Img/product/'+item.img[0]"
                  :key="item.pid"
                />
              </div>
              <div class="p_info">
                <div class="p_name">
                  <h3>{{item.name}} X{{item.Qty}}</h3>
                </div>
                <div class="p_choose">{{item.options}}</div>
              </div>
              <div class="p_price font_price">${{item.subtotal}}</div>
            </li>
          </ul>
        </div>
      </div>
      <!-- 結帳資訊 -->
      <div class="total" v-show="detail !== ''">
        <ul v-for="(item,key,index) in customer" :key="index">
          <li>
            <h4>數量</h4>
            <p class="font_price">{{item.totalQty}}</p>
          </li>
          <li>
            <h4>總計</h4>
            <p class="color_red font_price">${{item.totalAmount}}</p>
          </li>
        </ul>
        <div
          class="checkout"
          v-if="filterMenu.status !== 'paid'"
          v-show="filterMenu.status !== 'cancel'"
        >
          <div class="btn btn_lg btn_gray mr-2 fz_md" @click="cancelOrder()">取消訂單</div>
          <div class="btn btn_lg btn_green" @click="checkOrder()">結帳</div>
        </div>
        <div class="checkout" v-if="filterMenu.status == 'cancel'">
          <div class="btn btn_lg btn_default mr-2" @click="cancelBack()">恢復訂單</div>
        </div>
        <div class="checkout" v-if="filterMenu.status == 'paid'">
          <div class="btn btn_lg btn_default mr-2" @click="paidBack()">恢復訂單</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      filterMenu: {
        type: "forhere",
        status: ""
      },
      productList: {},
      customer: "",
      detail: "",
      thisOrderID: "",
      pages: {
        curPage: 1,
        sum: ""
      },
      messages: []
    };
  },
  methods: {
    getProduct(type, status, pages = "1") {
      let loader = this.$loading.show();
      const vm = this;
      this.filterMenu.type = type;
      this.filterMenu.status = status;
      const url = `${process.env.APIPATH}/Counter/ShowOrderList?type=${type}&status=${status}&page=${pages}`;
      this.$http.get(url).then(response => {
        loader.hide();
        vm.productList = response.data;
        // console.log(response.data);
      });
      this.detail = "";
      this.customer = "";
      this.getPages();
    },
    getTime(time) {
      const date = new Date(time);
      let m = date.getMinutes();
      if (m < 10) {
        m = "0" + m;
      }
      const newTime = `${date.getHours()}:${m}`;
      return newTime;
    },
    getPages() {
      const vm = this;
      const url = `${process.env.APIPATH}/Counter/TotalPage?type=${this.filterMenu.type}&status=${this.filterMenu.status}`;
      this.$http.get(url).then(response => {
        // console.log("頁數",response.data);
        this.pages.sum = response.data;
      });
    },
    getFullTime(time) {
      const date = new Date(time);
      let year = date.getFullYear();
      let mon = date.getMonth() + 1;
      let day = date.getDate();
      let hour = date.getHours();
      let min = date.getMinutes();
      if (min < 10) {
        min = "0" + min;
      }
      const newFullDay = `${year}/${mon}/${day} ${hour}:${min}`;
      return newFullDay;
    },
    getTheDay(time) {
      const date = new Date(time);
      let year = date.getFullYear();
      let mon = date.getMonth() + 1;
      let day = date.getDate();
      const newDay = `${year}/${mon}/${day}`;
      return newDay;
    },
    showDetail(id) {
      // console.log('顯示訂單詳情');
      const vm = this;
      this.thisOrderID = id;
      document
        .querySelectorAll(".content .order .item")
        .forEach(item => item.classList.remove("active"));
      document.getElementById(id).classList.add("active");
      this.$http
        .get(`${process.env.APIPATH}/Order/ShowOrderSummary/${id}`)
        .then(response => {
          vm.customer = response.data;
        });
      this.$http
        .get(`${process.env.APIPATH}/Order/ShowOrderDetail/${id}`)
        .then(response => {
          vm.detail = response.data;
        });
    },
    orderdelivered(id) {
      const vm = this;
      this.$http
        .get(`${process.env.APIPATH}/Counter/OrderDelivered/${id}`)
        .then(response => {
          // console.log(response.data);
          this.websocketbtn("counter訂單送餐完成");
          this.$swal({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            type: "success",
            title: "此訂單全數出餐完成"
          });
          this.showDetail(id);
          this.getProduct(
            this.filterMenu.type,
            this.filterMenu.status,
            this.pages.curPage
          );
        });
    },
    cancelOrder() {
      this.$swal({
        title: "取消訂單",
        text: "確認要取消此筆訂單嗎？",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "確認",
        cancelButtonText: "取消"
      }).then(result => {
        if (result.value) {
          this.$http
            .get(
              `${process.env.APIPATH}/Counter/CancelOrder/${this.thisOrderID}`
            )
            .then(response => {
              // console.log(response.data);
              this.websocketbtn("counter訂單取消");
              this.$swal("成功取消", "本筆訂單已移動至取消訂單", "success");
              this.getProduct(
                this.filterMenu.type,
                this.filterMenu.status,
                this.pages.curPage
              );
            });
        }
      });
    },
    cancelBack() {
      this.$swal({
        title: "確定要恢復本筆訂單嗎？",
        type: "info",
        showCancelButton: true,
        confirmButtonText: "確定",
        cancelButtonText: "取消"
      }).then(result => {
        if (result.value) {
          this.$http
            .get(
              `${process.env.APIPATH}/Counter/BackToPrepare?Oid=${this.thisOrderID}`
            )
            .then(response => {
              // console.log(response.data);
              this.websocketbtn("counter恢復取消單");
              if (response.data == "success") {
                this.$swal({
                  toast: true,
                  position: "top-end",
                  showConfirmButton: false,
                  timer: 3000,
                  type: "success",
                  title: "成功恢復本筆訂單"
                });
                this.getProduct(
                  this.filterMenu.type,
                  this.filterMenu.status,
                  this.pages.curPage
                );
              } else {
                this.$swal({
                  toast: true,
                  position: "top-end",
                  showConfirmButton: false,
                  timer: 3000,
                  type: "warning",
                  title: "訂單恢復失敗"
                });
              }
            });
        }
      });
    },
    paidBack() {
      this.$swal({
        title: "確定要恢復本筆訂單嗎？",
        type: "info",
        showCancelButton: true,
        confirmButtonText: "確定",
        cancelButtonText: "取消"
      }).then(result => {
        if (result.value) {
          this.$http
            .get(
              `${process.env.APIPATH}/Counter/BackToDone?Oid=${this.thisOrderID}`
            )
            .then(response => {
              console.log(response.data);
              if (response.data == "success") {
                this.websocketbtn("counter恢復完成單");
                this.$swal({
                  toast: true,
                  position: "top-end",
                  showConfirmButton: false,
                  timer: 3000,
                  type: "success",
                  title: "成功恢復本筆訂單"
                });
                this.getProduct(
                  this.filterMenu.type,
                  this.filterMenu.status,
                  this.pages.curPage
                );
              } else {
                this.$swal({
                  toast: true,
                  position: "top-end",
                  showConfirmButton: false,
                  timer: 3000,
                  type: "warning",
                  title: "訂單恢復失敗"
                });
              }
            });
        }
      });
    },
    checkOrder() {
      // console.log("我要結帳嚕");

      this.$swal({
        title: "確認收款",
        text: "訂單完成，收錢嚕！",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "結帳",
        cancelButtonText: "取消"
      }).then(result => {
        if (result.value) {
          this.$http
            .get(
              `${process.env.APIPATH}/Counter/CheckOrder/${this.thisOrderID}`
            )
            .then(response => {
              // console.log(response.data);
              this.websocketbtn("counter訂單結帳");
              this.$swal("完成結帳", "發財嚕！", "success");
              this.getProduct(
                this.filterMenu.type,
                this.filterMenu.status,
                this.pages.curPage
              );
            });
        }
      });
    },
    itemDelivered(Oid, Pid) {
      // console.log("單品送餐");
      this.$http
        .get(
          `${process.env.APIPATH}/Counter/ItemDelivered?Oid=${Oid}&id=${Pid}`
        )
        .then(response => {
          // console.log(response.data);
          this.websocketbtn("counter單品送餐完成");
          this.$swal({
            toast: true,
            position: "top-end",
            showConfirmButton: false,
            timer: 3000,
            type: "success",
            title: "出餐完成"
          });
          this.getProduct(
            this.filterMenu.type,
            this.filterMenu.status,
            this.pages.curPage
          );
          this.showDetail(Oid);
        });
    },
    websocketbtn(par = "counter") {
      console.log("websocket送出", par);
      $.connection.chatHub.server.send(par);
    },
    websocketListen() {
      const vm = this;
      $.connection.chatHub.client.addNewMessageToPage = function(message) {
        // console.log('websocket來嚕',message);
        if (
          message == "cart送出訂單" ||
          message == "kitchen單品備餐完成" ||
          message == "kitchen訂單備餐完成"
        ) {
          vm.getProduct(
            vm.filterMenu.type,
            vm.filterMenu.status,
            vm.pages.curPage
          );
          vm.showDetail(vm.thisOrderID);
        }
      };
      $.connection.hub.start().done();
    }
  },
  computed: {},
  watch: {
    "filterMenu.status": function() {
      this.getProduct(this.filterMenu.type, this.filterMenu.status);
    }
  },
  created() {
    this.getProduct("forhere", "");
    this.websocketListen();
  }
};
</script>
<style src="../../assets/css/store.css" scoped></style>