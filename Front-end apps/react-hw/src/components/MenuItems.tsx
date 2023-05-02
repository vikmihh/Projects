import { appendFileSync } from 'fs';
import { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { IItemCategory } from '../domain/IItemCategory';
import { IMenuItem } from '../domain/IMenuItem';
import { ItemCategoryService } from '../services/ItemCategoryService';
import { MenuItemService } from '../services/MenuItemService';
import { AppContext } from '../state/AppContext';


const initialItem: IMenuItem[] = [];
const initialCategory: IItemCategory[] = [];

const MenuItems = () => {
    const userRole = useContext(AppContext).jwt.role;
    let selectedItemCategory: string = '';
    let itemCategoryName: string;
    const menuItemService = new MenuItemService(useContext(AppContext));
    const itemCategoryService = new ItemCategoryService(useContext(AppContext));
    const [menuItems, setMenuItems] = useState(initialItem);
    const [categories, setCategories] = useState(initialCategory);

    useEffect(() => {
        itemCategoryService.getAll().then(data => {
            setCategories(data);
            handle(data[0].id!);
        }
        );
        if (userRole == "admin") {
            menuItemService.getAll().then(data => setMenuItems(data));
        }
    }, []);

    const handle = (itemId: string) => {
        if (userRole != "admin") {
            selectedItemCategory = itemId;
            menuItemService.getAllByCategoryId(selectedItemCategory).then(data => setMenuItems(data));
        }
    };

    return (
        <>
            <h3>Menu</h3>
            {userRole != 'admin' &&
                <>
                    <ul className="nav">
                        {categories.map(item => {
                            return (
                                <li className="nav-item" key={item.id} onClick={() => handle(item.id!)}>
                                    <a href="#" className="nav-link"  >{item.name}
                                        {selectedItemCategory == item.id} </a>
                                </li>
                            )
                        }
                        )}
                    </ul>
                    <div className="h-100 row align-items-center">
                        <table>
                            {menuItems.map(item => {
                                return (
                                    <tr key={item.id}>
                                        <div className="card" >

                                            {item.itemCategoryName == "Sandwiches" &&
                                                <img src="https://cdn-icons-png.flaticon.com/512/1942/1942895.png" className="card-img-top"></img>
                                            }
                                            {item.itemCategoryName == "Drinks" &&
                                                <img src="https://cdn-icons-png.flaticon.com/512/161/161542.png" className="card-img-top"></img>
                                            }
                                            {item.itemCategoryName != "Drinks" && item.itemCategoryName != "Sandwiches" &&
                                                <img src="https://uxwing.com/wp-content/themes/uxwing/download/20-food-and-drinks/meal-food.png" className="card-img-top"></img>
                                            }
                                            <div className="card-body">
                                                <h5 className="card-title">{item.itemName}</h5>
                                                <p className="card-text"> Price: {item.price} $</p>
                                                <p className="card-text"> Description: {item.description}</p>
                                            </div>
                                        </div>
                                    </tr>
                                );
                            })}
                        </table>
                    </div>
                </>
            }

            {userRole == 'admin' &&
                <>
                    <Link to={`/menuitems/create`}>Create new</Link>
                    <div className="container">
                        <ul className="nav">
                            <li className="nav-item"></li>
                        </ul>
                        <table className="table">
                            <thead>
                                <tr>
                                    <th>
                                        Item name
                                    </th>
                                    <th>
                                        Price
                                    </th>
                                    <th>
                                        Description
                                    </th>
                                    <th>
                                        ItemCategory
                                    </th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {menuItems.map(item => {
                                    return (
                                        <>
                                            <tr>
                                                <td>
                                                    {item.itemName}
                                                </td>
                                                <td>
                                                    {item.price}
                                                </td>
                                                <td>
                                                    {item.description}
                                                </td>
                                                <td>
                                                    {item.itemCategoryName}

                                                </td>
                                                <td>
                                                    <Link to={`/menuItems/MenuItemsEdit/${item.id}`}>Edit</Link>
                                                </td>
                                            </tr>
                                        </>);
                                })}

                            </tbody>
                        </table>
                    </div>
                </>
            }
        </>
    );
}

export default MenuItems;

function setCategories(data: IItemCategory[]): any {
    throw new Error('Function not implemented.');
}
