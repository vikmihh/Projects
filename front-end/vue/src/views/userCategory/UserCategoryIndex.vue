<template>
    <div>
        <h1>User categories</h1>
        <p>
            <RouterLink to="/usercategories/create">Create New</RouterLink>
        </p>
        <div class="container">
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            Category name
                        </th>
                        <th>
                            Orders Amount
                        </th>
                        
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item of userCategoryStore.userCategories" :key="item.id">
                        <td>
                            {{ item.categoryName }}
                        </td>
                        <td>
                            {{ item.ordersAmount }}
                        </td>
                        
                        <td>
                            <RouterLink  :to="'/userCategories/edit/' + item.id">Edit</RouterLink> |
                           
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
import { UserCategoryService } from "@/services/UserCategoryService";
import { useUserCategoryStore } from "@/stores/userCategories";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class UserCategoryIndex extends Vue {
    userCategoryStore = useUserCategoryStore();
    userCategoryService = new UserCategoryService();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.userCategoryStore.$state.userCategories = await this.userCategoryService.getAll();

    }

    async deleteItem(id:string){
         await this.userCategoryService.delete(id);
          this.userCategoryStore.$state.userCategories =
            await this.userCategoryService.getAll();
    }
}
</script>
