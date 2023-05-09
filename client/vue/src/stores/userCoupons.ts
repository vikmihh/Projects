import type { IUserCoupon } from "@/domain/IUserCoupon";
import { UserCouponService } from "@/services/UserCouponService";
import { defineStore } from "pinia";

export const useUserCouponStore = defineStore({
  id: "usersCoupon",
  state: () => ({
    userCoupons: [{
    } 
  ] as IUserCoupon[],
  }),
  getters: {
    userCouponCount: (state) => state.userCoupons.length
    
  },
  actions: {
    add(userCoupon: IUserCoupon) {
      this.userCoupons.push(userCoupon);
    },
    userCouponsAll() {
      let service = new UserCouponService();
      return service.getAll();
    }
  },
});
