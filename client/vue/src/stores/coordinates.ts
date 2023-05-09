import type { ICoordinate } from "@/domain/ICoordinate";
import { CoordinateService } from "@/services/CoordinateService";
import { defineStore } from "pinia";

export const useCoordinateStore = defineStore({
  id: "coordinate",
  state: () => ({
    coordinates: [{
    } 
  ] as ICoordinate[],
  }),
  getters: {
    coordinateCount: (state) => state.coordinates.length
    
  },
  actions: {
    add(coordinate: ICoordinate) {
      this.coordinates.push(coordinate);
    },
    coordinatesAll() {
      let service = new CoordinateService();
      return service.getAll();
    }
  },
});
