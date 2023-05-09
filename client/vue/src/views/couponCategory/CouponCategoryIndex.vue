<template>
    <div>
        <h1>Coupons</h1>
        <p>
            <RouterLink to="/couponCategory/create">Create New</RouterLink>
        </p>
        <div class="container">
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            Name
                        </th>
                        <th>
                            Discount
                        </th>
                        <th>
                            Orders Amount
                        </th>
                        
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                     <tr v-for="item of couponCategoriesStore.couponCategories" :key="item.id">
                        <td>
                            {{ item.name }}
                        </td>
                        <td>
                            {{ item.discount }}
                        </td>
                        <td>
                            {{ item.ordersAmount }}
                        </td>
                        
                        <td>
                            <RouterLink :to="'/couponCategory/edit/' + item.id ">Edit</RouterLink> |
                            
                            <a @click="deleteItem(item.id!)" class="btn btn-link">Delete</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">

import { Options, Vue } from "vue-class-component";
import { CouponCategoryService } from "@/services/CouponCategoryService";
import { useCouponCategoryStore } from "@/stores/couponCategories";
import { useIdentityStore } from "@/stores/identity";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class CouponCategoryIndex extends Vue {
    couponCategoriesService = new CouponCategoryService();
    couponCategoriesStore = useCouponCategoryStore();
    identityStore = useIdentityStore();

    async mounted(): Promise<void> {
        console.log("mounted");
          this.couponCategoriesStore.$state.couponCategories = await this.couponCategoriesService.getAll();
    }

     async deleteItem(id:string){
         await this.couponCategoriesService.delete(id);
          this.couponCategoriesStore.$state.couponCategories =
            await this.couponCategoriesService.getAll();
            
    }
}
</script>
