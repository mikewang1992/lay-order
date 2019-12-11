<template>
  <footer>
    <ul>
      <li>
        <a href="index.html">
          <img src="@/assets/img/icon_footer01.png" alt />
          <p>菜單</p>
        </a>
      </li>
      <li>
        <a href="cart.html">
          <img src="@/assets/img/icon_footer02.png" alt />
          <p>點菜單</p>
        </a>
      </li>
      <li>
        <a href="order.html">
          <img src="@/assets/img/icon_footer03.png" alt />
          <p>訂單狀態</p>
        </a>
      </li>
      <li class="active">
        <router-link to="/member">
          <img src="@/assets/img/icon_footer04.png" alt />
          <p>會員資訊</p>
        </router-link>
      </li>
    </ul>
  </footer>
</template>