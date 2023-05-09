import type { ITicketInOrder } from "@/domain/ITicketInOrder";
import { TicketInOrderService } from "@/services/TicketInOrderService";
import { defineStore } from "pinia";

export const useTicketInOrderStore = defineStore({
  id: "ticketsInOrder",
  state: () => ({
    ticketsInOrder: [{
    } 
  ] as ITicketInOrder[],
  }),
  getters: {
    ticketCount: (state) => state.ticketsInOrder.length
    
  },
  actions: {
    add(ticketInOrder: ITicketInOrder) {
      this.ticketsInOrder.push(ticketInOrder);
    },
    ticketsAll() {
      let service = new TicketInOrderService();
      return service.getAll();
    }
  },
});