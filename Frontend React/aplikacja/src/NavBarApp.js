import React from 'react'
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link } from "react-router-dom"

function NavBarApp() {
  return (
    
    <Navbar bg="dark" variant="dark">
    <Container>
      <Navbar.Brand href="#home">Navbar</Navbar.Brand>
      <Nav className="me-auto">
        <Nav.Link as={Link} to={"/Login"} href="Login">Login</Nav.Link>
        <Nav.Link as={Link} to={"/DaneKonta"} href="DaneKonta">Dane Konta</Nav.Link>
        <Nav.Link as={Link} to={"/AddUser"} href="AddUser">Add Users</Nav.Link>
        <Nav.Link as={Link} to={"/AllUsers"} href="AllUsers">All Users</Nav.Link>
        </Nav>
        </Container>
      </Navbar>
      
  )

}
export default NavBarApp;

