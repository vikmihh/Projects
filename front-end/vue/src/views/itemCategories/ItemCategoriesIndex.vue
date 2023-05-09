<template>
    <div>
        <h1>Menu items categories</h1>
        <p>
            <RouterLink to="/itemcategories/create">Create New</RouterLink>
        </p>
        <div class="container">
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            Name
                        </th>

                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item of categoriesStore.itemCategories" :key="item.id">
                        <td>
                            {{ item.name }}
                        </td>

                        <td>
                            | <RouterLink :to="'/itemCategories/edit/' + item.id ">Edit
                            </RouterLink>

                            <a @click="deleteItem(item.id!)" class="btn btn-link">Delete</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
import { ItemCategoryService } from "@/services/ItemCategoryService";
import { useItemCategoryStore } from "@/stores/itemCategories";
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class ItemCategoriesIndex extends Vue {
    categoriesStore = useItemCategoryStore();
    categoryService = new ItemCategoryService();
    async mounted(): Promise<void> {
        console.log("mounted");
        this.categoriesStore.$state.itemCategories =
            await this.categoryService.getAll();
    }

    async deleteItem(id:string){
         await this.categoryService.delete(id);
          this.categoriesStore.$state.itemCategories =
            await this.categoryService.getAll();
            
    }
}
</script>
