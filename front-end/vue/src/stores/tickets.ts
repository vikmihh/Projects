import type { ITicket } from "@/domain/ITicket";
import { TicketService } from "@/services/TicketService";
import { defineStore } from "pinia";

export const useTicketStore = defineStore({
  id: "tickets",
  state: () => ({
    tickets: [{
    } 
  ] as ITicket[],
  }),
  getters: {
    ticketCount: (state) => state.tickets.length
    
  },
  actions: {
    add(ticket: ITicket) {
      this.tickets.push(ticket);
    },
    ticketsAll() {
      let service = new TicketService();
      return service.getAll();
    }
  },
});
