import React, { useState } from 'react'
import { Button, Col, Form, Row } from 'react-bootstrap'

function DaneKonta(props) {
  
  const [formData, setfromData] = useState([])
  const [user, setUser] = useState('');
  const [httpRequest,setHttpRequest] = useState()

  const handleChange = (e) => {
    setfromData({
      ...formData,
      [e.target.name]: e.target.value,

    })

  }

  const handleSubmit = (e) => {
    e.preventDefault();

    const userLogin = {
      Login: user.login,
      NewPassword: formData.newPassword,
      OldPassword: formData.oldPassword
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
        setHttpRequest(resFromServer);
        if(resFromServer === 200){alert("Haslo zmienione")}
        else if(resFromServer === 400){alert("Bledne stare haslo")}

      })
      .catch((error) => {
        console.log(error);
        alert(error);
      })

  }

  function tokenAuth() {

    const userLogin = {
      Token: props.token,
    };

    const url = 'http://localhost:5157/Account/tokenAuth';

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
        if(resFromServer !== 400){ setUser(resFromServer);}
        else if(resFromServer === 400){alert("Bledny token")}

     


      })
      .catch((error) => {
        console.log(error);
        alert(error);
      })



  }


  return (
    <div>
      
      {(user === '') && (tokenAuth())}
      {(user !== '') && (
        <div>
          <Form className='DaneKontaForm'>

            <Form.Group as={Row} className="mb-3" controlId="formPlaintextLogin">
              <Form.Label column sm="3">
                Login:
              </Form.Label>
              <Col >
                <Form.Control plaintext readOnly defaultValue={user.login} />
              </Col>
            </Form.Group>



            <Form.Label htmlFor="inputPassword" >Stare haslo</Form.Label>
            <Form.Control
              type="password"
              id="oldPassword"
              onChange={handleChange}
              value={formData.oldPassword}
              name="oldPassword"
            />

            <Form.Label htmlFor="inputPasswordNew" >Nowe haslo</Form.Label>
            <Form.Control
              type="password"
              id="newPassword"
              onChange={handleChange}
              value={formData.newPassword}
              name="newPassword"
            />

            <Form.Text id="passwordHelpBlock" muted >
              Zmiana hasla
            </Form.Text>


            <p><Button variant="primary" onClick={handleSubmit}>Zmień</Button></p>

          </Form>
        </div>
      )}

    </div>
  )
}

export default DaneKonta