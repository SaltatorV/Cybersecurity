import React from "react";
import { NavLink, Link } from "react-router-dom";
import { Navbar, Nav, NavItem } from "reactstrap";

function PageNavbar({ login, userRole }) {
  const loginCookie = document.cookie.indexOf("login=");

  let menu;

  const handleLogout = async (event) => {
    event.preventDefault();

    await fetch("http://localhost:7277/api/account/logout", {
      method: "POST",
      credentials: "include",
    })
      .then((response) => {
        if (response.status === 200) {
          window.location.href = "/";
        } else {
          console.log("Error");
        }
      })
      .catch((error) => console.log(error));
  };

  const menuItemLogin = [
    {
      index: 1,
      path: "/login",
      name: "Login",
    },
  ];

  const menuItemUser = [
    {
      index: 1,
      path: "/account",
      name: login,
    },
  ];

  const menuItemChangePassword = [
    {
      index: 1,
      path: "/changepassword",
      name: "Change Password",
    },
  ];

  const menuItemAdmin = [
    {
      index: 1,
      path: "/account",
      name: login,
    },
    {
      index: 2,
      path: "/adminpanel",
      name: "Admin Panel",
    },
    {
      index: 3,
      path: "/changepassword",
      name: "Change-password",
    },
  ];

  if (loginCookie === -1) {
    menu = menuItemLogin;
  } else if (loginCookie === 44) {
    menu = menuItemUser;
  } else if (loginCookie === 33) {
    menu = menuItemChangePassword;
  } else if (userRole === 1) {
    menu = menuItemAdmin;
  }

  return (
    <div>
      <Navbar className="navbar navbar-expand navbar-light navbar-dark bg-dark fixed-top">
        <Link className="navbar-brand" to="/">
          Cybersecurity
        </Link>
        <Nav className="me-auto navbar-nav" navbar>
          {menu.map((item) => (
            <NavItem key={item.index}>
              <NavLink className="nav-link" to={item.path}>
                {item.name}
              </NavLink>
            </NavItem>
          ))}
        </Nav>
        <button className="btn btn-secondary" onClick={handleLogout}>
          Logout
        </button>
      </Navbar>
    </div>
  );
}

export default PageNavbar;
