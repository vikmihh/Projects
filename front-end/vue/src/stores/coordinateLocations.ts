import type { ICoordinateLocation } from "@/domain/ICoordinateLocation";
import { CoordinateLocationService } from "@/services/CoordinateLocationService";
import { defineStore } from "pinia";

export const useCoordinateLocationStore = defineStore({
  id: "coodinateLocation",
  state: () => ({
    locations: [{
    } 
  ] as ICoordinateLocation[],
  }),
  getters: {
    coordinateCount: (state) => state.locations.length
    
  },
  actions: {
    add(coordinate: ICoordinateLocation) {
      this.locations.push(coordinate);
    },
    coordinatesAll() {
      let service = new CoordinateLocationService();
      return service.getAll();
    }
  },
});
