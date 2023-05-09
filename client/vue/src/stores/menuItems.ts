import type { IMenuItem } from "@/domain/IMenuItem";
import { MenuItemService } from "@/services/MenuItemService";
import { defineStore } from "pinia";

export const useMenuItemStore = defineStore({
  id: "menuItems",
  state: () => ({
    menuItems: [{
    } 
  ] as IMenuItem[],
  }),
  getters: {
    menuItemCount: (state) => state.menuItems.length
    
  },
  actions: {
    add(menuItem: IMenuItem) {
      this.menuItems.push(menuItem);
    },
    menuItemsAll() {
      let service = new MenuItemService();
      return service.getAll();
    }
  },
});
