<template>
    <h3>Create new menu item</h3>
    <div className="row">
        <div className="col-md-12">
            <form>
                <div className="form-group">
                    <label className="control-label">Item name </label>
                    <input type="text" v-model="menuItemName" className="form-control" />
                </div>
                <div className="form-group">
                    <label className="control-label">Description</label>
                    <input type="text" v-model="menuItemDescription" className="form-control" />
                </div>
                <div className="form-group">
                    <label className="control-label">Price</label>
                    <input type="number" v-model="menuItemPrice" className="form-control" />
                </div>
                <div className="form-group">
                    <label className="control-label">Item Category</label>
                    <select class="form-control" data-val="true"
                        data-val-required="The ItemCategoryId field is required." id="ItemCategoryId"
                        name="ItemCategoryId" v-model="itemCategoryId">
                        <option v-for="item of categoriesStore.itemCategories" :value="item.id">{{ item.name }}
                        </option>
                    </select>
                </div>
                <div class="form-group">
                    <a @click="submitClicked()" type="submit" value="Create" class="btn btn-primary">Create</a>
                </div>
                <RouterLink to="/menuitems">Back to main</RouterLink>
            </form>
        </div>
    </div>

</template>

<script lang="ts">
import router from "@/router";
import { ItemCategoryService } from "@/services/ItemCategoryService";
import { MenuItemService } from "@/services/MenuItemService";
import { useItemCategoryStore } from "@/stores/itemCategories";
import { useMenuItemStore } from "@/stores/menuItems";
import { Vue } from "vue-class-component";


export default class MenuItemCreate extends Vue {
    menuItemName!: string;
    menuItemDescription!: string;
    menuItemPrice!: string;
    itemCategory!: string;
    itemCategoryId!: string;
    errorMessage: string | null = null;

    menuItemsStore = useMenuItemStore();
    menuItemService = new MenuItemService();

    categoriesStore = useItemCategoryStore();
    categoryService = new ItemCategoryService();

    async mounted(): Promise<void> {
        console.log("mounted");
        this.categoriesStore.$state.itemCategories = await this.categoryService.getAll();
    }

    async submitClicked(): Promise<void> {
    

        var res = await this.menuItemService.add({
            itemName: this.menuItemName,
            description: this.menuItemDescription,
            price: this.menuItemPrice,
            itemCategoryId: this.itemCategoryId,
           itemCategoryName:""
        });
        if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMsg;
                alert("Please fill all the fields!")
            } else {
                this.menuItemsStore.$state.menuItems =
                    await this.menuItemService.getAll();

                this.$router.push('/menuitems');
            }
        } 
    }

    

</script>