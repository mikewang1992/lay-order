<template>
  <div class="page login">
    <footer>
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
          <router-link to="/order">
            <img src="@/assets/img/icon_footer03.png" alt />
            <p>訂單狀態</p>
          </router-link>
        </li>
        <li class="active">
          <router-link to="/member">
            <img src="@/assets/img/icon_footer04.png" alt />
            <p>會員資訊</p>
          </router-link>
        </li>
      </ul>
    </footer>
    <div class="main col-lg-4 col-md-6">
      <div class="content">
        <img src="img/logo.png" alt />
        <ul class="nav_group mb-3">
          <li>
            <router-link to="/login">登入</router-link>
            <!-- <a href="login.html">登入</a> -->
          </li>
          <li>
            <router-link to="/register" class="active">註冊</router-link>
            <!-- <a href="register.html" class="active">註冊</a> -->
          </li>
        </ul>
        <div class="input-group">
          <input type="text" class="form-control mb-1" placeholder="電話" v-model="registerInfo.Tel" />
          <span class="iconfont icon-Mobile"></span>
          <div class="input-group-append">
            <a href="#" class="btn" id v-if="!changetoVertify">送出驗證碼</a>
            <a
              href="#"
              class="btn"
              id
              @click.prevent="vertify(registerInfo.Tel,vertifyCodes.Vertify)"
              v-if="changetoVertify"
            >送出驗證碼</a>
          </div>
        </div>
        <small class="pb-2 d-block text-left color_gray pl-3">電話號碼即為您的登入帳號</small>
        <div class="form-group" v-if="changetoVertify">
          <label class="sr-only" for="phone">請輸入簡訊驗證碼</label>
          <span class="iconfont icon-message"></span>
          <input
            class="form-control"
            type="text"
            id
            placeholder="請輸入簡訊驗證碼"
            autocomplete="off"
            v-model="vertifyCodes.Vertify"
          />
        </div>
        <div class="form-group" v-if="!changetoVertify">
          <label class="sr-only" for="phone">姓名</label>
          <span class="iconfont icon-user"></span>
          <input
            class="form-control"
            type="text"
            id
            placeholder="姓名"
            autocomplete="off"
            v-model="registerInfo.Name"
          />
        </div>
        <div class="form-group" v-if="!changetoVertify">
          <label class="sr-only" for="phone">居住地</label>
          <span class="iconfont icon-location"></span>
          <div class="d-flex">
            <select
              class="form-control mr-1"
              name
              id
              v-model="registerInfo.County"
              @change="getTown(registerInfo.County)"
            >
              <option value="城市" hidden selected>城市</option>
              <option v-for="(item,key,index) in Counties" :key="index" :value="item">{{item}}</option>
            </select>
            <select class="form-control" name id v-model="registerInfo.Dist">
              <option value="區域" hidden selected>區域</option>
              <option v-for="(item,key,index) in TownShips" :key="index" :value="item">{{item}}</option>
            </select>
          </div>
        </div>
        <div class="form-group" v-if="!changetoVertify">
          <label class="sr-only" for="phone">生日</label>
          <span class="iconfont icon-birthday-cake"></span>
          <input
            class="form-control"
            type="date"
            id="inputDate"
            placeholder="生日"
            autocomplete="off"
            v-model="registerInfo.Birth"
            ref="inputDate"
          />
        </div>
        <div class="form-group mb-4" v-if="!changetoVertify">
          <label class="sr-only" for="phone">密碼</label>
          <span class="iconfont icon-lock"></span>
          <input
            class="form-control"
            type="password"
            id
            placeholder="密碼"
            autocomplete="off"
            v-model="registerInfo.Password"
          />
        </div>
        <a
          href="#"
          class="btn btn_default mb-2"
          @click.prevent="register"
          v-if="!changetoVertify"
        >註冊</a>
      </div>
    </div>
  </div>
  <!-- Mike
    <div>
    <h1>Register</h1>
    <input type="text" placeholder="Tel" v-model="registerInfo.Tel" />
    <input type="text" placeholder="Password" v-model="registerInfo.Password" />
    <input type="text" placeholder="Name" v-model="registerInfo.Name" />
    <input type="text" placeholder="Birth" v-model="registerInfo.Birth" />
    <input type="text" placeholder="City" v-model="registerInfo.City" />
    <select name="城市" v-model="registerInfo.City" @change="getTown(registerInfo.City)">
      <option v-for="(item,key,index) in Counties" :key="index" :value="item">{{item}}</option>
    </select>
    <input type="text" placeholder="Dist" v-model="registerInfo.Dist" />
    <select name="地區" v-model="Dist">
      <option v-for="(item,key,index) in TownShips" :key="index" :value="item">{{item}}</option>
    </select>
    <button @click.prevent="register">19.註冊Create</button>
    <input type="text" placeholder="Vertify" v-model="vertifyCodes.Vertify" />
    <button @click.prevent="vertify(registerInfo.Tel,vertifyCodes.Vertify)">17.Vertify確認驗證碼</button>
  </div>-->
</template>

<script>
export default {
  data() {
    return {
      registerInfo: { County: "城市", Dist: "區域" },
      vertifyCodes: {},
      Counties: {},
      TownShips: {},
      changetoVertify: false,
      footerNumber: 0
    };
  },
  methods: {
    register() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Create`;
      const data = vm.registerInfo;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      if (
        vm.registerInfo.County !== "城市" &&
        vm.registerInfo.Dist !== "區域"
      ) {
        this.$http.post(url, data, config).then(response => {
          console.log(response);
          alert(response.data);
          if (response.data === "success") {
            vm.changetoVertify = true;
          } else if (response.data === "此電話已存在，請勿重複申請") {
          }
        });
      } else {
        alert("請選擇城市或區域");
      }
    },
    vertify(tel, vertify) {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Vertify`;
      const data = { Tel: tel, Vertify: vertify };
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      this.$http.post(url, data, config).then(response => {
        console.log(response);
        if (typeof response.data == "number") {
          alert("驗證成功");
        } else {
          alert(response.data);
        }
      });
    },
    getCounty() {
      const vm = this;
      const url = `${process.env.APIPATH}/Areas/County`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.Counties = response.data;
      });
    },
    getTown(County) {
      // console.log(county);
      const vm = this;
      const url = `${process.env.APIPATH}/Areas/Town?county=${County}`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.TownShips = response.data;
      });
    },
    checkFooterCart() {
      if (JSON.parse(localStorage.getItem("totalcart")) !== null) {
        this.footerNumber = JSON.parse(
          localStorage.getItem("totalcart")
        ).length;
      }
    }
  },
  created() {
    this.getCounty();
    this.checkFooterCart();
  }
};
</script>

<style scope>
</style>