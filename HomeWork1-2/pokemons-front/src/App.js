import logo from './logo.svg';
import './App.css';
import MainSection from './pages/MainPage/MainPage';
import Layout from './layout/Layout';

import {BrowserRouter, Routes, Route} from 'react-router-dom'



function App() {
  return (
    <BrowserRouter>
    <div className="App">
        <Layout/>
      </div>
    </BrowserRouter>
  );
}

export default App;
