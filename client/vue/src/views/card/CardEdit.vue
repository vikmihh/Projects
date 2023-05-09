<template>
    <h1>Edit card</h1>
    <hr />
    <div class="row">
        <div class="col-md-12">

            <!-- <div v-if="errorMessage != null" class="text-danger validation-summary-errors" data-valmsg-summary="true">
                <ul>
                    <li>{{ errorMessage }}</li>
                </ul>
            </div> -->

            <div>
                <div v-if="errorMessage != null && firstName.length < 1 || firstName.length >= 129"   class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Please fill the Name</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="FirstName">Name</label>
                    <input v-model="firstName" class="form-control" type="text" />
                </div>
                <div v-if="errorMessage != null && lastName.length < 1 || lastName.length >= 129"   class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Please fill the Surname</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="LastName">Surname</label>
                    <input v-model="lastName" class="form-control" type="text" />
                </div>
                <div v-if="errorMessage != null && cardNumber.toString().length != 16"   class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Card number must contain 16 numbers</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="CardNumber">Card number</label>
                    <input v-model="cardNumber" class="form-control" type="text" />
                </div>
                <div v-if="errorMessage != null && securityCode.toString().length != 3"   class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Card CVC must contain 3 numbers</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="SecurityCode">CVC</label>
                    <input v-model="securityCode" class="form-control" type="text" />
                </div>
                <div v-if="errorMessage != null && expiryMonth.toString().length != 2"   class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Expiry month must be 2 numbers</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="ExpiryMonth">Expiry month</label>
                    <input v-model="expiryMonth" class="form-control" type="number" />
                </div>
                <div v-if="errorMessage != null && expiryYear.toString().length != 4"   class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Expiry year must be 4 numbers</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="ExpiryYear">Expiry year</label>
                    <input v-model="expiryYear" class="form-control" type="number" />
                </div>
                
                <div class="form-group">
                    <a @click="submitClicked()" class="btn btn-primary" >Save</a>
                </div>
            </div>
        </div>
    </div>

    <div>
        <RouterLink class="nav-link active" to="/cards">Back to List</RouterLink>
    </div>
</template>


<script lang="ts">
import { CardService } from "@/services/CardService";
import type { ICard } from "@/domain/ICard";
import { useCardStore } from "@/stores/cards";
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
export default class CardCreate extends Vue {
    id!: string;
    cardNumber: string = '';
    securityCode: string = '';
    expiryMonth: string= '';
    expiryYear: string= '';
    firstName: string = '';
    lastName: string = '';
    card!:ICard;
    cardsStore = useCardStore();
    cardService = new CardService();


    
    errorMessage: string | null = null;

    async mounted(): Promise<void> {
        console.log("mounted");
        this.cardsStore.$state.cards = [];
        this.card = await this.cardService.get(this.id);
        this.firstName = this.card.firstName;
        this.lastName = this.card.lastName;
        this.cardNumber = this.card.cardNumber;
        this.securityCode = this.card.securityCode;
        this.expiryMonth = this.card.expiryMonth;
        this.expiryYear = this.card.expiryYear;
    }

    async submitClicked(): Promise<void>  {
        console.log('submitClicked');

        if (this.isFormCorrectlyFilled()) {


            var res = await this.cardService.put(this.id,
                {   id:this.id,
                    firstName: this.firstName,
                    lastName: this.lastName,
                    cardNumber: this.cardNumber,
                    securityCode: this.securityCode,
                    expiryMonth: this.expiryMonth,
                    expiryYear: this.expiryYear,
                    appUserId: this.card.appUserId
                }
            );

            if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMsg;
            } else {
                this.cardsStore.$state.cards =
                    await this.cardService.getAll();

                this.$router.push('/cards');
            }
        } else {
            this.errorMessage = 'Too short!';
        }
    }

    isFormCorrectlyFilled():boolean {
        return this.firstName.length > 0 && this.lastName.length > 0 &&
          this.securityCode.toString().length == 3 && 
          this.cardNumber.toString().length == 16 &&
         this.expiryMonth.toString().length == 2 &&
         this.expiryYear.toString().length == 4 
    }
}
</script>

