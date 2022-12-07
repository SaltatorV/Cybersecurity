import React, { useState } from 'react'
import CloseButton from 'react-bootstrap/CloseButton';
import { Link } from 'react-router-dom';
import Form from 'react-bootstrap/Form';
import { Button } from 'react-bootstrap';

function OpcjeHasel(props) {
    const [formData, setfromData] = useState([]);
    const [checkSwitch, setCheckSwitch] = useState(false);
    const [zaIle,setZaIle]= useState(0);
    const [pobrano,setPobrano] = useState(false);

    function handleChange() {
        setCheckSwitch(!checkSwitch)
        console.log(!checkSwitch)
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
    
        function ZaIleWygas() {
          const url = 'http://localhost:5157/Account/ZaIleWygasnie/' + props.login;
          setPobrano(true);
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
              if(resFromServer !== undefined)
              {
              setZaIle(resFromServer)
              
              }
            })
            .catch((error) => {
              console.log(error);
              alert(error);
            })
            
        }
      
      
      
      
    return (
        
      <div >
          {(pobrano === false)&&(ZaIleWygas())}
            <Link to={"/AllUsers"}>
                <CloseButton className='ButtonClose'/>
            </Link>
           
            <div className='App'>

            <>
                <Form.Check
                    aria-label="option 1"
                    onChange={handleChange}
                    value={checkSwitch}
                    label="(Polityka hasła) Hasło musi zawierać co najmniej 8 znaków, co najmniej jedną wielką literę, co najmniej jeden znak specjalny"
                />
            </>
            <Form>
      <Form.Group className="mb-3" controlId="formBasicText">
     
     {(zaIle !== undefined)&&(
      <div>
        <p></p>
        <p><text>Haslo wygasnie za: {zaIle.days}</text></p>
        <p><text>Polityka hasła: {zaIle.politykaHasel}</text></p>
        </div>
        )}
        <Form.Label>Wpisz dni wygasniecia hasla</Form.Label>
        <Form.Control type="text" placeholder="Wpisz za ile dni wygasnie hasło" onChange={handleChangeText} name={"days"}  />
      </Form.Group>
      </Form>
        <Button variant='primary' onClick={handleSubmit}>Zatwierdź</Button>


        </div>
        </div >
    )
}

export default OpcjeHasel