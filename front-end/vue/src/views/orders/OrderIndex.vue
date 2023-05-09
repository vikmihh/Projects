<template>
    <h1>My cart</h1>

    <table class="table">
        <tr v-for="item of ticketsInOrderStore.ticketsInOrder" :key="item.id">
            <td> <img src="https://cdn-icons-png.flaticon.com/512/1633/1633154.png" class=" image-fluid "
                    style="width:10em;"></td>
            <td class="col-8">
                <h5> {{ item.ticketName }}</h5>
                <p> Price: {{ item.ticketPrice }}$</p>
                <p>Duration: {{ item.ticketDayRange }} day(s)</p>
            </td>
            <td>
                <div class="input-group mb-3" style="width: 10em;">
                    <div class="input-group-prepend">
                        <button class="btn btn-danger" type="button" id="button-addon1"
                            @click="deleteTicketInOrder(item.id)">remove</button>
                    </div>

                </div>
            </td>
            <th class="col-2" scope="row">Price: {{ item.ticketPrice }}$</th>
        </tr>

        <tr v-for="item of itemsInOrderStore.itemsInOrder" :key="item.id">
            <td> <img src="https://www.clipartmax.com/png/middle/303-3030672_fast-food-icon-food-drink-icon.png"
                    class=" image-fluid " style="width:10em;"></td>
            <td class="col-8">
                <h5> {{ item.itemName }}</h5>
                <p> Price: {{ item.price }}$</p>
                <p> {{ item.description }}</p>
            </td>
            <td>
                <div class="input-group mb-3" style="width: 10em;">
                    <div class="input-group-prepend">
                        <button class="btn btn-danger" type="button" id="button-addon1"
                            @click="deleteItemInOrder(item.id!, item.amount)">remove</button>
                    </div>
                    <input type="number" min="1" v-model="item.amount" class="form-control">


                </div>
            </td>
            <th class="col-1" scope="row">Price: {{ item.total }}$</th>
        </tr>

    </table>


    <div v-for="item of ordersStore.orders" :key="item.id" class="card bg-light" style="width: 18rem;">
        <div v-if="userCouponsStore.userCoupons.length == 0" class="card-body">
            <h6 class="card-subtitle mb-2 text-muted">Price: {{ calculatePrice() }}$</h6>
            <h6 class="card-subtitle mb-2 text-muted">Discount: {{ }}%</h6>
            <h6 class="card-subtitle mb-2 text-muted">OrderNr: {{ item.orderNr }}</h6>
            <h6 class="card-subtitle mb-2  font-weight-bold">Have a promo code?</h6>
            <div class="input-group mb-3" style="width: 15rem;">
                <input type="text" v-model="promoCode" placeholder="Enter promo code" class="form-control">
                <div class="input-group-prepend">
                    <a class="btn btn-link" type="button" id="button-addon1" @click="activate(true)">Activate</a>
                </div>

            </div>
            <h5 class="card-title">Total: {{ calculateTotal() }}$</h5>
             <div v-if="itemsInOrderStore.itemsInOrder.length > 0 || ticketsInOrderStore.ticketsInOrder.length > 0">
                <RouterLink class="btn btn-primary" to="/processOrder">Checkout</RouterLink>
            </div>

        </div>
        <div v-else v-for="coupon of userCouponsStore.userCoupons" class="card-body">
            <h6 class="card-subtitle mb-2 text-muted">Price: {{ calculatePrice() }}$</h6>
            <h6 class="card-subtitle mb-2 text-muted">Discount: {{ coupon.couponCategoryDiscount }}%</h6>
            <h6 class="card-subtitle mb-2 text-muted">OrderNr: {{ item.orderNr }}</h6>
            <h6 class="card-subtitle mb-2 text-muted">Coupon Name: {{ coupon.couponCategoryName }} </h6>
            <div class="input-group-prepend">
                <a class="btn btn-link" type="button" id="button-addon1" @click="activate(false)">Remove</a>
            </div>

            <h5 class="card-title">Total: {{ calculateTotal() }}$</h5>
            <div v-if="itemsInOrderStore.itemsInOrder.length > 0 || ticketsInOrderStore.ticketsInOrder.length > 0">
                <RouterLink class="btn btn-primary" to="/processOrder">Checkout {{itemsInOrderStore.itemsInOrder.length}}</RouterLink>
            </div>


        </div>
    </div>
    <div>


    </div>
</template>

<script lang="ts">

import { OrderService } from "@/services/OrderService";
import { TicketInOrderService } from "@/services/TicketInOrderService";
import { ItemInOrderService } from "@/services/ItemInOrderService";
import { useOrderStore } from "@/stores/orders";
import { useTicketInOrderStore } from "@/stores/ticketsInOrderStore";
import { useItemInOrderStore } from "@/stores/itemsInOrderStore";
import { Options, Vue } from "vue-class-component";
import { useIdentityStore } from "@/stores/identity";
import { useUserCouponStore } from "@/stores/userCoupons";
import { UserCouponService } from "@/services/UserCouponService";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class OrderIndex extends Vue {
    id: string = "id000";
    promoCode: string = "";
    ordersStore = useOrderStore();
    orderService = new OrderService();
    ticketsInOrderStore = useTicketInOrderStore();
    ticketInOrderService = new TicketInOrderService();
    itemsInOrderStore = useItemInOrderStore();
    itemInOrderService = new ItemInOrderService();
    identityStore = useIdentityStore();

    userCouponsStore = useUserCouponStore();
    userCouponService = new UserCouponService();

    async activate(adding: boolean) {
        await this.userCouponService.activateCoupon(this.promoCode, adding);

        let test = await this.userCouponService.getCouponByOrderId(this.ordersStore.$state.orders[0].id);
        this.userCouponsStore.$state.userCoupons = [];

        if (test.id != undefined) {
            this.userCouponsStore.$state.userCoupons.push(test);
            this.promoCode = this.userCouponsStore.$state.userCoupons[0].promoCode;
        }
    }

    async mounted(): Promise<void> {
        console.log("mounted");
        this.ordersStore.$state.orders = []
        this.ticketsInOrderStore.$state.ticketsInOrder = []
        this.itemsInOrderStore.$state.itemsInOrder = []
        if (this.identityStore.jwt != null) {
            this.ordersStore.$state.orders.push(await this.orderService.getCurrentOrder());
            this.ticketsInOrderStore.$state.ticketsInOrder
                = await this.ticketInOrderService.getTicketsInOrderByOrderId(this.ordersStore.$state.orders[0].id);
            this.itemsInOrderStore.$state.itemsInOrder
                = await this.itemInOrderService.getItemsByOrderId(this.ordersStore.$state.orders[0].id);
            let test = await this.userCouponService.getCouponByOrderId(this.ordersStore.$state.orders[0].id);
            this.userCouponsStore.$state.userCoupons = [];

            if (test.id != undefined) {
                this.userCouponsStore.$state.userCoupons.push(test);
                this.promoCode = this.userCouponsStore.$state.userCoupons[0].promoCode;
            }


        }

    }
    calculatePrice(): number {
        let itemsInOrderSum: number = this.itemsInOrderStore.$state.itemsInOrder.length == 0 ?
            0 : this.itemsInOrderStore.$state.itemsInOrder.map(i => i.total).reduce((a, b) => a + b)
        let ticketInOrderSum: number = this.ticketsInOrderStore.$state.ticketsInOrder.length == 0 ?
            0 : this.ticketsInOrderStore.$state.ticketsInOrder.map(i => i.ticketPrice).reduce((a, b) => a + b);
        return itemsInOrderSum + ticketInOrderSum;
    }

    calculateTotal(): number {
        let priceWithoutDiscount = this.calculatePrice();
        if (this.userCouponsStore.$state.userCoupons.length !== 0) {
            priceWithoutDiscount = priceWithoutDiscount * (1 - this.userCouponsStore.$state.userCoupons[0].couponCategoryDiscount / 100);
        }
        return priceWithoutDiscount;
    }

    async deleteTicketInOrder(ticketInOrderId: string): Promise<void> {
        await this.ticketInOrderService.delete(ticketInOrderId);
        this.ticketsInOrderStore.$state.ticketsInOrder = await this.ticketInOrderService.getTicketsInOrderByOrderId(this.ordersStore.$state.orders[0].id);
    }

    async deleteItemInOrder(itemInOrderId: string, amount: number) {
        await this.itemInOrderService.deleteItemInOrder(itemInOrderId, amount);
        this.itemsInOrderStore.$state.itemsInOrder
            = await this.itemInOrderService.getItemsByOrderId(this.ordersStore.$state.orders[0].id);
    }

}
</script>
