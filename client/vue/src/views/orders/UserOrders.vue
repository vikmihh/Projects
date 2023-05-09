<template>
    <h1>Orders history</h1>

    <div class="h-100 row align-items-center">
        <table>
            <tbody>
                <tr v-for="item of ordersStore.orders" :key="item.id">
                    <div class="card border-success" style="width: 18rem;" v-if="item.orderNr" >
                        <div class="card-body">
                            <h3 class="card-text">OrderNr: {{ item.orderNr }}</h3>
                            <h5 class="card-title">Price: {{ item.price }}$</h5>
                            <h5 class="card-title">Discount: {{ item.discount }} %</h5>
                            <h4 class="card-title">Final price: {{ item.finalPrice }}$</h4>
                            <p class="card-text">CardNr: {{ item.cardNr }}</p>
                            <p class="card-text">Ordered At: {{item.orderedAt}}</p>
                        </div>
                    </div>
                </tr>
            </tbody>
        </table>
    </div>

</template>

<script lang="ts">
import { OrderService } from "@/services/OrderService";
import { useOrderStore } from "@/stores/orders";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props:{},
    emits: [],
})
export default class OrderIndex extends Vue {
    id : string = "id000";
    ordersStore = useOrderStore();
    orderService = new OrderService();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.ordersStore.$state.orders =
            await this.orderService.getAll();
    }




}
</script>
