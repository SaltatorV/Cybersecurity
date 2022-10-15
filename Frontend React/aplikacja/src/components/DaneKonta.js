import React, { useState } from 'react'
import { Button, Col, Form, Row } from 'react-bootstrap'

function DaneKonta() {
  
    const [formData,setfromData]=useState([])

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
        console.log(userLogin)
    
    
        const url = 'http://localhost:5157/Account/ChangePassword';
    
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
    
            console.log(resFromServer)
           
    
            
          })
          .catch((error) => {
            console.log(error);
            alert(error);
          })
    
        }
    
    
  
  
    return (
    <div>

<Form className='DaneKontaForm'>
        
        <Form.Group as={Row} className="mb-3" controlId="formPlaintextLogin">
          <Form.Label column sm="3">
            Login:
          </Form.Label>
          <Col >
            <Form.Control plaintext readOnly defaultValue={props.account.login} />
          </Col>
        </Form.Group>

        <Form.Label htmlFor="inputPassword" >Stare haslo</Form.Label>
        <Form.Control
          type="password"
          id="password"
          onChange={handleChange}
          value={formData.password}
          name="password"
        />

        <Form.Label htmlFor="inputPasswordNew" >Nowe haslo</Form.Label>
        <Form.Control
          type="password"
          id="passwordNew"
          onChange={handleChange}
          value={formData.passwordNew}
          name="passwordNew"
        />

        <Form.Text id="passwordHelpBlock" muted >
          Zmiana hasla
        </Form.Text>

        
        <p><Button variant="primary" onClick={handleSubmit}>Zmień</Button></p>
        
      </Form>

    </div>
  )
}

export default DaneKonta