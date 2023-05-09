<template>
    <div class="container">
        <h1>1. Your coordinates</h1>
        <label class="control-label" for="Location">Location</label>
        <div class="input-group mb-3">
            <select class="custom-select" id="inputGroupSelect01" @change="showSeats()" v-model="selection">
                <option v-for="item of coordinateLocationStore.$state.locations" :value="item.id">{{ item.location }}
                </option>
            </select>
        </div>

        <label class="control-label" for="Location">Your seat</label>
        <div class="input-group mb-3">
            <select class="custom-select" id="inputGroupSelect01" v-model="selectedCoordinateId">
                <option v-for="item of coordinatesStore.coordinates" :value="item.id">{{ item.index }}</option>
            </select>
        </div>

        <h1>2. Choose card</h1>
        <div class="input-group mb-3">
            <select class="custom-select" id="inputGroupSelect01" v-model="selectedCardId">
                <option v-for="item of cardsStore.cards" :value="item.id">{{ item.cardNumber }}</option>
            </select>
        </div>

        <h1>3. Order information</h1>
        <div v-for="item of ordersStore.orders" :key="item.id" class="card bg-light" style="width: 18rem;">
            <div class="card-body">
                <h6 class="card-subtitle mb-2 text-muted">Price: {{ calculatePrice() }}$</h6>
                <h6 class="card-subtitle mb-2 text-muted">Discount: {{ userCouponsStore.userCoupons.length !==
                        0 ? userCouponsStore.userCoupons[0].couponCategoryDiscount : 0
                }}%</h6>
                <h6 class="card-subtitle mb-2 text-muted">OrderNr: {{ item.orderNr }}</h6>
                <h5 class="card-title">Total: {{ calculateTotal() }}$</h5>
            </div>
        </div>
        <a class="btn btn-primary" @click="confirmOrder()">Confirm order</a>
    </div>

</template>


<script lang="ts">




import { CoordinateService } from "@/services/CoordinateService";
import { CoordinateLocationService } from "@/services/CoordinateLocationService";
import { useCoordinateStore } from "@/stores/coordinates";

import { CardService } from "@/services/CardService";
import { useCardStore } from "@/stores/cards";
import { Options, Vue } from "vue-class-component";
import { useOrderStore } from "@/stores/orders";
import { OrderService } from "@/services/OrderService";
import { useCoordinateLocationStore } from "@/stores/coordinateLocations";
import { ItemInOrderService } from "@/services/ItemInOrderService";
import { TicketInOrderService } from "@/services/TicketInOrderService";
import { useItemInOrderStore } from "@/stores/itemsInOrderStore";
import { useTicketInOrderStore } from "@/stores/ticketsInOrderStore";
import { useIdentityStore } from "@/stores/identity";
import router from "@/router";
import { useUserCouponStore } from "@/stores/userCoupons";
import { UserCouponService } from "@/services/UserCouponService";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class ProcessOrder extends Vue {
    id: string = "id000";
    selection: string = "";
    selectedCoordinateId: string = "";
    selectedCardId: string = "";
    promoCode: string = "";
    errorMessage: string | null = null;

    cardsStore = useCardStore();
    cardService = new CardService();
    ordersStore = useOrderStore();
    orderService = new OrderService();
    coordinateLocationStore = useCoordinateLocationStore();
    coordinateLocationService = new CoordinateLocationService();

    coordinatesStore = useCoordinateStore();
    coordinateService = new CoordinateService();

    ticketsInOrderStore = useTicketInOrderStore();
    ticketInOrderService = new TicketInOrderService();
    itemsInOrderStore = useItemInOrderStore();
    itemInOrderService = new ItemInOrderService();
    identityStore = useIdentityStore();
    userCouponsStore = useUserCouponStore();
    userCouponService = new UserCouponService();


    async mounted(): Promise<void> {
        console.log("mountedProcessOrder");
        this.coordinatesStore.$state.coordinates = [];
        this.cardsStore.$state.cards = await this.cardService.getAll();
        this.coordinateLocationStore.$state.locations = await this.coordinateLocationService.getAll();
        if (this.identityStore.jwt != null) {
            this.ordersStore.$state.orders = [];
            this.ordersStore.$state.orders.push(await this.orderService.getCurrentOrder());
            this.ticketsInOrderStore.$state.ticketsInOrder
                = await this.ticketInOrderService.getTicketsInOrderByOrderId(this.ordersStore.$state.orders[0].id);
            this.itemsInOrderStore.$state.itemsInOrder
                = await this.itemInOrderService.getItemsByOrderId(this.ordersStore.$state.orders[0].id);
            this.userCouponsStore.$state.userCoupons = [];

            let test = await this.userCouponService.getCouponByOrderId(this.ordersStore.$state.orders[0].id);
            this.userCouponsStore.$state.userCoupons = [];

            if (test.id != undefined) {
                this.userCouponsStore.$state.userCoupons.push(test);
                this.promoCode = this.userCouponsStore.$state.userCoupons[0].promoCode;
            }
        }

    }

    async showSeats() {
        this.coordinatesStore.$state.coordinates
            = await this.coordinateService.getCoordinatesByLocationId(this.selection);
    }

    async confirmOrder() {


        if (this.selection.length == 0 || this.selectedCoordinateId.length == 0 || this.selectedCardId.length == 0) {

            alert("Please fill all the fields!")
        }
        else {
            var res = await this.orderService.confirmOrder(this.selectedCardId,
                this.selectedCoordinateId, this.ordersStore.$state.orders[0]);
            router.push('/orderResult')
            console.log('pushed')
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

 
}
</script>