import { IItemCategory } from "../domain/IItemCategory";
import { IAppState } from "../state/IAppState";
import { BaseService } from "./BaseService";

export class ItemCategoryService extends BaseService<IItemCategory> {
    constructor(appState:IAppState) {
        super("itemcategories",appState);
    }

    async create(name:string) {
        let category: IItemCategory = {
            name:name
        };
        this.add(category);
   }
}