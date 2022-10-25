import React, { useState } from 'react'
import { Form, Row, Col, Button } from 'react-bootstrap'

function AddUser() {

    const [formData, setfromData] = useState([])
    const [httpRequest, setHttpRequest] = useState()

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
            NewPassword: formData.newPassword,
            ConfirmPassword: formData.confirmPassword,
            Role: formData.role
        };
        console.log(userLogin)


        const url = 'http://localhost:5157/Account/CreateUser';

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
                if (resFromServer === 200) { alert("Uzytkownik stworzony") }
                else if (resFromServer === 400) { alert("Blednie wprowadzone dane") }

            })
            .catch((error) => {
                console.log(error);
                alert(error);
            })

    }


    return (
        <div className='App'>
            <div>
                <Form className='DaneKontaForm'>


                    <Form.Label htmlFor="inputLogin" > Login</Form.Label>
                    <Form.Control
                        type="text"
                        id="login"
                        onChange={handleChange}
                        value={formData.login}
                        name="login"
                    />

                    <Form.Label htmlFor="inputPassword" >Haslo</Form.Label>
                    <Form.Control
                        type="password"
                        id="newPassword"
                        onChange={handleChange}
                        value={formData.newPassword}
                        name="newPassword"
                    />

                    <Form.Label htmlFor="inputConfirmPassword" >Potwierdz haslo</Form.Label>
                    <Form.Control
                        type="password"
                        id="confirmPassword"
                        onChange={handleChange}
                        value={formData.confirmPassword}
                        name="confirmPassword"
                    />
                    <Form.Text id="passwordHelpBlock" muted >
                        Wybierz role
                        </Form.Text>

                    <Form.Select aria-label="Default select example" onChange={handleChange} name="role">
                        <option>Open this select menu</option>
                        <option value="Admin">Admin</option>
                        <option value="User">User</option>
                    </Form.Select>


                    <p></p>
                    <Button variant="primary" onClick={handleSubmit}>Stwórz</Button>

                </Form>
            </div>


        </div>
    )
}

export default AddUser