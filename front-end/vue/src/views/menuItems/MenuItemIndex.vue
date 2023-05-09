<template>
    <h1>Menu</h1>
    <div class="container">
        <ul class="nav" v-if="identityStore.jwt?.role != 'admin'">
            <li class="nav-item" v-for="item of itemCategoriesStore.itemCategories" :key="item.id">
                <a href="#" class="nav-link" :class="{ 'disabled': selectedItemCategory == item.id }"
                    @click="handle(item.id!)">{{ item.name }}</a>
            </li>

        </ul>

        <table v-if="identityStore.jwt?.role != 'admin'">

            <tbody>
                <tr v-for="item of menuItemsStore.menuItems" :key="item.id">
                    <div class="card" style="width: 18rem;">
                        <img v-if="item.itemCategoryName == 'Sandwiches'" class="card-img-top"
                            src="https://cdn-icons-png.flaticon.com/512/1942/1942895.png" alt="Card image cap">
                        <img v-else-if="item.itemCategoryName == 'Drinks'" class="card-img-top"
                            src="https://cdn-icons-png.flaticon.com/512/161/161542.png" alt="Card image cap">
                        <img v-else class="card-img-top"
                            src="https://uxwing.com/wp-content/themes/uxwing/download/20-food-and-drinks/meal-food.png"
                            alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">{{ item.itemName }}</h5>
                            <h6 class="card-subtitle mb-2 text-muted"> {{ item.price }}</h6>
                            <p class="card-text"> {{ item.description }}</p>
                            <div class="input-group mb-3" style="width: 14rem;">
                                <input type="number" min="1" class="form-control" v-model="amount">
                                <div class="input-group-prepend">
                                    <button class="btn btn-primary" type="button" @click="addToOrder(item.id!)"
                                        id="button-addon1">Add to cart</button>
                                </div>

                            </div>
                        </div>
                    </div>
                </tr>
            </tbody>
        </table>
        <div v-if="identityStore.jwt?.role == 'admin'">
            <RouterLink to="/menuitems/create">Create New</RouterLink>
            <table class="table">

                <thead>
                    <tr>
                        <th>
                            Item name
                        </th>
                        <th>
                            Description
                        </th>
                        <th>
                            Price
                        </th>
                        <th>
                            ItemCategory
                        </th>

                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item of menuItemsStore.menuItems">
                        <td>
                            {{ item.itemName }}
                        </td>
                        <td>
                            {{ item.description }}
                        </td>
                        <td>
                            {{ item.price }}
                        </td>
                        <td>
                            {{ item.itemCategoryName }}
                        </td>


                        <td>
                            <RouterLink :to="'/menuitems/edit/' + item.id">Edit
                            </RouterLink> |
                            <a @click="deleteItem(item.id!)" class="btn btn-link">Delete</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>


</template>

<script lang="ts">
import router from "@/router";
import { MenuItemService } from "@/services/MenuItemService";
import { useMenuItemStore } from "@/stores/menuItems";
import { ItemCategoryService } from "@/services/ItemCategoryService";
import { useItemCategoryStore } from "@/stores/itemCategories";
import { Options, Vue } from "vue-class-component";
import { ItemInOrderService } from "@/services/ItemInOrderService";
import { useItemInOrderStore } from "@/stores/itemsInOrderStore";
import { useIdentityStore } from "@/stores/identity";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class MenuItemIndex extends Vue {
    id: string = "id000";
    selectedItemCategory = "";
    menuItemsStore = useMenuItemStore();
    itemCategoriesStore = useItemCategoryStore();
    menuItemService = new MenuItemService();
    itemCategoryService = new ItemCategoryService();
    itemsInOrderStore = useItemInOrderStore();
    itemInOrderService = new ItemInOrderService();
    identityStore = useIdentityStore();
    amount: number = 1;

    async mounted(): Promise<void> {
        console.log("mounted");
        this.itemCategoriesStore.$state.itemCategories = await this.itemCategoryService.getAll()

        this.handle(this.itemCategoriesStore.$state.itemCategories[0].id!);


    }
    async handle(itemCategoryId: string) {
        this.selectedItemCategory = itemCategoryId
        this.menuItemsStore.$state.menuItems = this.identityStore.jwt?.role != 'admin' ?
            await this.menuItemService.getAllByCategoryId(this.selectedItemCategory) :  await this.menuItemService.getAll();
    }

    async addToOrder(menuItemId: string): Promise<void> {
        if (this.identityStore.jwt == null) {
            router.push('/login')
            return;
        }
        this.itemsInOrderStore.add(await this.itemInOrderService.addItemInOrder(menuItemId, this.amount));

    }

    async deleteItem(id: string) {
        await this.menuItemService.delete(id);
        this.menuItemsStore.$state.menuItems =
            await this.menuItemService.getAll();

    }
}
</script>
