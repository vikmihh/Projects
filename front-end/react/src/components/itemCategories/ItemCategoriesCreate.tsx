import React, { useContext, useState } from 'react';
import { Link } from 'react-router-dom';
import { IItemCategory } from '../../domain/IItemCategory';
import { ItemCategoryService } from '../../services/ItemCategoryService';
import { AppContext } from '../../state/AppContext';

let initialCategory: IItemCategory = {
    id: "",
    name: ""
};

const ItemCategoriesCreate = () => {
    let appState = useContext(AppContext);
    const itemCategoryService = new ItemCategoryService(appState);
    const [category, setItemCategory] = useState(initialCategory);
    const [jwt, setJwt] = useState(appState.jwt);

    const handleSubmit = () => {
        itemCategoryService.create(category.name);
    }

    return (
        <>
            <h3>Create new menu item category</h3>
            <div className="row">
                <div className="col-md-12">
                    <div className="form-group">
                        <label className="control-label">Category Name</label>
                        <input type="text" className="form-control" value={category.name} onChange={(e) => {

                            category.name = e.target.value;
                            appState.setJwt(jwt);
                            setItemCategory(category);
                        }}
                        />
                    </div>
                    <div className="form-group">
                        <Link to="/itemcategories" className="btn btn-primary" onClick={handleSubmit}>Create</Link>
                    </div>
                    <Link to="/itemcategories">Back to main</Link>

                </div>
            </div>

        </>
    )
}

export default ItemCategoriesCreate;