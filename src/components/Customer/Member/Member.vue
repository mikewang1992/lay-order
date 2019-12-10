<template>
  <div class="page member">
    <footer>
      <ul>
        <li>
          <a href="index.html">
            <img src="img/icon_footer01.png" alt />
            <p>菜單</p>
          </a>
        </li>
        <li>
          <a href="cart.html">
            <img src="img/icon_footer02.png" alt />
            <p>點菜單</p>
          </a>
        </li>
        <li>
          <a href="order.html">
            <img src="img/icon_footer03.png" alt />
            <p>訂單狀態</p>
          </a>
        </li>
        <li class="active">
          <a href="member_sale.html">
            <img src="img/icon_footer04.png" alt />
            <p>會員資訊</p>
          </a>
        </li>
      </ul>
    </footer>
    <div class="main col-md-6 col-xl-4">
      <header>
        <h1 class="title">會員資訊</h1>
      </header>
      <div class="content">
        <div class="row mb-2">
          <div class="col-sm-8 offset-sm-2">
            <ul class="nav_group">
              <li>
                <a href="member_sale.html">我的優惠券</a>
              </li>
              <li>
                <a href="member_info.html" class="active">個人資訊</a>
              </li>
            </ul>
          </div>
        </div>
        <div class="row text-center">
          <div class="ml-auto mr-auto mb-3">
            <h2 class="mb-1">{{memberInfo.Tel}}</h2>
            <small class="color_red">若需修改電話號碼，請重新註冊</small>
          </div>

          <div class="col-sm-8 offset-sm-2">
            <div class="form-group">
              <label class="sr-only" for="phone">姓名</label>
              <span class="iconfont icon-Mobile"></span>
              <input
                class="form-control"
                type="text"
                id
                placeholder="電話"
                :value="memberInfo.Tel"
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
                :value="memberInfo.Name"
                autocomplete="off"
              />
            </div>
            <div class="form-group">
              <label class="sr-only" for="phone">居住地</label>
              <span class="iconfont icon-location"></span>
              <div class="d-flex">
                <select class="form-control mr-1" name id>
                  <option value hidden>城市</option>
                  <option value selected>高雄市</option>
                  <option value>高雄市</option>
                  <option value>高雄市</option>
                </select>
                <select class="form-control" name id>
                  <option value hidden>區域</option>
                  <option value selected>三民區</option>
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
                :value="memberInfo.Birth"
              />
            </div>
            <div class="form-group mb-4">
              <label class="sr-only" for="phone">密碼</label>
              <span class="iconfont icon-lock"></span>
              <input
                class="form-control"
                type="password"
                id
                value="123123123"
                placeholder="密碼"
                autocomplete="off"
              />
            </div>
            <a href="#" class="btn btn_default mb-2" @click.prevent="editMemberInfo">修改</a>
            <br />
            <small>
              <a href="login.html" class="color_gray">登出</a>
            </small>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- <div>
    <h1>Member</h1>
    <ul>
      <li>
        Tel:
        <input type="text" placeholder="Tel" v-model="memberInfo.Tel" required />
      </li>
      <li>
        Name:
        <input type="text" placeholder="Name" v-model="memberInfo.Name" />
      </li>
      <li>
        Birth:
        <input type="text" placeholder="Birth" v-model="memberInfo.Birth" />
      </li>
      <li>
        City:
        <input type="text" placeholder="City" v-model="memberInfo.City" />
      </li>
      <li>
        Dist:
        <input type="text" placeholder="Dist" v-model="memberInfo.Dist" />
      </li>
      <li>
        NewPassword:
        <input type="text" placeholder="NewPassword" v-model="memberInfo.NewPassword" />
      </li>
    </ul>
    <button @click.prevent="editMemberInfo">23.Edit修改會員資料</button>
  </div>-->
</template>

<script type="text/javascript" src="js/jquery-3.4.1.min.js"></script>
<script type="text/javascript" src="js/datepicker.min.js"></script>
<script type="text/javascript" src="js/datepicker.zh.js"></script>
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
      memberInfo: {}
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
        console.log(response);
      });
    }
  },
  created() {
    this.getMemberInfo();
  }
};
</script>

<style scope>
</style>