import React from 'react';

import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'

import Login from './components/Login';
import NavBarApp from './NavBarApp';

import DaneKonta from './components/DaneKonta'
import AddUser from './components/AddUser';



class App extends React.Component{
  state = {
    token:''
  }

  onUserLogin(tokenUser) {

  this.setState({token:tokenUser})
  alert("Zalogowano")

  }


  render()
  {
    
      
      return(
        <>
          <Router>
          <NavBarApp />
          <Routes>
            <Route path="/Login" element={<Login onUserLogin={this.onUserLogin.bind(this)}></Login>}></Route>
            <Route path="/DaneKonta" element={<DaneKonta token={this.state.token}></DaneKonta>}></Route>
            <Route path="/AddUser" element={<AddUser></AddUser>}></Route>

          </Routes>

          
        </Router>
        </>
      )
    
  }
}

export default App;
