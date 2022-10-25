import { Button } from 'react-bootstrap'
import React, { useState } from 'react'
import { Form } from 'react-bootstrap'

import { Link } from 'react-router-dom'

import './style.css'

function Login(props) {

  const [formData, setfromData] = useState([])
  const [zalogowano, setZalogowano] = useState('');

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

        console.log(resFromServer)
        if (resFromServer.token !== undefined) {
          props.onUserLogin(resFromServer.token);
          setZalogowano(resFromServer.token);

        }
        else if (resFromServer === 400) { alert("Bad password or login"); }

      })
      .catch((error) => {
        console.log(error);
        alert(error);
      })

  }


  return (

    <div className='App'>

      {(props.token === '') && (
        <Form>
          <Form.Group className="mb-3" controlId="formBasicEmail">
            <Form.Label>Email address</Form.Label>
            <Form.Control type="text" placeholder="login" value={formData.login} name={"login"} onChange={handleChange} />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formBasicPassword">
            <Form.Label>Password</Form.Label>
            <Form.Control type="password" placeholder="Password" value={formData.password} name={"password"} onChange={handleChange} />
          </Form.Group>


          <Button variant="primary" type="submit" onClick={handleSubmit}>
            Submit
          </Button>

        </Form>


      )}

      {(props.token !== '') && (
        <h1>Zalogowano</h1>
        )}


    </div>
  )
}

export default Login;