<template>
  <div class="main member login_content col-md-6 col-xl-4">
    <header>
      <h1 class="title">會員資訊</h1>
    </header>
    <div class="content">
      <div class="row mb-2">
        <div class="col-sm-12">
          <ul class="nav_group">
            <li>
              <router-link to="/coupon">我的優惠券</router-link>
            </li>
            <li>
              <router-link to="/member" class="active">個人資訊</router-link>
            </li>
          </ul>
        </div>
      </div>
      <div class="row text-center">
        <div class="ml-auto mr-auto mb-3 mt-3">
          <h2 class="mb-1 font_lg font_en">{{memberInfo.Tel}}</h2>
          <small class="color_red">若需修改電話號碼，請重新註冊</small>
        </div>

        <div class="col-sm-12">
          <div class="form-group">
            <label class="sr-only" for="phone">電話</label>
            <span class="iconfont icon-Mobile"></span>
            <input
              class="form-control"
              type="text"
              id
              placeholder="電話"
              v-model="memberInfo.Tel"
              autocomplete="off"
              disabled
            />
          </div>
          <div class="form-group">
            <label class="sr-only" for="phone">姓名</label>
            <span class="iconfont icon-user"></span>
            <input
              class="form-control"
              type="text"
              id
              placeholder="姓名"
              v-model="memberInfo.Name"
              autocomplete="off"
            />
          </div>
          <div class="form-group">
            <label class="sr-only" for="phone">居住地</label>
            <span class="iconfont icon-location"></span>
            <div class="d-flex">
              <select
                class="form-control mr-1"
                name
                id
                v-model="memberInfo.County"
                @change="getTown(memberInfo.County)"
              >
                <option :value="memberInfo.County" hidden selected>{{memberInfo.County}}</option>
                <option v-for="(item,key,index) in Counties" :key="index" :value="item">{{item}}</option>
              </select>
              <select class="form-control" name id v-model="memberInfo.Dist">
                <option :value="memberInfo.Dist" hidden selected>{{memberInfo.Dist}}</option>
                <option v-for="(item,key,index) in TownShips" :key="index" :value="item">{{item}}</option>
              </select>
            </div>
          </div>
          <div class="form-group">
            <label class="sr-only" for="phone">生日</label>
            <span class="iconfont icon-birthday-cake"></span>
            <input
              class="form-control"
              type="date"
              id="inputDate"
              placeholder="生日"
              autocomplete="off"
              v-model="memberInfo.Birth"
            />
          </div>
          <div class="form-group mb-4">
            <label class="sr-only" for="phone">密碼</label>
            <span class="iconfont icon-lock"></span>
            <input
              class="form-control"
              type="password"
              id
              placeholder="密碼"
              autocomplete="off"
              v-model="memberInfo.NewPassword"
            />
          </div>
          <a href="#" class="btn btn_default mb-2" @click.prevent="editMemberInfo">修改</a>
          <br />
          <small>
            <a href="#" class="color_gray" @click.prevent="logout()">登出</a>
          </small>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      memberInfo: { County: "", Dist: "", NewPassword: "" },
      Counties: {},
      TownShips: {},
      footerNumber: 0
    };
  },
  methods: {
    getMemberInfo() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/ShowUser`;
      this.$http.get(url).then(response => {
        console.log(response);
        vm.memberInfo = response.data[0];
      });
    },
    editMemberInfo() {
      let loader = this.$loading.show();
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Edit`;
      const data = vm.memberInfo;
      const config = {
        headers: {
          "Content-Type": "application/json"
        }
      };
      console.log(data);
      this.$http.post(url, data, config).then(response => {
        loader.hide();
        if (response.data == "success") {
          this.$swal("修改成功", "", "success");
        } else {
          this.$swal(response.data, "", "info");
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
    },
    logout() {
      let loader = this.$loading.show();
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Logout`;
      this.$http.get(url).then(response => {
        console.log(response);
        if (response.data === "success") {
          loader.hide();
          this.$swal("登出成功", "", "success");
          this.$router.push({ name: "Login" });
          localStorage.setItem("totalcart", JSON.stringify([]));
        }
      });
    }
  },
  created() {
    this.checkFooterCart();
    this.getMemberInfo();
    this.getCounty();
  }
};
</script>
<style src="../../../assets/css/all.css" scoped></style>