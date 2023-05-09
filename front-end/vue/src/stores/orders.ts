import type { IOrder } from "@/domain/IOrder";
import { OrderService } from "@/services/OrderService";
import { defineStore } from "pinia";

export const useOrderStore = defineStore({
  id: "order",
  state: () => ({
    orders: [{
    } 
  ] as IOrder[],
  }),
  getters: {
    orderCount: (state) => state.orders.length
    
  },
  actions: {
    add(order: IOrder) {
      this.orders.push(order);
    },
    ordersAll() {
      let service = new OrderService();
      return service.getAll();
    }
  },
});
