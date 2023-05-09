<template>
    <h1>{{ identityStore.jwt?.role != 'admin' ? "Buy pass" : "Tickets" }}</h1>
    <p v-if="identityStore.jwt?.role == 'admin'">
        <RouterLink to="/tickets/create">Create New</RouterLink>
    </p>
    <div class="h-100 row align-items-center">
        <table v-if="identityStore.jwt?.role != 'admin'">
            <tbody>
                <tr v-for="item of ticketsStore.tickets" :key="item.id">
                    <div class="card" style="width: 18rem;">
                        <div class="card-body">
                            <h5 class="card-title">{{ item.name }}</h5>
                            <p class="card-text"> Price: {{ item.price }}$</p>
                            <p class="card-text"> Duration: {{ item.dayRange }} day(s)</p>
                            <div class="input-group mb-3" style="width: 14rem;">
                                <div class="input-group-prepend">
                                    <button class="btn btn-primary" type="button" id="button-addon1"
                                        @click="addToOrder(item.id!)">Add to cart</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </tr>
            </tbody>
        </table>
        <table class="table" v-if="identityStore.jwt?.role == 'admin'">
            <thead>
                <tr>
                    <th>
                        Ticket name
                    </th>
                    <th>
                        Price
                    </th>
                    <th>
                        DayRange
                    </th>
                   
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item of ticketsStore.tickets"  :key="item.id">
                    <td>
                        {{ item.name }}
                    </td>
                    <td>
                        {{ item.price }}
                    </td>
                    <td>
                        {{ item.dayRange }}
                    </td>
                    
                    <td>
                        <RouterLink :to="'/tickets/edit/' + item.id ">Edit</RouterLink> |
                        <a @click="deleteTicket(item.id!)" class="btn btn-link" >Delete</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

</template>

<script lang="ts">
import router from "@/router";
import { TicketInOrderService } from "@/services/TicketInOrderService";
import { TicketService } from "@/services/TicketService";
import { useTicketStore } from "@/stores/tickets";
import { useTicketInOrderStore } from "@/stores/ticketsInOrderStore";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";
import { useIdentityStore } from "@/stores/identity";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class TicketIndex extends Vue {
    id: string = "id000";
    ticketsStore = useTicketStore();
    ticketService = new TicketService();
    ticketInOrderService = new TicketInOrderService();
    ticketsInOrderStore = useTicketInOrderStore();
    identityStore = useIdentityStore();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.ticketsStore.$state.tickets =
            await this.ticketService.getAll();
    }

    async addToOrder(ticketId: string): Promise<void> {
        if (this.identityStore.jwt == null) {
            router.push('/login')
            return;
        }
        this.ticketsInOrderStore.add(await this.ticketInOrderService.addTicketInOrder(ticketId));

    }

    async deleteTicket(ticketId: string): Promise<void> {
        var response=await this.ticketService.delete(ticketId);
        this.ticketsStore.$state.tickets = await this.ticketService.getAll();
           
        console.log(response);
    }

}
</script>


