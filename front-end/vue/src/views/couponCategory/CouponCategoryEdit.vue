<template>
    <h3>Edit coupon</h3>
            <div className="row">
                <div className="col-md-12">
                    <form >
                        <div className="form-group">
                            <label className="control-label">Name </label>
                            <input type="text" className="form-control" v-model="couponCategoryName"/>
                        </div>
                        <div className="form-group">
                            <label className="control-label">Discount</label>
                            <input type="text" className="form-control"  v-model="couponCategoryDiscount"/>
                        </div>
                        <div className="form-group">
                            <label className="control-label">Orders Amount</label>
                            <input type="text" className="form-control"  v-model="couponCategoryOrdersAmount"/>
                        </div>
                        <div class="form-group">
                            <a @click="submitClicked()" class="btn btn-primary" >Save</a>
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
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
    },
    props: {
        id: String
    },
    emits: [],
})

export default class CouponCategoryEdit extends Vue {
    id!: string;
    couponCategoryName: string = "";
    couponCategoryDiscount: string = "";
    couponCategoryOrdersAmount: string = "";

    couponCategoriesStore = useCouponCategoryStore();
    couponCategoryService = new CouponCategoryService();
     errorMessage: string | null = null;

    async mounted(): Promise<void> {
        console.log("mounted");
        this.couponCategoriesStore.$state.couponCategories = [];
        let couponCategory =  (await this.couponCategoryService.get(this.id));
        this.couponCategoryName = couponCategory.name;
        this.couponCategoryDiscount = couponCategory.discount;
        this.couponCategoryOrdersAmount = couponCategory.ordersAmount;
    }
    async submitClicked(): Promise<void>  {
        var res = await this.couponCategoryService.put(this.id, {
            id: this.id,
            name: this.couponCategoryName,
            discount: this.couponCategoryDiscount,
            ordersAmount: this.couponCategoryOrdersAmount
        });
        
         if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMsg;
                alert("Please fill all the fields!")
                console.log("res.status");
                console.log(res.status);
            } else {
                this.couponCategoriesStore.$state.couponCategories =
                    await this.couponCategoryService.getAll();

                this.$router.push('/couponcategory');
            }
        }
}
</script>