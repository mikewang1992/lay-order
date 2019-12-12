<template>
  <div class="page">
    <div class="main pb-2">
      <header>
        <h1>點餐成功</h1>
      </header>
      <div class="content">
        <div class="result">
          <img src="@/assets/img/result_in.svg" alt />
          <div class="number in">A</div>
          <p class="text-center mb-4">
            <b class="color_default">點餐完成，我們將盡快製作您的餐點</b>
            <br />用餐完請至櫃檯結帳^＿^
            <br />邀請您加入線上點餐系統
            <br />節省預約時間還可以取得特別優惠歐！
          </p>
        </div>
        <div class="order_list mb-4">
          <ul>
            <li class="item total p-0 align-items-center">
              <h4>
                共
                <b class="color_default">5</b> 份
              </h4>
              <span>$600</span>
            </li>
          </ul>
        </div>
        <div class="d-flex text-center">
          <div class="col-6">
            <a href="#" class="btn btn_yellow btn_lg d-block">撥打電話</a>
          </div>
          <div class="col-6">
            <a href="order.html" class="btn btn_default btn_lg d-block">訂單狀態</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  data() {
    return {};
  },
  methods: {}
};
</script>