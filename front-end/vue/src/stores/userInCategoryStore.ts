

import type { IUserInCategory } from "@/domain/IUserInCategory";
import { defineStore } from "pinia";

export const useUserInCategoryStore = defineStore({
  id: "userInCategory",
  state: () => ({
    userInCategories: [{
    } 
  ] as IUserInCategory[],
  }),
  getters: {
    
    
  },
  actions: {
    add(userInCategory: IUserInCategory) {
      this.userInCategories.push(userInCategory);

    }
  },
});