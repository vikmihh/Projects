import type { IItemInOrder } from "@/domain/IItemInOrder";
import { ItemInOrderService } from "@/services/ItemInOrderService";
import { defineStore } from "pinia";

export const useItemInOrderStore = defineStore({
  id: "itemsInOrder",
  state: () => ({
    itemsInOrder: [{
    } 
  ] as IItemInOrder[],
  }),
  getters: {
    ticketCount: (state) => state.itemsInOrder.length
    
  },
  actions: {
    add(ticketInOrder: IItemInOrder) {
      this.itemsInOrder.push(ticketInOrder);
    },
    ticketsAll() {
      let service = new ItemInOrderService();
      return service.getAll();
    }
  },
});