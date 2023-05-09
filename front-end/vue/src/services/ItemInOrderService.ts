import type { IItemInOrder } from "@/domain/IItemInOrder";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class ItemInOrderService extends BaseService<IItemInOrder> {
    constructor() {
        super("itemsInOrder");
    }

    async addItemInOrder(menuItemId: string, amount: number): Promise<IItemInOrder> {
        console.log("addTicketInOrder");
        let response = await httpClient.post(`/itemsInOrder/add/${menuItemId}/amount/${amount}`,null, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as IItemInOrder;
        return res;
    }


    async getItemsByOrderId(orderId:string): Promise<IItemInOrder[]> {
        console.log("getItemsByOrderId");
        let response = await httpClient.get(`/itemsInOrder/orderby/${orderId}`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as IItemInOrder[];
        return res;
    }

    async deleteItemInOrder(itemInOrderId: string, amount: number): Promise<IItemInOrder> {
        console.log("addTicketInOrder");
        let response = await httpClient.delete(`/itemsInOrder/${itemInOrderId}/amount/${amount}`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as IItemInOrder;
        return res;
    }

}