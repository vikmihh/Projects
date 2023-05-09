<template>
    <h1>Coupons I have</h1>
    <div class="h-100 row align-items-center">
        <table>

            <tbody>
                <tr v-for="item of couponsStore.userCoupons" :key="item.id">
                    <div class="card" style="width: 20rem;">
                        <div class="card-body">
                            <h5 class="card-title">Promocode: {{ item.promoCode }}</h5>
                            <p class="card-text"> Coupon category: {{ item.couponCategoryName }}</p>
                            <p class="card-text"> Discount: {{ item.couponCategoryDiscount }}%</p>

                        </div>
                    </div>
                </tr>

            </tbody>
        </table>

    </div>
</template>

<script lang="ts">
import { UserCouponService } from "@/services/UserCouponService";
import { useUserCouponStore } from "@/stores/userCoupons";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class CouponIndex extends Vue {
    id: string = "id000";
    couponsStore = useUserCouponStore();
    couponService = new UserCouponService();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.couponsStore.$state.userCoupons =
            await this.couponService.getAll();
    }

}
</script>
