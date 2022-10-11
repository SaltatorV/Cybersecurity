import React from 'react';

import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'

import Login from './components/Login';
import NavBarApp from './NavBarApp';
import ZmianaHasla from './components/ZmianaHasla';
class App extends React.Component{
  state = {
    elements: [
   
    ]
  }



  render()
  {
    
      
      return(
        <>
          <Router>
          <NavBarApp />
          <Routes>
            <Route path="/Login" element={<Login></Login>}></Route>
            <Route path="/ZmianaHasla" element={<ZmianaHasla></ZmianaHasla>}></Route>
          </Routes>

          
        </Router>
        </>
      )
    
  }
}

export default App;
