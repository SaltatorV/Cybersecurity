import React, { useState } from 'react'
import CloseButton from 'react-bootstrap/CloseButton';
import { Link } from 'react-router-dom';
import Form from 'react-bootstrap/Form';
import { Button } from 'react-bootstrap';

function OpcjeHasel(props) {
    const [formData, setfromData] = useState([]);
    const [checkSwitch, setCheckSwitch] = useState(false);

    const handleChange = (e) => {
        setCheckSwitch(!checkSwitch)
        
    }

    const handleChangeText = (e) => {
        
        setfromData({
          ...formData,
          [e.target.name]: e.target.value,
    
        })
    
      }

      const handleSubmit = (e) => {
        e.preventDefault();
    
        const userLogin = {
            Login: props.login,
            PolitykaHasla: checkSwitch,
            Wygasniecie: formData.days,
        };
        console.log(userLogin)
        console.log(props.token)
    
        const header = new Headers();
        header.set('Authorization', `Bearer ${props.token}`);
        header.set('Content-Type', 'application/json');

        const url = 'http://localhost:5157/Account/OpcjeHasel';
    
        fetch(url, {
          method: "POST",
          headers: header,
          body: JSON.stringify(userLogin)
        })
          .then(res => res.json()
    
          )
          .then(resFromServer => {
    
            console.log(resFromServer)
            if(resFromServer === 200){alert("Pomyslnei zmieniono ustawienia")}
            else if(resFromServer === 400){alert("Bad password or login");}
            
          })
          .catch((error) => {
            console.log(error);
            alert(error);
          })
    
        }
    
    return (
        <div>
            <Link to={"/AllUsers"}>
                <CloseButton />;
            </Link>

            <>
                <Form.Check
                    aria-label="option 1"
                    onChange={handleChange}
                    value={checkSwitch}
                    label="Hasło musi zawierać co najmniej 8 znaków, co najmniej jedną wielką literę, co najmniej jeden znak specjalny"
                />
            </>
            <Form>
      <Form.Group className="mb-3" controlId="formBasicText">
        <Form.Label>Wpisz dni wygasniecia hasla</Form.Label>
        <Form.Control type="text" placeholder="days" onChange={handleChangeText} name={"days"}  />
      </Form.Group>
      </Form>
        <Button variant='primary' onClick={handleSubmit}>Zatwierdź</Button>



        </div >
    )
}

export default OpcjeHasel