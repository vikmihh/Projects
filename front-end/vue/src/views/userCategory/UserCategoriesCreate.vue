<template>
    <h3>Create new user category</h3>
            <div className="row">
                <div className="col-md-12">
                    <form >
                        <div className="form-group">
                            <label className="control-label">Category name </label>
                            <input type="text" v-model="userCategoryName" className="form-control"/>
                        </div>
                        <div className="form-group">
                            <label className="control-label">Orders Amount </label>
                            <input type="text" v-model="ordersAmount" className="form-control"/>
                        </div>
                        
                        <div class="form-group">
                            <a @click="submitClicked()" class="btn btn-primary" >Create</a>
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
import { Vue } from "vue-class-component";


export default class UserCategoriesCreate extends Vue {
    userCategoryName!: string;
    ordersAmount!: string;
    userCategoryService = new UserCategoryService();
    userCategorieStore= useUserCategoryStore();
     errorMessage: string | null = null;

    async submitClicked(): Promise<void>  {
        console.log('submitClicked');
        var res = await this.userCategoryService.add({
            categoryName: this.userCategoryName,
            ordersAmount: this.ordersAmount
        });
        if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMessage;
                alert("Please fill all the fields!")
            } else {
                this.userCategorieStore.$state.userCategories =
                    await this.userCategoryService.getAll();

                this.$router.push('/userCategory');
            }
        }
}
</script>