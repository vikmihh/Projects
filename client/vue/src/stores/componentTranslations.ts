import type { IComponentTranslation } from "@/domain/IComponentTranslation";
import { ComponentTranslationService } from "@/services/ComponentTranslationService";
import { defineStore } from "pinia";

export const useComponentTranslationStore = defineStore({
  id: "componentTranslations",
  state: () => ({
    componentTranslations: [{
    } 
  ] as IComponentTranslation[],
  }),
  getters: {
    componentTranslationCount: (state) => state.componentTranslations.length
    
  },
  actions: {
    add(componentTranslation: IComponentTranslation) {
      this.componentTranslations.push(componentTranslation);
    },
    componentTranslationsAll() {
      let service = new ComponentTranslationService();
    //   return service.getAll();
    }
  },
});
