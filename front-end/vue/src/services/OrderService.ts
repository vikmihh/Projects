import type { IOrder } from "@/domain/IOrder";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class OrderService extends BaseService<IOrder> {
    constructor() {
        super("order");
        
    }

    async getCurrentOrder(): Promise<IOrder> {
        console.log("addTicketInOrder");
        let response = await httpClient.get(`/order/current/`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as IOrder;
        return res;
    }

    async confirmOrder(selectedCardId:string, selectedCoordinateId:string,order:IOrder): Promise<IOrder> {
        console.log("confirmOrder");
        order.cardId = selectedCardId;
        order.coordinateId = selectedCoordinateId;
        let response = await httpClient.post(`/order/proceedOrder/`,order, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as IOrder;
        return res;
    }
}