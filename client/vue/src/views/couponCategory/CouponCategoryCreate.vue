<template>
    <h3>Create new coupon</h3>
    <div className="row">
        <div className="col-md-12">
            <form>
                <div className="form-group">
                    <label className="control-label">Name </label>
                    <input type="text" v-model="couponCategoryName" className="form-control" />
                </div>
                <div className="form-group">
                    <label className="control-label">Discount</label>
                    <input type="number" v-model="couponCategoryDiscount" className="form-control" />
                </div>
                <div className="form-group">
                    <label className="control-label">Orders Amount</label>
                    <input type="number" v-model="couponCategoryOrdersAmount" className="form-control" />
                </div>
                <div class="form-group">
                    <a @click="submitClicked()" class="btn btn-primary">Create</a>
                </div>
                <RouterLink to="/couponcategory">Back to main</RouterLink>
            </form>
        </div>
    </div>

</template>

<script lang="ts">
import router from "@/router";
import { CouponCategoryService } from "@/services/CouponCategoryService";
import { useCouponCategoryStore } from "@/stores/couponCategories";
import { Vue } from "vue-class-component";


export default class CouponCategoryCreate extends Vue {
    couponCategoryName!: string;
    couponCategoryDiscount!: string;
    couponCategoryOrdersAmount!: string;
    couponCategoryService = new CouponCategoryService();
    couponCategoriesStore = useCouponCategoryStore();
    errorMessage: string | null = null;

    async submitClicked(): Promise<void> {
        console.log('submitClicked');
        var res = await this.couponCategoryService.add({
            name: this.couponCategoryName,
            discount: this.couponCategoryDiscount,
            ordersAmount: this.couponCategoryOrdersAmount
        });
        if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMsg;
                alert("Please fill all the fields!")
            } else {
                this.couponCategoriesStore.$state.couponCategories =
                    await this.couponCategoryService.getAll();

                this.$router.push('/couponcategory');
            }
    }
}
</script>