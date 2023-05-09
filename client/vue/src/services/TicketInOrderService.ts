import type { ITicketInOrder } from "@/domain/ITicketInOrder";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class TicketInOrderService extends BaseService<ITicketInOrder> {
    constructor() {
        super("ticketsInOrder");
    }

    async addTicketInOrder(ticketId: string): Promise<ITicketInOrder> {
        console.log("addTicketInOrder");
        let response = await httpClient.post(`/ticketsInOrder/add/${ticketId}`,null, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as ITicketInOrder;
        return res;
    }

    async activateTicketInOrder(ticketId: string): Promise<ITicketInOrder> {
        console.log("addTicketInOrder");
        let response = await httpClient.post(`/ticketsInOrder/activate/${ticketId}`,null, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as ITicketInOrder;
        return res;
    }

    async getTicketsInOrderByOrderId(orderId:string): Promise<ITicketInOrder[]> {
        console.log("getTicketsInOrderByOrderId");
        let response = await httpClient.get(`/ticketsInOrder/orderby/${orderId}`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as ITicketInOrder[];
        return res;
    }
}