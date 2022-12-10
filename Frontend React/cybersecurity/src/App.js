import React, { useState, useEffect } from 'react';
import Cookies from 'js-cookie';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

import PageNavbar from './components/navbar/pagenavbar';
import Login from './components/login/login';

import ChangePassword from './page/changePassword';
import AdminPanel from './page/adminPanel';
import Account from './page/account';
import Home from './page/home';

function App() {
	const [userRole, setUserRole] = useState();
	const [login, setLogin] = useState();
	const loginCookie = document.cookie.indexOf('login=');

	let menu = '';

	useEffect(() => {
		if (loginCookie === 0) {
			getRole();
			setLogin(Cookies.get('login'));
		}

		async function getRole() {
			await fetch('http://localhost:7277/api/account/role', {
				credentials: 'include',
			})
				.then(response => {
					if (response.status === 200) {
						return response.json();
					} else {
						console.log('error');
					}
				})
				.then(res => {
					setUserRole(res);
				})
				.catch(error => console.log(error));
		}
	}, [loginCookie]);

	if (loginCookie === -1) {
		menu = (
			<div className="content">
				<Router>
					<div className="navbar-container">
						<PageNavbar />
					</div>
					<div className="content-container">
						<Routes>
							<Route path="/" element={<Home />} />
							<Route path="/Login" element={<Login setUserRole={setUserRole} />} />
						</Routes>
					</div>
				</Router>
			</div>
		);
	} else if (userRole === 2) {
		menu = (
			<div className="content">
				<Router>
					<div className="navbar-container">
						<PageNavbar />
					</div>
					<div className="content-container">
						<Routes>
							<Route path="/" element={<Home />} />
							<Route path="/Account" element={<Account />} />
						</Routes>
					</div>
				</Router>
			</div>
		);
	} else if (loginCookie === -4) {
		menu = (
			<div className="content">
				<Router>
					<div className="navbar-container">
						<PageNavbar />
					</div>
					<div className="content-container">
						<Routes>
							<Route path="/" element={<Home />} />
							<Route path="/changepassword" element={<ChangePassword />} />
						</Routes>
					</div>
				</Router>
			</div>
		);
	} else if (userRole === 1) {
		menu = (
			<div className="content">
				<Router>
					<div className="navbar-container">
						<PageNavbar login={login} userRole={userRole} />
					</div>
					<div className="content-container">
						<Routes>
							<Route path="/" element={<Home />} />
							<Route path="/account" element={<Account />} />
							<Route path="/adminpanel" element={<AdminPanel />} />
						</Routes>
					</div>
				</Router>
			</div>
		);
	}

	return <div className="App">{menu}</div>;
}

export default App;
