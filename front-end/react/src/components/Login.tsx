import React, { useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { IdentityService } from '../services/IdentityService';
import { AppContext } from '../state/AppContext';

const Login = () => {
    const appState = useContext(AppContext);
    let email = appState.user.email;
    let password = appState.user.password;
    let navigate = useNavigate();

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        navigate("/");
        event.preventDefault();
        const identityService = new IdentityService().login(email!, password!).then(data => appState.setJwt(data.data!));
    }
    return (
        <>
            <h1>Login</h1>

            <div className="row">
                <div className="col-md-12">

                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label className="control-label">email</label>
                            <input className="form-control" type="text" onChange={(e) => {
                                let user = appState.user;
                                user.email = e.target.value;
                                email = user.email;
                                appState.setUser(user);
                            }}
                            />
                        </div>
                        <div className="form-group">
                            <label className="control-label">password</label>
                            <input className="form-control" type="password" onChange={(e) => {
                                let user = appState.user;
                                user.password = e.target.value;
                                password = user.password;
                                appState.setUser(user);
                            }
                            } />
                        </div>
                        <div className="form-group">
                            <input type="submit" className="btn btn-primary" />
                        </div>
                    </form>
                </div>
            </div>
        </>
    )
}
export default Login;
