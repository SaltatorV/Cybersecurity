import React from 'react';

import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'

import Login from './components/Login';
import NavBarApp from './NavBarApp';

import DaneKonta from './components/DaneKonta'
import AddUser from './components/AddUser';
import AllUsers from './components/AllUsers';
import OpcjeHasel from './components/OpcjeHasel';



class App extends React.Component {
  state = {
    token: '',
    selectLogin: ''
  }

  onUserLogin(tokenUser) {

    if (tokenUser !== null) {
      this.setState({ token: tokenUser })
      alert("Zalogowano")
    }

    else if (tokenUser === null) {
      this.setState({ token: '' })
      alert("Wylogowano")
    }

  }
  selectUser(login) {
    console.log(login)
    this.setState({ selectLogin: login })
  }



  render() {


    return (
      <>
        <Router>
          <NavBarApp token={this.state.token} onUserLogin={this.onUserLogin.bind(this)} />
          <Routes>
            <Route path="/Login" element={<Login onUserLogin={this.onUserLogin.bind(this)} token={this.state.token}></Login>}></Route>
            <Route path="/DaneKonta" element={<DaneKonta token={this.state.token}></DaneKonta>}></Route>
            <Route path="/AddUser" element={<AddUser></AddUser>}></Route>
            <Route path="/AllUsers" element={<AllUsers token={this.state.token} selectUser={this.selectUser.bind(this)}></AllUsers>}></Route>
            <Route path="/OpcjeHasel" element={<OpcjeHasel token={this.state.token} login={this.state.selectLogin}></OpcjeHasel>}></Route>

          </Routes>


        </Router>
      </>
    )

  }
}

export default App;
