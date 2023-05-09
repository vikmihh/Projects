import type { IUserLog } from "@/domain/IUserLog";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class UserLogService  extends BaseService<IUserLog> {
    constructor() {
        super("userLogs");
        
    }

    async generatePass(ticketInOrderId: string): Promise<IUserLog> {
        console.log("addTicketInOrder");
        let response = await httpClient.post(`/userLogs/generate/${ticketInOrderId}`,null, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as IUserLog;
        return res;
    }
}