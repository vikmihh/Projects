<template>
    <h3>Create new ticket</h3>
    <div className="row">
        <div className="col-md-12">
            <form>
                <div className="form-group">
                    <label className="control-label">Ticket name </label>
                    <input type="text" v-model="ticketName" className="form-control" />
                </div>
                <div className="form-group">
                    <label className="control-label">Day Range</label>
                    <input type="number" v-model="ticketDayRange" className="form-control" />
                </div>
                <div className="form-group">
                    <label className="control-label">Price</label>
                    <input type="number" v-model="ticketPrice" className="form-control" />
                </div>
                <div class="form-group">
                    <a @click="submitClicked()" class="btn btn-primary">Create</a>
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
import { Vue } from "vue-class-component";




export default class TicketCreate extends Vue {
    ticketName!: string;
    ticketPrice!: string;
    ticketDayRange!: string;
    ticketService = new TicketService();
    ticketsStore = useTicketStore();
    errorMessage: string | null = null;

    async submitClicked(): Promise<void> {
    
            var res = await this.ticketService.add({
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