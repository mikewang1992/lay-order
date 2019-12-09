<template>
  <div>
    <h1>Register</h1>
    <input type="text" placeholder="Tel" v-model="registerInfo.Tel" />
    <input type="text" placeholder="Password" v-model="registerInfo.Password" />
    <input type="text" placeholder="Name" v-model="registerInfo.Name" />
    <input type="text" placeholder="Birth" v-model="registerInfo.Birth" />
    <input type="text" placeholder="City" v-model="registerInfo.City" />
    <input type="text" placeholder="Dist" v-model="registerInfo.Dist" />
    <button @click.prevent="register">19.註冊Create</button>
    <input type="text" placeholder="Vertify" v-model="vertifyCode" />
    <button @click.prevent="vertify">17.Vertify確認驗證碼</button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      registerInfo: {},
      vertifyCode: ""
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
      this.$http.post(url, data, config).then(response => {
        console.log(response);
      });
    },
    vertify() {
      const vm = this;
      const url = `${process.env.APIPATH}/Accounts/Vertify`;
      const data = vm.vertifyCode;
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
  }
};
</script>

<style scope>
#app > div {
  background: red;
}
</style>