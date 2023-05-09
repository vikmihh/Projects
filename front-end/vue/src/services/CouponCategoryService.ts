import type { ICouponCategory } from "@/domain/ICouponCategory";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class CouponCategoryService extends BaseService<ICouponCategory>{
    constructor() {
        super("couponCategories");
        
    }
}