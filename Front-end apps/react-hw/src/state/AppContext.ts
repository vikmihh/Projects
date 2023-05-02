import React from 'react';
import { IAppState } from './IAppState';


export const initialState: IAppState = {
    ticket: {
        id: "",
        name: "",
        dayRange: "",
        price: ""
    },
    menuItem: {
        id: "",
        itemCategoryId: "",
        itemCategoryName: "",
        itemName: "",
        description: "",
        price: ""
    },
    user: {
        firstName: '',
        lastName: '',
        email: '',
        password: ''
    },
    category: {
        id: '',
        name: ''
    },
    jwt: {
        token: '',
        refreshToken: '',
        firstName: '',
        lastName: '',
        email: '',
        role: ''
    },
    setMenuItem: () => { },
    setTicket: () => { },
    setUser: () => { },
    setJwt: () => { },
    setItemCategory: () => { }
};

export const AppContext = React.createContext<IAppState>(initialState);
export const AppContextProvider = AppContext.Provider;
