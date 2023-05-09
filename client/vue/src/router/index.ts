import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
// import ContactIndex from "@/views/contact/ContactIndex.vue";
// import ContactTypeIndex from "@/views/contacttype/ContactTypeIndex.vue";
// import PersonIndex from "@/views/person/PersonIndex.vue";
import CardIndex from "@/views/card/CardIndex.vue";
import CardCreate from "@/views/card/CardCreate.vue";
import CardEdit from "@/views/card/CardEdit.vue";

import UserIndex from "@/views/user/UserIndex.vue";
import Login from "@/views/identity/Login.vue";
import Register from "@/views/identity/Register.vue";

import MenuItemIndex from "@/views/menuItems/MenuItemIndex.vue";
import MenuItemCreate from "@/views/menuItems/MenuItemCreate.vue";
import MenuItemEdit from "@/views/menuItems/MenuItemEdit.vue";

import TicketIndex from "@/views/tickets/TicketIndex.vue";
import TicketCreate from "@/views/tickets/TicketCreate.vue";
import TicketEdit from "@/views/tickets/TicketEdit.vue";
import UserLogIndex from "@/views/tickets/UserLogIndex.vue";
import TicketPurchase from "@/views/tickets/TicketPurchase.vue";

import OrderIndex from "@/views/orders/OrderIndex.vue";
import UserOrders from "@/views/orders/UserOrders.vue";
import ProcessOrder from "@/views/orders/ProcessOrder.vue";
import OrderResult from "@/views/orders/OrderResult.vue";

import CoordinateIndex from "@/views/coordinate/CoordinateIndex.vue";
import CoordinateCreate from "@/views/coordinate/CoordinateCreate.vue";
import CoordinateEdit from "@/views/coordinate/CoordinateEdit.vue";

import CoordinateLocationIndex from "@/views/coordinateLocation/CoordinateLocationIndex.vue";
import CoordinateLocationCreate from "@/views/coordinateLocation/CoordinateLocationCreate.vue";
import CoordinateLocationEdit from "@/views/coordinateLocation/CoordinateLocationEdit.vue";

import CouponIndex from "@/views/coupons/CouponIndex.vue";
import CouponCategoryIndex from "@/views/couponCategory/CouponCategoryIndex.vue";
import CouponCategoryCreate from "@/views/couponCategory/CouponCategoryCreate.vue";
import CouponCategoryEdit from "@/views/couponCategory/CouponCategoryEdit.vue";

import ItemCategoriesIndex from "@/views/itemCategories/ItemCategoriesIndex.vue";
import ItemCategoriesCreate from "@/views/itemCategories/ItemCategoriesCreate.vue";
import ItemCategoriesEdit from "@/views/itemCategories/ItemCategoriesEdit.vue";

import UserCategoryIndex from "@/views/userCategory/UserCategoryIndex.vue";
import UserCategoriesCreate from "@/views/userCategory/UserCategoriesCreate.vue";
import UserCategoriesEdit from "@/views/userCategory/UserCategoriesEdit.vue";




const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
    },
    {
      path: "/login",
      name: "login",
      component: Login,
    },
    {
      path: "/register",
      name: "register",
      component: Register,
    },
    { path: "/account", name: "account", component: UserIndex },

    { path: "/cards/create", name: "cardsCreate", component: CardCreate, props: true },
    { path: "/cards/edit/:id", name: "cardsEdit", component: CardEdit, props: true },
    { path: "/cards", name: "cards", component: CardIndex },

    { path: "/tickets", name: "tickets", component: TicketIndex },
    { path: "/tickets/create", name: "ticketsCreate", component: TicketCreate },
    { path: "/tickets/edit/:id", name: "ticketsEdit", component: TicketEdit, props: true },
    { path: "/ticketsInOrder", name: "ticketsInOrder", component: TicketPurchase },
    { path: "/userLogs/:id", name: "userLogs", component: UserLogIndex, props: true },
    
    { path: "/order", name: "order", component: OrderIndex },
    { path: "/orders", name: "orders", component: UserOrders },
    { path: "/processOrder", name: "processOrder", component: ProcessOrder },
    { path: "/orderResult", name: "orderResult", component: OrderResult },

    { path: "/coordinate", name: "coordinate", component: CoordinateIndex },
    { path: "/coordinate/create", name: "coordinateCreate", component: CoordinateCreate },
    { path: "/coordinate/edit/:id", name: "coordinateEdit", component: CoordinateEdit, props: true  },

    { path: "/location", name: "coordinateLocation", component: CoordinateLocationIndex },
    { path: "/location/create", name: "coordinateLocationCreate", component: CoordinateLocationCreate },
    { path: "/location/edit/:id", name: "coordinateLocationEdit", component: CoordinateLocationEdit, props: true },

    { path: "/menuItems", name: "menuItems", component: MenuItemIndex },
    { path: "/menuItems/create", name: "menuItemsCreate", component: MenuItemCreate },
    { path: "/menuItems/edit/:id", name: "menuItemsEdit", component: MenuItemEdit, props: true },
    
    { path: "/usersCoupon", name: "usersCoupon", component: CouponIndex },
    { path: "/couponCategory", name: "couponCategory", component: CouponCategoryIndex },
    { path: "/couponCategory/create", name: "couponCategoryCreate", component: CouponCategoryCreate},
    { path: "/couponCategory/edit/:id", name: "couponCategoryEdit", component: CouponCategoryEdit, props: true},

    { path: "/itemCategories", name: "itemCategories", component: ItemCategoriesIndex  },
    { path: "/itemCategories/create", name: "itemCategoriesCreate", component: ItemCategoriesCreate },
    { path: "/itemCategories/edit/:id", name: "itemCategoriesEdit", component: ItemCategoriesEdit, props: true },

    { path: "/userCategory", name: "userCategory", component: UserCategoryIndex },
    { path: "/usercategories/create", name: "userCategoriesCreate", component: UserCategoriesCreate },
    { path: "/usercategories/edit/:id", name: "userCategoriesEdit", component: UserCategoriesEdit, props: true },
  ],

});

export default router;
