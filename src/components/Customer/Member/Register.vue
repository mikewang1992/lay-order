<template>
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
  </div>
</template>

<script>
export default {
  data() {
    return {
      registerInfo: { City: null },
      vertifyCodes: {},
      Counties: {},
      TownShips: {},
      Dist: null
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
      this.$http.post(url, data, config).then(response => {
        console.log(response);
        alert(response.data);
      });
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
};
</script>

<style scope>
#app > div {
  background: red;
}
</style>