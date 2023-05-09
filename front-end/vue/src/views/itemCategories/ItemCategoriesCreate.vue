<template>
    <h3>Create new item category</h3>
    <div className="row">
        <div className="col-md-12">
            <form>
                <div className="form-group">
                    <label className="control-label">Category name </label>
                    <input type="text" v-model="itemCategoryName" className="form-control" />
                </div>

                <div class="form-group">
                    <a @click="submitClicked()" class="btn btn-primary">Create</a>
                </div>
                <RouterLink to="/itemcategories">Back to main</RouterLink>
            </form>
        </div>
    </div>

</template>

<script lang="ts">
import router from "@/router";
import { ItemCategoryService } from "@/services/ItemCategoryService";
import { useItemCategoryStore } from "@/stores/itemCategories";
import { Vue } from "vue-class-component";


export default class ItemCategoriesCreate extends Vue {
    itemCategoryService = new ItemCategoryService();
    itemCategoriesStore = useItemCategoryStore();
    itemCategoryName!: string;
    errorMessage: string | null = null;

    async submitClicked(): Promise<void> {
        console.log('submitClicked');
        var res = await this.itemCategoryService.add({
            name: this.itemCategoryName
        });
       if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMsg;
                alert("Please fill all the fields!")
            } else {
                this.itemCategoriesStore.$state.itemCategories =
                    await this.itemCategoryService.getAll();

                this.$router.push('/itemcategories');
            }
    }
}
</script>