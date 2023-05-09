import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { IJWTResponse } from '../domain/IJWTResponse';
import { AppContext } from '../state/AppContext';

let initialJWT: IJWTResponse = {
    token: "",
    refreshToken: "",
    firstName: "",
    lastName: "",
    email: "",
    role: ""
};

const Header = () => {

    const userRole = useContext(AppContext).jwt.role;
    const appState = useContext(AppContext);
    const handleLogout = () => {
        appState.setJwt(initialJWT);
    }

    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container container-fluid">
                    <Link to="/" className="navbar-brand">Home</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                <Link to="/tickets" className="nav-link text-dark">Buy pass</Link>
                            </li>
                            <li className="nav-item">
                                <Link to="/menuitems" className="nav-link text-dark">Food&Drink</Link>
                            </li>
                            {userRole == 'admin' &&
                                <li className="nav-item">
                                    <Link to="/itemcategories" className="nav-link text-dark">Categories</Link>
                                </li>
                            }

                        </ul>
                        {!userRole &&
                            <ul className="navbar-nav">
                                <li className="nav-item">
                                    <Link to="/register" className="nav-link text-dark">Register</Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/login" className="nav-link text-dark">Login</Link>
                                </li>
                            </ul>
                        }
                        {userRole &&
                            <>
                                <a className="nav-link text-dark"> Hello, {appState.jwt.firstName}! </a>
                                <Link to="/login" className="nav-link text-dark" onClick={handleLogout}>Logout</Link>
                            </>
                        }
                    </div>
                </div>
            </nav>
        </header>
    );

}

export default Header;