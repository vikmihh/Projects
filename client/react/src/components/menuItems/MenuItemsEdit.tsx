import React, { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import { IItemCategory } from '../../domain/IItemCategory';
import { ItemCategoryService } from '../../services/ItemCategoryService';
import { MenuItemService } from '../../services/MenuItemService';


import { AppContext } from '../../state/AppContext';


const initialCategory: IItemCategory[] = [];

const MenuItemsEdit = () => {
    const appState = useContext(AppContext);
    const itemCategoryService = new ItemCategoryService(appState);
    const menuItemService = new MenuItemService(appState);
    const [jwt, setJwt] = useState(appState.jwt);
    const [categories, setCategories] = useState(initialCategory);
    const [menuItem, setMenuItem] = useState(appState.menuItem);
    let { id } = useParams();


    useEffect(() => {
        menuItemService.getById(id!).then(data => setMenuItem(data));
        itemCategoryService.getAll().then(data => setCategories(data));
    }, []);

    const handleDelete = () => {
        menuItemService.delete(id!);
    }

    const handleSubmit = () => {
        menuItemService.put(id!, menuItem).then(data => setMenuItem(data));
    }

    return (
        <>
            <h3>Edit menuItem</h3>
            <div className="row">
                <div className="col-md-4">

                    <div className="form-group">
                        <label className="control-label">Item Name</label>
                        <input type="text" value={menuItem.itemName} onChange={(e) => {
                            menuItem.itemName = e.target.value;
                            appState.setJwt(jwt);
                            setMenuItem(menuItem);
                        }} className="form-control"
                        />

                    </div>
                    <div className="form-group">
                        <label className="control-label">Price</label>
                        <input type="number" value={menuItem.price} onChange={(e) => {
                            menuItem.price = e.target.value;
                            appState.setJwt(jwt);
                            setMenuItem(menuItem);
                        }} className="form-control"
                        />

                    </div>
                    <div className="form-group">
                        <label className="control-label">Description</label>
                        <input type="text" value={menuItem.description} onChange={(e) => {
                            menuItem.description = e.target.value;
                            appState.setJwt(jwt);
                            setMenuItem(menuItem);
                        }} className="form-control"
                        />

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
                                    <option value={item.id} selected>{item.name}</option>
                                ) : (
                                    <option value={item.id}  >{item.name}</option>
                                )
                            })}
                        </select>
                    </div>
                    <div className="form-group">
                        <Link to="/menuItems" className="btn btn-primary" onClick={handleSubmit}>Save</Link>
                    </div>
                    <div className="form-group">
                        <Link to="/menuItems" className="btn btn-danger" onClick={handleDelete}>Delete</Link>
                    </div>

                    <td />
                    <Link to="/menuItems">Back to main</Link>
                </div>
            </div>
        </>
    )
}

export default MenuItemsEdit;