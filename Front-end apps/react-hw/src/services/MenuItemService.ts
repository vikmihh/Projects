import { useContext } from "react";
import { IItemCategory } from "../domain/IItemCategory";
import { IMenuItem } from "../domain/IMenuItem";
import { AppContext } from "../state/AppContext";
import { IAppState } from "../state/IAppState";
import { BaseService } from "./BaseService";
import httpClient from "./HttpClient";

export class MenuItemService extends BaseService<IMenuItem> {
    constructor(appState: IAppState) {
        super("menuitems", appState);

    }

    async create(itemCategoryName: string, itemCategoryId: string, itemName: string, description: string, price: string) {

        let menuItem: IMenuItem = {
            itemCategoryId: itemCategoryId,
            itemCategoryName: itemCategoryName,
            itemName: itemName,
            description: description,
            price: price
        };
        await this.add(menuItem);
    }

    async getAllByCategoryId(itemCategoryId: string): Promise<IMenuItem[]> {
        let response = await httpClient.get(`/menuItems/category/${itemCategoryId}`);
        let res = response.data as IMenuItem[];
        return res;
    }
}
