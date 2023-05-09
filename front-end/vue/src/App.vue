<template>
  <Header/>

  <div class="container">
    <main role="main" class="pb-3">
      <RouterView />
    </main>
  </div>

  <Footer id="footer"></Footer>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink, RouterView } from "vue-router";
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";

import { useCardStore } from "@/stores/cards";
import { CardService } from "./services/CardService";

import { useTicketStore } from "@/stores/tickets";
import { TicketService } from "./services/TicketService";

import { useOrderStore } from "@/stores/orders";
import { OrderService } from "./services/OrderService";

import { useMenuItemStore } from "@/stores/menuItems";
import { MenuItemService } from "./services/MenuItemService";
import { useItemCategoryStore } from "./stores/itemCategories";
import { ItemCategoryService } from "./services/ItemCategoryService";
import { useUserCategoryStore } from "./stores/userCategories";
import { UserCategoryService } from "./services/UserCategoryService";
import { useCouponCategoryStore } from "./stores/couponCategories";
import { CouponCategoryService } from "./services/CouponCategoryService";
import { useCoordinateLocationStore } from "./stores/coordinateLocations";
import { CoordinateLocationService } from "./services/CoordinateLocationService";


@Options({
  components: {
    Header, Footer
  }
 })
 
export default class App extends Vue{
  cardsStore = useCardStore();
  cardService = new CardService;

  ticketsStore = useTicketStore();
  ticketService = new TicketService;

  ordersStore = useOrderStore();
  orderService = new OrderService;

  menuItemsStore = useMenuItemStore();
  menuItemService = new MenuItemService;

  itemCategoriesStore = useItemCategoryStore();
  itemCategoryService = new ItemCategoryService;

  userCategoriesStore = useUserCategoryStore();
  userCategoryService = new UserCategoryService;

  couponCategoriesStore = useCouponCategoryStore();
  couponCategoryService = new CouponCategoryService;

  coordinateLocationsStore = useCoordinateLocationStore();
  coordinateLocationService = new CoordinateLocationService;

  async mounted(): Promise<void>{
    console.log("monted")
    this.cardsStore.$state.cards = await this.cardService.getAll();

    this.ticketsStore.$state.tickets = await this.ticketService.getAll();

    this.menuItemsStore.$state.menuItems = await this.menuItemService.getAll();

    this.ordersStore.$state.orders = await this.orderService.getAll();
    this.itemCategoriesStore.$state.itemCategories = await this.itemCategoryService.getAll();

    this.userCategoriesStore.$state.userCategories = await this.userCategoryService.getAll();
    this.couponCategoriesStore.$state.couponCategories = await this.couponCategoryService.getAll();

    this.coordinateLocationsStore.$state.locations = await this.coordinateLocationService.getAll();
  }
}
</script>

<style lang="css">
#footer {
    position: fixed;
    bottom: 0;
}
</style>
