import type { IUserInCategory } from "@/domain/IUserInCategory";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class UserInCategoryService  extends BaseService<IUserInCategory> {
    constructor() {
        super("userInCategory");
        
    }

    async getUserInCategory(): Promise<IUserInCategory> {
       
        let response = await httpClient.get(`/usersInCategory/current`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as IUserInCategory;
        return res;
    }
}