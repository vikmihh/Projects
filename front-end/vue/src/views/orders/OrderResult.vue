<template>
    <h1>Success!!</h1>




</template>

<script lang="ts">
import { ItemInOrderService } from "@/services/ItemInOrderService";
import { OrderService } from "@/services/OrderService";
import { TicketInOrderService } from "@/services/TicketInOrderService";
import { useItemInOrderStore } from "@/stores/itemsInOrderStore";
import { useOrderStore } from "@/stores/orders";
import { useTicketInOrderStore } from "@/stores/ticketsInOrderStore";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class OrderResult extends Vue {
    id: string = "id000";
    ordersStore = useOrderStore();
    orderService = new OrderService();
    ticketsInOrderStore = useTicketInOrderStore();
    ticketInOrderService = new TicketInOrderService();
    itemsInOrderStore = useItemInOrderStore();
    itemInOrderService = new ItemInOrderService();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.ordersStore.$state.orders =
            await this.orderService.getAll();
    }
   

}
</script>
