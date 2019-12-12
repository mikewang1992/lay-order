<template>
  <div class="page">
    <div class="main pb-2">
      <header>
        <h1>點餐成功</h1>
      </header>
      <div class="content">
        <div class="result out">
          <img src="@/assets/img/result_ok.png" alt />
          <div class="number">001</div>
          <p class="text-center mb-4">
            謝謝您的訂餐！
            <br />請於您指定的時間憑
            <span class="color_default">取餐編號</span>來店取餐
            <br />若需調整訂單請撥打電話與我們聯繫！
          </p>
        </div>
        <div class="order_list mb-4">
          <ul>
            <li class="item total">
              <h4>
                共
                <b class="color_default">5</b> 份
              </h4>
              <span>$600</span>
            </li>
            <li class="item">
              <h4>電話</h4>
              <span>0980123123</span>
            </li>
            <li class="item">
              <h4>取餐人</h4>
              <span>林美麗</span>
            </li>
            <li class="item">
              <h4>取餐時間</h4>
              <span>2019/12/12 12:00</span>
            </li>
          </ul>
        </div>
        <div class="d-flex text-center">
          <div class="col-6">
            <a href="#" class="btn btn_lg btn_yellow d-block">撥打電話</a>
          </div>
          <div class="col-6">
            <a href="order.html" class="btn btn_lg btn_default d-block">訂單狀態</a>
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