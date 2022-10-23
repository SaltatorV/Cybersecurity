import React, { useState } from 'react'
import { Button, Table } from 'react-bootstrap'

function AllUsers(props) {

    const [usersList, setUsersList] = useState('');

    function getUsers() {
        const header = new Headers();
        header.set('Authorization', `Bearer ${props.token}`);
        header.set('Content-Type', 'application/json');
        console.log(props.token);

        const url = 'http://localhost:5157/Account/GetUsers';

        fetch(url, {
            method: 'GET',
            headers: header
        })
            .then(res => res.json()

            )
            .then(resFromServer => {

                console.log(resFromServer)
                if (resFromServer !== 400) { setUsersList(resFromServer); }
                else if (resFromServer === 400) { alert("Bląd pobrania uzytkoników!") }




            })
            .catch((error) => {
                console.log(error);
                alert(error);
            })



    }

    function Delete(login) {

        const header = new Headers();
        header.set('Authorization', `Bearer ${props.token}`);
        header.set('Content-Type', 'application/json');



        const url = 'http://localhost:5157/Account/deleteUser/' + login;

        fetch(url, {
            method: 'DELETE',
            headers: header
        })
            .then(res => res.json()

            )
            .then(resFromServer => {

                console.log(resFromServer)
                if (resFromServer === 200) {
                    alert("Usunięto użytkownika");
                }
                else if (resFromServer === 400) { alert("Bląd pobrania uzytkoników!") }

            })
            .catch((error) => {
                console.log(error);
                alert(error);
            })



    }

    function Zablokuj(login) {

        const header = new Headers();
        header.set('Authorization', `Bearer ${props.token}`);
        header.set('Content-Type', 'application/json');



        const url = 'http://localhost:5157/Account/zablokuj/' + login;

        fetch(url, {
            method: 'PUT',
            headers: header
        })
            .then(res => res.json()

            )
            .then(resFromServer => {

                console.log(resFromServer)
                if (resFromServer === 200) {
                    alert("Zablokowano użytkownika");
                }
                else if (resFromServer === 400) { alert("Bląd pobrania uzytkoników!") }

            })
            .catch((error) => {
                console.log(error);
                alert(error);
            })



    }
   
    return (

        <div>

            {(usersList === '') && (getUsers())}
            {(usersList != '') && (
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Login</th>
                            <th>Hasło</th>
                            <th>Rola</th>
                            <th>Status</th>
                            <th>Akcja</th>
                        </tr>
                    </thead>
                    {(usersList.map((user, id) =>
                        <tbody>
                            <tr>
                                <td>{id}</td>
                                <td>{user.login}</td>
                                <td>*******</td>
                                <td>{user.role}</td>
                                <td>{user.isActive.toString()}</td>
                                <td><Button variant="warning" onClick={() => Zablokuj(user.login)}>Zablokuj</Button>
                                <Button variant="danger" onClick={() => Delete(user.login)}>Usuń</Button></td>
                            </tr>
                        </tbody>
                    ))}
                </Table>

            )}
        </div>
    )
}

export default AllUsers