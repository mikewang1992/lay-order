<template>
  <div>
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
  </div>
</template>
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
#app > div {
  background: red;
}
</style>