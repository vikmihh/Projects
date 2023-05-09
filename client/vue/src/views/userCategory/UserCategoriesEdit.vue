<template>
    <h3>Edit user category</h3>
            <div className="row">
                <div className="col-md-12">
                    <form >
                        <div className="form-group">
                            <label className="control-label">Category name </label>
                            <input type="text" v-model="userCategoryName" className="form-control"/>
                        </div>
                        <div className="form-group">
                            <label className="control-label">Orders Amount </label>
                            <input type="text" v-model="orderAmount" className="form-control"/>
                        </div>
                        <div class="form-group">
                            <a @click="submitClicked()" class="btn btn-primary" >Save</a>
                        </div>
                        <RouterLink to="/userCategory">Back to main</RouterLink>
                    </form>
                </div>
            </div>

</template>

<script lang="ts">

import router from "@/router";
import { UserCategoryService } from "@/services/UserCategoryService";
import { useUserCategoryStore } from "@/stores/userCategories";
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
    },
    props: {
        id: String
    },
    emits: [],
})

export default class UserCategoriesEdit extends Vue {
    id!: string;
    userCategoryName: string = "";
    orderAmount: string = "";
    userCategoriesStore = useUserCategoryStore();
    userCategoryService = new UserCategoryService();
    errorMessage: string | null = null;

    async mounted(): Promise<void> {
            console.log("mounted");
            this.userCategoriesStore.$state.userCategories = [];
            this.userCategoryName = (await this.userCategoryService.get(this.id)).categoryName;
            this.orderAmount = (await this.userCategoryService.get(this.id)).ordersAmount;
        }

    async submitClicked(): Promise<void>  {
        var res = await this.userCategoryService.put(this.id, {
            id: this.id,
            categoryName: this.userCategoryName,
            ordersAmount: this.orderAmount
        });
        if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMessage;
                alert("Please fill all the fields!")
            } else {
                this.userCategoriesStore.$state.userCategories =
                    await this.userCategoryService.getAll();

                this.$router.push('/userCategory');
            }
        }
}
</script>