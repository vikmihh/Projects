import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { IdentityService } from '../services/IdentityService';
import { AppContext } from '../state/AppContext';

const Register = () => {
    let appState = useContext(AppContext);
    let navigate = useNavigate();
    let firstName = appState.user.firstName;
    let lastName = appState.user.lastName;
    let email = appState.user.email;
    let password = appState.user.password;

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        navigate("/");
        const identityService = new IdentityService().register(firstName, lastName, email, password).then(data => appState.setJwt(data.data!));
        event.preventDefault();
    }
    return (
        <>
            <h1>Register</h1>

            <div className="row">
                <div className="col-md-12">
                    <div v-if="errorMsg != null" className="text-danger validation-summary-errors" data-valmsg-summary="true">
                    </div>
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label className="control-label">firstName</label>
                            <input className="form-control" type="text" onChange={(e) => {
                                let user = appState.user;
                                user.firstName = e.target.value;
                                firstName = user.firstName;
                                appState.setUser(user);
                            }}
                            />
                        </div>
                        <div className="form-group">
                            <label className="control-label">lastName</label>
                            <input className="form-control" type="text" onChange={(e) => {
                                let user = appState.user;
                                user.lastName = e.target.value;
                                lastName = user.lastName;
                                appState.setUser(user);
                            }} />
                        </div>
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
                            }
                            />
                        </div>
                        <div className="form-group">
                            <input type="submit" value="Register" className="btn btn-primary" />
                        </div>
                    </form>
                </div>
            </div>
        </>
    )
}
export default Register;