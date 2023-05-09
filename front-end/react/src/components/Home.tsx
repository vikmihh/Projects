import { render } from '@testing-library/react';
import React from 'react';
import '../App.css';


const Home = () =>{
    return(
        <>
    <div className=" container h-100 row align-items-center ">
        <table >
            <tr style={{ height:"179px"}} >
                <td className="text">We offer a place to study, work and eat our snacks!</td>
                <td className="o"></td>
            </tr>
            <tr style={{ height:"179px"}}>
                <td className="o" width="256px"></td>
                <td className="text">Buy a pass, come and order some sandwiches!</td>
            </tr>
        </table>
    </div>
    </>
    )};

export default Home;