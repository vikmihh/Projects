<template>
    <h1>Tickets I have</h1>
    <div class="h-100 row align-items-center">
        <table>

            <tbody>
                <tr v-for="item of ticketsInOrderStore.ticketsInOrder" :key="item.id">
                    <div class="card border-success" style="width: 18rem;" v-if="item.activated" >
                        <div class="card-body">
                            <h4 class="card-title">{{ item.ticketName }}</h4>
                            <h5 class="card-title">{{ item.ticketPrice }} $</h5>
                            <h6 class="card-title">Duration: {{ item.ticketDayRange }} day(s)</h6>
                            <p class="card-text">Valid From: {{ item.validFromStr }}</p>
                            <p class="card-text">Valid Until: {{ item.validUntilStr }}</p>
                            <button class="btn btn-link" type="button" href="#" disabled>Activate</button>
                            <RouterLink :to="{ name: 'userLogs', params: { id: item.id } }" class="btn btn-link" >Generate QR</RouterLink>
                            
                        </div>
                    </div>
                     <div class="card border-danger" style="width: 18rem;" v-if="!item.activated">
                        <div class="card-body">
                            <h4 class="card-title">{{ item.ticketName }}</h4>
                            <h5 class="card-title">{{ item.ticketPrice }} $</h5>
                            <h6 class="card-title">Duration: {{ item.ticketDayRange }} day(s)</h6>
                            <button class="btn btn-link" type="button" @click="activate(item.id)"  href="#">Activate</button>
                            <RouterLink  class="btn btn-link disabled" to="/userLogs">Generate QR</RouterLink>
                            
                        </div>
                    </div>
                </tr>

            </tbody>
        </table>
    </div>

   


</template>


<script lang="ts">
import { TicketInOrderService } from "@/services/TicketInOrderService";
import { useTicketInOrderStore } from "@/stores/ticketsInOrderStore";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class TicketInOrderIndex extends Vue {
    id: string = "id000";
    ticketsInOrderStore = useTicketInOrderStore();
    ticketInOrderService = new TicketInOrderService();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.ticketsInOrderStore.$state.ticketsInOrder =
            await this.ticketInOrderService.getAll();

    }

    async show() {
      
    }

    async activate(ticketInOrderId:string) {
       await this.ticketInOrderService.activateTicketInOrder(ticketInOrderId);
       this.ticketsInOrderStore.$state.ticketsInOrder =
            await this.ticketInOrderService.getAll();
    }

}
</script>

<style>
    .disabled {
    opacity: 0.5;
    pointer-events: none;
}
</style>


