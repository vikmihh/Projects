import type { IItemCategory } from "@/domain/IItemCategory";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class ItemCategoryService extends BaseService<IItemCategory>{
    constructor() {
        super("itemCategories");
    }
}