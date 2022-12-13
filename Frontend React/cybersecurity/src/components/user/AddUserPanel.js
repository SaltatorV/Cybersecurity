import React, { useState } from 'react'

function AddUserPanel() {

    const [formData, setfromData] = useState([])

    const columns = [
        {
            Index: 1,
            Header: 'Login',
            Accessor: 'login',
        },
        {
            Index: 2,
            Header: 'Password',
            Accessor: 'password',
        },
        {
            Index: 3,
            Header: 'ConfirmPassword',
            Accessor: 'confirmPassword',
        },
        {
            Index: 4,
            Header: 'RoleId',
            Accessor: 'roleId',
        },

    ];


    const handleChange = (e) => {
        setfromData({
            ...formData,
            [e.target.name]: e.target.value,

        })
        console.log(e.target.name)
        console.log(e.target.value)

    }
    const handleSubmit = async (e) => {
        e.preventDefault();

        const userRegisterPost = {
            Login: formData.login,
            Password: formData.password,
            ConfirmPassword: formData.confirmPassword,
            RoleId: formData.roleId
        }
        console.log(userRegisterPost);

        await fetch("http://localhost:7277/api/account/register/", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
            },
            credentials: "include",
            body: JSON.stringify(userRegisterPost),
        })
            .then((response) => {
                console.log(response.status);
                return response.json();
            })
            .then((res) => {
                if (res === 200)
                    alert("Dodawnie użytkownika powiodła się!");
            })
            .catch((error) => console.log(error));

    }
    return (
        <div>AddUserPanel

            <div>
                <div className="scroll-section">
                    <form onSubmit={handleSubmit} name="login">
                        <label>
                            Login:
                            <input value={formData.login} type="text" name="login" onChange={handleChange} />
                        </label>
                        <label>
                            Password:
                            <input value={formData.password} name="password" type="password" onChange={handleChange} />
                        </label>
                        <label>
                            ConfirmPassword:
                            <input value={formData.confirmPassword} type="password" name="confirmPassword" onChange={handleChange} />
                        </label>
                        <label>
                            RoleId:
                            <input value={formData.roleid} type="text" name="roleId" onChange={handleChange} />
                        </label>
                        <input type="submit" value="Submit" />
                    </form>
                </div>
            </div>
        </div>
    )
}

export default AddUserPanel