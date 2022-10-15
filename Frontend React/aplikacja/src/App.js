import React from 'react';

import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'

import Login from './components/Login';
import NavBarApp from './NavBarApp';
import DaneKonta from './components/DaneKonta'

class App extends React.Component{
  state = {
    elements: [
   
    ]
  }

  onUserToken(tokenUser) {


    console.log("User token drag.");

    if (tokenUser.length > 3) {

      this.setState({ token: tokenUser })

    }
    else {

      alert("Blad token");
    }

  }


  render()
  {
    
      
      return(
        <>
          <Router>
          <NavBarApp />
          <Routes>
            <Route path="/Login" element={<Login onUserLogin={this.onUserLogin.bind(this)} onUserToken={this.onUserToken.bind(this)}></Login>}></Route>
            <Route path="/DaneKonta" element={<DaneKonta ></DaneKonta>}></Route>
          </Routes>

          
        </Router>
        </>
      )
    
  }
}

export default App;
