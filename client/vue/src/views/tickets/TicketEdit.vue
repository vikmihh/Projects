<template>
    <h3>Edit ticket</h3>
    <div className="row">
        <div className="col-md-12">
            <form>
               
                <div className="form-group">
                    <label className="control-label">Ticket name </label>
                    <input type="text" className="form-control" v-model="ticketName" />
                </div>

                
                <div className="form-group">
                    <label className="control-label">Day Range</label>
                    <input type="number" className="form-control" v-model="ticketDayRange" />
                </div>

               
                <div className="form-group">
                    <label className="control-label">Price</label>
                    <input type="number" className="form-control" v-model="ticketPrice" />
                </div>

                <div class="form-group">
                    <a @click="submitClicked()" class="btn btn-primary">Save</a>
                </div>
                <RouterLink to="/tickets">Back to main</RouterLink>
            </form>
        </div>
    </div>

</template>

<script lang="ts">
import router from "@/router";
import { TicketService } from "@/services/TicketService";
import { useTicketStore } from "@/stores/tickets";
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
    },
    props: {
        id: String
    },
    emits: [],
})

export default class TicketCreate extends Vue {
    id!: string;
    ticketName: string = "";
    ticketPrice: string = "";
    ticketDayRange: string = "";
    ticketsStore = useTicketStore();
    ticketService = new TicketService();
    errorMessage: string | null = null;

    async mounted(): Promise<void> {
        console.log("mounted");
        this.ticketsStore.$state.tickets = [];
        this.ticketName = (await this.ticketService.get(this.id)).name;
        this.ticketPrice = (await this.ticketService.get(this.id)).price;
        this.ticketDayRange = (await this.ticketService.get(this.id)).dayRange;
    }

    async submitClicked(): Promise<void> {

            var res = await this.ticketService.put(this.id, {
                id: this.id,
                name: this.ticketName,
                price: this.ticketPrice,
                dayRange: this.ticketDayRange
            });

            if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMsg;
                alert("Please fill all the fields!")
            } else {
                this.ticketsStore.$state.tickets =
                    await this.ticketService.getAll();

                this.$router.push('/tickets');
            }
        } 
    }

   

</script>