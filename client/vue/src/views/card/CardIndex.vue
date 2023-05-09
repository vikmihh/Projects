<template>
    <h1>Cards I have</h1>

    <p>
        <RouterLink to="/cards/create">Add new card</RouterLink>
    </p>

    <div class="h-100 row align-items-center">
        <table>

            <tbody>
                <tr v-for="item of cardsStore.cards" :key="item.id">
                    <div class="card" style="width: 18rem;">
                        <div class="card-body">
                            <h5 class="card-title">{{ item.firstName }} {{ item.lastName }}</h5>
                            <p class="card-text"> Card Num: {{ item.cardNumber }}</p>
                            <p class="card-text"> SecurityCode {{ item.securityCode }}</p>
                            <p class="card-text"> Expity month {{ item.expiryMonth }}</p>
                            <p class="card-text"> Expity year {{ item.expiryYear }}</p>
                            <RouterLink :to="'/cards/edit/' + item.id " class="btn btn-link">Edit
                            </RouterLink>
                            <a @click="deleteItem(item.id!)" class="btn btn-danger">Delete</a>
                        
                        </div>
                    </div>
                </tr>
            </tbody>
        </table>
    </div>



</template>

<script lang="ts">
import { CardService } from "@/services/CardService";
import { useCardStore } from "@/stores/cards";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class CardIndex extends Vue {
    id: string = "id000";
    cardsStore = useCardStore();
    cardService = new CardService();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.cardsStore.$state.cards =
            await this.cardService.getAll();
    }

    async deleteItem(cardId:string) {
        await this.cardService.delete(cardId);
        this.cardsStore.$state.cards =
            await this.cardService.getAll();
    }

}
</script>
