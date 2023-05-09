import type { IUserCategory } from "@/domain/IUserCategory";
import { UserCategoryService } from "@/services/UserCategoryService";
import { defineStore } from "pinia";

export const useUserCategoryStore = defineStore({
  id: "userCategories",
  state: () => ({
    userCategories: [{
    } 
  ] as IUserCategory[],
  }),
  getters: {
    userCategoryCount: (state) => state.userCategories.length
    
  },
  actions: {
    add(userCategory: IUserCategory) {
      this.userCategories.push(userCategory);
    },
    userCategoriesAll() {
      let service = new UserCategoryService();
    //   return service.getAll();
    }
  },
});
