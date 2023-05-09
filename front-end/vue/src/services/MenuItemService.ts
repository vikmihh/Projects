import type { IMenuItem } from "@/domain/IMenuItem";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";
import type { AxiosError } from "axios";
import { IdentityService } from "./IdentityService";

export class MenuItemService extends BaseService<IMenuItem> {
    constructor() {
        super("menuItems");
        
    }


    async getAllByCategoryId(itemCategoryId:string ) : Promise<IMenuItem[]>{
        console.log("called");
        try{
            console.log(this.identityStore.$state.jwt?.token);
            let response = await httpClient.get(`/menuItems/category/${itemCategoryId}`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        let res = response.data as IMenuItem[];
        return res;
        }catch(e){
            let response = (e as AxiosError).response!;
            if(response.status == 401 && this.identityStore.jwt){
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if ( !this.identityStore.$state.jwt) return [];
                

                let response = await httpClient.get(`/menuItems/category/${itemCategoryId}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });
                console.log(response);
    
                let res = response.data as IMenuItem[];
                return res;

            }
        }
        return [];
    }
}