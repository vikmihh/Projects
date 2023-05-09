import type { IUserCategory } from "@/domain/IUserCategory";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class UserCategoryService  extends BaseService<IUserCategory>{
    constructor() {
        super("userscategory");
        
    }
}