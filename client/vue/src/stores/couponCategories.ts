import type { ICouponCategory } from "@/domain/ICouponCategory";
import { CouponCategoryService } from "@/services/CouponCategoryService";
import { defineStore } from "pinia";

export const useCouponCategoryStore = defineStore({
  id: "couponCategory",
  state: () => ({
    couponCategories: [{
    } 
  ] as ICouponCategory[],
  }),
  getters: {
    couponCategoryCount: (state) => state.couponCategories.length
    
  },
  actions: {
    add(couponCategory: ICouponCategory) {
      this.couponCategories.push(couponCategory);
    },
    couponCategoriesAll() {
      let service = new CouponCategoryService();
      return service.getAll();
    }
  },
});
