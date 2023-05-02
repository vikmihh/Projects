import { IItemCategory } from "../domain/IItemCategory";
import { IJWTResponse } from "../domain/IJWTResponse";
import { IMenuItem } from "../domain/IMenuItem";
import { ITicket } from "../domain/ITicket";
import { IUser } from "../domain/IUser";

export interface IAppState {
    ticket: ITicket;
    menuItem: IMenuItem;
    jwt: IJWTResponse;
    user: IUser;
    category: IItemCategory;
    setJwt:(jwt: IJWTResponse)=> void;
    setUser: (user: IUser) => void;
    setTicket: (ticket: ITicket) => void;
    setMenuItem: (menuItem: IMenuItem) => void;
    setItemCategory: (category: IItemCategory) => void;
}
