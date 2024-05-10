import React from 'react';
import MainPage from '../pages/MainPage/MainPage';
import PokeInfoPage from '../pages/PokeInfoPage/PokeInfoPage';
import {Route, Routes} from 'react-router-dom'

const Layout = () => {
    return (
        <div>
            <header>

            </header>
            <main>
            <div className='wrapper'>
                <Routes>
                    <Route path='/' element={<MainPage/>}/>
                    <Route path='/pokemon/:name' element={<PokeInfoPage/>}/>
                </Routes>
            </div>
            </main>
        </div>
    );
};

export default Layout;