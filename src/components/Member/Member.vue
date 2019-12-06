<template>
  <div>
    <h1>Member</h1>
    <ul>
      <li>1</li>
      <li>2</li>
      <li>3</li>
      <li>4</li>
      <li>5</li>
      <li>6</li>
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
        vm.memberInfo = response.data;
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