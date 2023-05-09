<template>
    <h3>Edit item category</h3>
    <div className="row">
        <div className="col-md-12">
            <form>
                <div className="form-group">
                    <label className="control-label">Category name </label>
                    <input type="text" className="form-control" v-model="itemCategoryName" />
                </div>

                <div class="form-group">
                    <a @click="submitClicked()" class="btn btn-primary"> Save</a>
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
import { Options, Vue } from "vue-class-component";
@Options({
    components: {
    },
    props: {
        id: String
    },
    emits: [],
})


export default class ItemCategoriesEdit extends Vue {
    id!: string;
    itemCategoriesStore = useItemCategoryStore();
    itemCategoryService = new ItemCategoryService();
    itemCategoryName: string = "";
    errorMessage: string | null = null;
    async mounted(): Promise<void> {
        console.log("mounted");
        this.itemCategoriesStore.$state.itemCategories = [];
        this.itemCategoryName = (await this.itemCategoryService.get(this.id)).name;
    }
    async submitClicked(): Promise<void> {
        var res = await this.itemCategoryService.put(this.id, {
            id: this.id,
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