<template>
    <h1>Activated</h1>
    
    <div class="h-100 row align-items-center">
        <table  v-for="item of userLogsStore.userLogs" :key="item.id">
           <img src="https://api.qrserver.com/v1/create-qr-code/?size=150x150&data={{item.fromStr}}DD{{item.untillStr}}UID{{item.appUserId}}ULID{{item.id}}"
                    class=" image-fluid " style="width:10em;">
            <tbody>
                <tr>
                    <div class="card" style="width: 18rem;">
                        <div class="card-body">
                            <h5 class="card-title">Valid from: {{ item.fromStr }}</h5>
                            <h5 class="card-text"> Valid until: {{ item.untilStr }}</h5>
                        </div>
                    </div>
                </tr>

            </tbody>
        </table>
        <div>
             <RouterLink class="nav-link active" to="/ticketsInOrder">Back to List</RouterLink>
    </div>

    </div>



</template>

<script lang="ts">
import { UserLogService } from "@/services/UserLogService";
import { useUserLogStore } from "@/stores/userLogs";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {
        id: String
    },
    emits: [],
})
export default class UserLogIndex extends Vue {
    id!: string;
    userLogsStore = useUserLogStore();

    userLogService = new UserLogService();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.userLogsStore.$state.userLogs = [];
        this.userLogsStore.$state.userLogs.push( await this.userLogService.generatePass(this.id));
    }
    

}
</script>