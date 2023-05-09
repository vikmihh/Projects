import { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { IItemCategory } from '../domain/IItemCategory';
import { ItemCategoryService } from '../services/ItemCategoryService';
import { AppContext } from '../state/AppContext';


const initialCategory: IItemCategory[] = [];

const ItemCategories = () => {
    const itemCategoryService = new ItemCategoryService(useContext(AppContext));
    const [categories, setCategories] = useState(initialCategory);
    let appState = useContext(AppContext);
    useEffect(() => {
        itemCategoryService.getAll().then(data => setCategories(data));
    }, []);

    return (
        <>
            <h3>Menu items categories</h3>
            <Link to={`/itemcategories/create`}>Create new category</Link>
            <div>
                <div className="container">
                    <table className="table">
                        <thead>
                            <tr>
                                <th>
                                    Name
                                </th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {categories.map(item => {
                                return (
                                    <>
                                        <tr key={item.id}>
                                            <td>
                                                {item.name}
                                            </td>

                                            <td>
                                                <Link to={`/itemcategories/edit/${item.id}`}>Edit</Link>
                                            </td>
                                        </tr>
                                    </>);
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
        </>
    )
}

export default ItemCategories;