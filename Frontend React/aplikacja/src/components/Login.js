import { Button } from 'react-bootstrap'
import React, { useState } from 'react'
import { Form } from 'react-bootstrap'
import {Link} from 'react-router-dom'
import ZmianaHasla from './ZmianaHasla';
import DropdownButton from 'react-bootstrap/DropdownButton';

function Login() {
  const initialData = Object.freeze({
    login: 'Admin',
    password: 'admin'
  });

  const [formData, setfromData] = useState(initialData)
  const [token, setToken] = useState('');


  const handleChange = (e) => {
    setfromData({
      ...formData,
      [e.target.name]: e.target.value,

    })

  }

  
    const handleSubmit = (e) => {
      e.preventDefault();
  
      const userLogin = {
        Login: formData.login,
        Password: formData.password
      };
    

      
    const url = 'http://localhost:5157/Account/';

    fetch(url, {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(userLogin)
    })
      .then(res => res.json()

      )
      .then(resFromServer => {
        
        var userStr = JSON.stringify(resFromServer);
        var a = userStr.length - 2;
        var tokenUser = userStr.slice(10, a);
        setToken(tokenUser);

        
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      })

    }
    console.log(formData.select)

    
  return (
    
    <div>
      {(token === '')&&(
<Form>
          <Form.Group className="mb-3" controlId="login">
            <Form.Label>Email address</Form.Label>
            <Form.Control value={formData.login} name="login" type="text" onChange={handleChange} />

          </Form.Group>

          <Form.Group className="mb-3" controlId="password">
            <Form.Label>Hasło</Form.Label>
            <Form.Control value={formData.password} name="password" type="password" onChange={handleChange} />
          </Form.Group>


          <Button variant="primary" type="submit" onClick={handleSubmit}>
            Zaloguj
          </Button>
</Form>
      )}
    {(token != '')&&(
      <div>

      

        <Form.Select aria-label="Default select example" value={formData.select} name="select" type="text" onChange={handleChange}>
      <option value="0">Menu</option>
      <option value="1">Zmiana hasła</option>
      <option value="2">Modyfikować szczegóły konta</option>
      <option value="3">Lista użytkowników</option>
      <option value="4">Lista użytkowników</option>
      <option value="5">Blokowanie konta użytkowników</option>
      <option value="6">Blokowanie ograniczenia wybranych haseł</option>
      <option value="7">Usuwanie konta użytkowników</option>
      <option value="8">Włączyć /wyłączyć ograniczenia haseł wybranych przez użytkownika</option>
      <option value="9">Ustawić ważność hasła użytkownika</option>
      
    </Form.Select>


    
        </div>

    )}
    </div>
  )
}

export default Login;