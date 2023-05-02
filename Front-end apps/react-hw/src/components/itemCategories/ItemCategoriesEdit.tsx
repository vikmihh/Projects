import React, { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import { ItemCategoryService } from '../../services/ItemCategoryService';
import { AppContext } from '../../state/AppContext';


const ItemCategoriesEdit = () => {

    let appState = useContext(AppContext);
    let { id } = useParams();
    const itemCategoryService = new ItemCategoryService(appState);
    const [jwt, setJwt] = useState(appState.jwt);
    const [category, setItemCategory] = useState(appState.category);

    useEffect(() => {
        itemCategoryService.getById(id!).then(data => setItemCategory(data));
    }, []);

    const handleSubmit = () => {
        itemCategoryService.put(id!, category).then(data => setItemCategory(data));
    }

    const handleDelete = () => {
        itemCategoryService.delete(id!);
    }

    return (
        <>
            <h3>Edit item category</h3>
            <div className="row">
                <div className="col-md-4">
                    <div className="text-danger validation-summary-valid" data-valmsg-summary="true">
                        <ul>
                            <li ></li>
                        </ul>
                    </div>
                    <div className="form-group">
                        <label className="control-label">Ticket Name</label>
                        <input type="text" value={category.name} onChange={(e) => {

                            category.name = e.target.value;
                            appState.setJwt(jwt);
                            setItemCategory(category);
                        }} className="form-control"
                        />
                        <span
                            className="text-danger field-validation-valid"
                            data-valmsg-for="TicketName"
                            data-valmsg-replace="true"
                        ></span>
                    </div>
                    <div className="form-group">
                        <Link to="/itemcategories" className="btn btn-primary" onClick={handleSubmit}>Save</Link>
                    </div>
                    <div className="form-group">
                        <Link to="/itemcategories" className="btn btn-danger" onClick={handleDelete}>Delete</Link>
                    </div>
                    <td />
                    <Link to="/itemcategories">Back to main</Link>
                </div>
            </div>
        </>
    )
}

export default ItemCategoriesEdit;