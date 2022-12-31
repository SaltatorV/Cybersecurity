import React, { useEffect, useState } from 'react'

function EditUserPanel(props) {



  const [user, setUser] = useState();
  const [formData, setfromData] = useState([]);
  const [show, setShow] = useState(0)
  const [select, setSelect] = useState("");


  const columns = [
    {
      Index: 1,
      Header: 'Login',
      Accessor: 'login',
    },
    {
      Index: 2,
      Header: 'IsPasswordExpire',
      Accessor: 'isPasswordExpire',
    },
    {
      Index: 3,
      Header: 'DayExpire',
      Accessor: 'dayExpire',
    },
    {
      Index: 5,
      Header: 'RoleId',
      Accessor: 'roleId',
    },
    {
      Index: 6,
      Header: 'SessionTime',
      Accessor: 'sessionTime',
    },
    {
      Index: 7,
      Header: 'MaxFailLogin',
      Accessor: 'maxFailLogin',
    },
  ];

  useEffect(() => {
    getUser();

    async function getUser() {
      await fetch('http://localhost:7277/api/account/user/' + props.id, {
        credentials: 'include',
      })
        .then(response => {
          if (response.status === 200) {
            return response.json();
          } else {
            console.log('Error');
          }
        })
        .then(res => {
          console.log(res);
          setUser(res);
        })
        .catch(error => console.log(error));
    }
  }, []);

  const handleChange = (e) => {



    if (e.target.value === "true") {
      setSelect(true);
    }
    else if (e.target.value === "false") {
      setSelect(false);
    }
    else {
      setSelect(e.target.value);
    }
  }
  const handleSubmit = async (e) => {
    e.preventDefault();

    //funkcja do edycji itp........

    console.log(e.target.name);

    var userPostEdit = {
      Id: user.id,
      Login: user.login,
      IsPasswordExpire: user.isPasswordExpire,
      DayExpire: user.DayExpire,
      RoleId: user.RoleId,
      SessionTime: user.SessionTime,
      MaxFailLogin: user.MaxFailLogin
    }


    for (const [key, value] of Object.entries(userPostEdit)) {
      //console.log(`${key}: ${value}`);
      //console.log(key);
      if (key === e.target.name) {
        console.log("key", key);
        console.log("value", value);
        console.log("set Value", select)
        console.log(userPostEdit[key]);

        userPostEdit[key] = select;

      }
    }
    console.log(userPostEdit)

    await fetch("http://localhost:7277/api/account/update/" + user.id, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      credentials: "include",
      body: JSON.stringify(userPostEdit),
    })
      .then((response) => {
        console.log(response.status);
        return response.json();
      })
      .then((res) => {
        if (res === 200)
          alert("Edycja użytkownika powiodła się!");
      })
      .catch((error) => console.log(error));
    setShow(0);
  };




  const edit = (name) => {
    return (<div>
      <form onSubmit={handleSubmit} name={name}>
        <label>
          {name}:
          <input type="text" login="login" onChange={handleChange} />
        </label>
        <input type="submit" value="Submit" />
      </form></div>)
  }
  const editSelect = (name) => {
    return (<div>
      <form onSubmit={handleSubmit} name={name}>
        <select onChange={handleChange} >
          <option value="">--Please choose an option--</option>
          <option value={true} >true</option>
          <option value={false}>false</option>
        </select>
        <input type="submit" value="Submit" />
      </form>
    </div>)
  }

  return (
    <div>EditUserPanel
      {(user !== undefined) && (
        <div>
          <div className="scroll-section">
            <table cellSpacing="0">
              <thead>
                <tr>
                  {columns.map(column => (
                    <th key={column.Index}>{column.Header}</th>
                  ))}
                </tr>
              </thead>
              <tbody>

                <tr key={user.id}>
                  <td>
                    {user.login}
                    {(show === 0) && (<button onClick={() => setShow(1)}>Edit</button>)}
                    {(show === 1) && (edit("Login"))}
                  </td>

                  <td>{user.isPasswordExpire.toString()}
                    {(show === 0) && (<button onClick={() => setShow(2)}>Edit</button>)}
                    {(show === 2) && (editSelect("IsPasswordExpire"))}

                  </td>
                  <td>{user.dayExpire}
                    {(show === 0) && (<button onClick={() => setShow(3)}>Edit</button>)}
                    {(show === 3) && (edit("DayExpire"))}
                  </td>
                  <td>{user.roleId}
                    {(show === 0) && (<button onClick={() => setShow(4)}>Edit</button>)}
                    {(show === 4) && (edit("RoleId"))}
                  </td>
                  <td>{user.SessionTime}
                    {(show === 0) && (<button onClick={() => setShow(5)}>Edit</button>)}
                    {(show === 5) && (edit("SessionTime"))}
                  </td>
                  <td>{user.maxFailLogin}
                    {(show === 0) && (<button onClick={() => setShow(6)}>Edit</button>)}
                    {(show === 6) && (edit("MaxFailLogin"))}
                  </td>
                </tr>
              </tbody>
            </table>
          </div>

          <button onClick={() => props.close('')}>Close</button>
        </div>
      )}
    </div>
  )
}

export default EditUserPanel