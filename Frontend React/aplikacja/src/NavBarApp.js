import React, { useState } from 'react'
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Link } from "react-router-dom"

function NavBarApp(props) {
  
  const [user,setUser] = useState('');
  const [pobrano,setPobrano] = useState(false);
 function tokenVerify()
  {
    setPobrano(true);
      const url = 'http://localhost:5157/Account/Verify/' + props.token;
      
      fetch(url, {
        method: "GET",
        headers: {
          'Content-Type': 'application/json'
        },
  
      })
        .then(res => res.json()
  
        )
        .then(resFromServer => {
  
          console.log(resFromServer)
          if (resFromServer !== undefined) { 
            setUser(resFromServer);
             }
          
  
        })
        .catch((error) => {
          console.log(error);
          alert(error);
        })
  
    }
  
  function logout()
  {
    props.onUserLogin(null);
    setUser('');
    setPobrano(false);
  }
  
  return (
    
    <Navbar bg="dark" variant="dark">
    <Container>
      <Navbar.Brand href="#home">Navbar</Navbar.Brand>
      <Nav className="me-auto">
        {(user.login === undefined)&&(<Nav.Link as={Link} to={"/Login"} href="Login">Login</Nav.Link>)}
        {(user.login !== undefined)&&(<Nav.Link as={Link} to={"/Login"} href="Logout"onClick={()=>logout()}>Logout</Nav.Link>)}
        {(pobrano === false)&&(props.token !== '')&&(tokenVerify())}
        
          
        {(user.role === 'Admin')&&(<Nav.Link as={Link} to={"/DaneKonta"} href="DaneKonta">Dane Konta</Nav.Link>)}
        {(user.role === 'User') &&(<Nav.Link as={Link} to={"/DaneKonta"} href="DaneKonta">Dane Konta</Nav.Link>)}
        {(user.role === 'Admin')&&(<Nav.Link as={Link} to={"/AddUser"} href="AddUser">Add Users</Nav.Link>)}
        {(user.role === 'Admin')&&(<Nav.Link as={Link} to={"/AllUsers"} href="AllUsers">All Users</Nav.Link>)}
        
        
        </Nav>
        </Container>
      </Navbar>
      
  )

}
export default NavBarApp;

