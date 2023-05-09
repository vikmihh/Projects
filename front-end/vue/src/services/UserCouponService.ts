import type { IUserCoupon } from "@/domain/IUserCoupon";
import httpClient from "@/http-client";
import type { AxiosError } from "axios";
import { BaseService } from "./BaseService";
import { IdentityService } from "./IdentityService";

export class UserCouponService extends BaseService<IUserCoupon> {
    constructor() {
        super("usersCoupon");

    }

    async getCouponByOrderId(orderId: string): Promise<IUserCoupon> {
        console.log("getUsersCouponByOrderId");
        try {
            let response = await httpClient.get(`/usersCoupon/orderby/${orderId}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }
            });
            console.log(response);
            let res = response.data as IUserCoupon;
            return res;
        } catch (e) {
            let response = (e as AxiosError).response!;
            let res = response.data as IUserCoupon;
            return res;
        }
    }


    async activateCoupon(promoCode: string,adding:boolean): Promise<IUserCoupon> {
        console.log("activatePromo");
        let response = await httpClient.get(`/usersCoupon/promo/${promoCode}/adding/${adding}`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as IUserCoupon;
        return res;
    }
}