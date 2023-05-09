import type { IItemCategory } from "@/domain/IItemCategory";
import { ItemCategoryService } from "@/services/ItemCategoryService";
import { defineStore } from "pinia";

export const useItemCategoryStore = defineStore({
  id: "itemCategories",
  state: () => ({
    itemCategories: [{
    } 
  ] as IItemCategory[],
  }),
  getters: {
    itemCategoryCount: (state) => state.itemCategories.length
    
  },
  actions: {
    add(itemCategory: IItemCategory) {
      this.itemCategories.push(itemCategory);
    },
    itemCategoriesAll() {
      let service = new ItemCategoryService();
      return service.getAll();
    }
  },
});
