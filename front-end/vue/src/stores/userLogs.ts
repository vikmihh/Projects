import type { IUserLog } from "@/domain/IUserLog";
import { UserLogService } from "@/services/UserLogService";
import { defineStore } from "pinia";

export const useUserLogStore = defineStore({
  id: "userLogs",
  state: () => ({
    userLogs: [{
    } 
  ] as IUserLog[],
  }),
  getters: {
    userLogCount: (state) => state.userLogs.length
    
  },
  actions: {
    add(userLog: IUserLog) {
      this.userLogs.push(userLog);
    },
    userLogsAll() {
      let service = new UserLogService();
      return service.getAll();
    }
  },
});
