import type { ICard } from "@/domain/ICard";
import { CardService } from "@/services/CardService";
import { defineStore } from "pinia";

export const useCardStore = defineStore({
  id: "cards",
  state: () => ({
    cards: [{
    } 
  ] as ICard[],
  }),
  getters: {
    cardCount: (state) => state.cards.length
    
  },
  actions: {
    add(card: ICard) {
      this.cards.push(card);
    },
    cardsAll() {
      let service = new CardService();
      return service.getAll();
    }
  },
});
