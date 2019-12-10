<template>
  <div class="page login">
    <div class="main col-lg-4 col-md-6">
      <div class="content">
        <img src="img/logo.png" alt />
        <ul class="nav_group mb-3">
          <li>
            <a href="login.html">登入</a>
          </li>
          <li>
            <a href="register.html" class="active">註冊</a>
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
              v-model="registerInfo.City"
              @change="getTown(registerInfo.City)"
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
  <!-- <div>
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
$(document).ready(function() {
  $(function() {
    $("#inputDate").datepicker({
      changeMonth: true,
      changeYear: true,
      maxDate: new Date(),
      language: "zh"
    });
  });
});
</script>
<script>
export default {
  data() {
    return {
      registerInfo: { City: "城市", Dist: "區域" },
      vertifyCodes: {},
      Counties: {},
      TownShips: {},
      changetoVertify: false
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
      if (vm.registerInfo.City !== "城市" && vm.registerInfo.Dist !== "區域") {
        this.$http.post(url, data, config).then(response => {
          console.log(response);
          alert(response.data);
          vm.changetoVertify = true;
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
        alert(response.data);
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
    getTown(City) {
      // console.log(county);
      const vm = this;
      const url = `${process.env.APIPATH}/Areas/Town?county=${City}`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.TownShips = response.data;
      });
    }
  },
  created() {
    this.getCounty();
  }
  // mounted() {
  //   this.getCounty();
  // }
};
</script>

<style scope>
</style>