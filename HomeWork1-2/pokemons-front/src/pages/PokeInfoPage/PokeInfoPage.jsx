import React from 'react';
import { Link } from 'react-router-dom';
import StatBlock from './components/StatBlock/StatBlock';
import './PokeInfoPage.css'
import { useParams } from 'react-router-dom';

const PokeInfoPage = () => {

    let {name} = useParams()

    return (
        <div className="wrapper">
            <div className="header-info">
                <Link to="/">
                    <img src="https://thypix.com/wp-content/uploads/2020/04/white-arrow-95-700x395.png" alt="" />
                </Link>
            </div>
            <img className='info-pokeball' src="https://cdn-icons-png.flaticon.com/512/188/188918.png" alt="" />
            <div className="content-info">
                <StatBlock name = {name}/>
            </div>
        </div>
    );
};

export default PokeInfoPage;