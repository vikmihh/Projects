import { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { IItemCategory } from '../../domain/IItemCategory';
import { IMenuItem } from '../../domain/IMenuItem';
import { ItemCategoryService } from '../../services/ItemCategoryService';
import { MenuItemService } from '../../services/MenuItemService';
import { AppContext } from '../../state/AppContext';

const initialState: IItemCategory[] = [];

let initialMenuItem: IMenuItem = {
    id: "",
    itemCategoryId: "",
    itemCategoryName: "",
    itemName: "",
    description: "",
    price: ""
};

const MenuItemCreate = () => {
    let appState = useContext(AppContext);
    const itemCategoryService = new ItemCategoryService(appState);
    const menuItemService = new MenuItemService(appState);
    const [menuItem, setMenuItem] = useState(initialMenuItem);
    const [categories, setCategories] = useState(initialState);
    const [jwt, setJwt] = useState(appState.jwt);
    useEffect(() => {
        itemCategoryService.getAll().then(data => {
            setCategories(data);
            menuItem.itemCategoryId = data[0].id!;
            appState.setJwt(jwt);
            setMenuItem(menuItem);
        });
    }, []);

    const handleSubmit = () => {
        menuItemService.create(menuItem.itemCategoryName, menuItem.itemCategoryId, menuItem.itemName, menuItem.description, menuItem.price);

    }

    return (
        <>
            <h3>Create new menu item</h3>

            <div className="row">
                <div className="col-md-12">
                    <div className="form-group">
                        <label className="control-label">Item Name</label>
                        <input type="text" className="form-control" value={menuItem.itemName} onChange={(e) => {
                            menuItem.itemName = e.target.value;
                            appState.setJwt(jwt);
                            setMenuItem(menuItem);
                        }}
                        />
                    </div>
                    <div className="form-group">
                        <label className="control-label">Description</label>
                        <input type="text" className="form-control" value={menuItem.description} onChange={(e) => {

                            menuItem.description = e.target.value;
                            appState.setJwt(jwt);
                            setMenuItem(menuItem);
                        }} />
                    </div>
                    <div className="form-group">
                        <label className="control-label">Price</label>
                        <input type="number" className="form-control" value={menuItem.price} onChange={(e) => {

                            menuItem.price = e.target.value;
                            appState.setJwt(jwt);
                            setMenuItem(menuItem);
                        }} />
                    </div>
                    <div className="form-group">
                        <label className="control-label">Item Category</label>
                        <select className="form-control" data-val="true"
                            onChange={(e) => {
                                menuItem.itemCategoryId = e.target.value;
                                appState.setJwt(jwt);
                                setMenuItem(menuItem);
                            }} >
                            {categories.map(item => {
                                return item.id == menuItem.itemCategoryId ? (
                                    <option value={item.id}  >{item.name}</option>
                                ) : (
                                    <option value={item.id} selected >{item.name}</option>
                                )
                            })}
                        </select>
                    </div>
                    <div className="form-group">
                        <Link to="/menuItems" className="btn btn-primary" onClick={handleSubmit}>Create</Link>
                    </div>
                    <Link to="/menuItems">Back to main</Link>

                </div>
            </div>

        </>
    )
}

export default MenuItemCreate;