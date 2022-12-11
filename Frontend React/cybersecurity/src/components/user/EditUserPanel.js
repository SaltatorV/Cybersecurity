import React, { useEffect, useState } from 'react'

function EditUserPanel(props) {



  const [user, setUser] = useState();
  const [formData, setfromData] = useState([]);
  const [show, setShow] = useState(0)


  console.log(props.id)
  const columns = [
    {
      Index: 1,
      Header: 'Login',
      Accessor: 'login',
    },
    {
      Index: 2,
      Header: 'Rola',
      Accessor: 'roleName',
    },
    {
      Index: 3,
      Header: 'IsPasswordExpire',
      Accessor: 'isPasswordExpire',
    },
    {
      Index: 4,
      Header: 'PasswordExpire',
      Accessor: 'passwordExpire',
    },
    {
      Index: 5,
      Header: 'MaxFailLogin',
      Accessor: 'maxFailLogin',
    },
    {
      Index: 6,
      Header: 'IsLoginLockOn',
      Accessor: 'isLoginLockOn',
    },
    {
      Index: 7,
      Header: 'LoginLockOnTime',
      Accessor: 'loginLockOnTime',
    },
    {
      Index: 8,
      Header: 'SessionTime',
      Accessor: 'sessionTime',
    },
    {
      Index: 9,
      Header: 'IsOneTimePasswordSet',
      Accessor: 'isOneTimePasswordSet',
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
    setfromData({
      ...formData,
      [e.target.name]: e.target.value,

    })
    console.log(e.target.value);

  }
  const handleSubmit = (e) => {
    e.preventDefault();

    //funkcja do edycji itp........


    setShow(0);
  }
  const edit = (name) => {
    return (<div>
      <form onSubmit={handleSubmit}>
        <label>
          {name}:
          <input type="text" login="login" onChange={handleChange} />
        </label>
        <input type="submit" value="Submit" />
      </form></div>)
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

                  <td>{user.roleName}
                    {(show === 0) && (<button onClick={() => setShow(2)}>Edit</button>)}
                    {(show === 2) && (edit("roleName"))}

                  </td>
                  <td>{user.isPasswordExpire ? 'tak' : 'nie'}
                    {(show === 0) && (<button onClick={() => setShow(3)}>Edit</button>)}
                    {(show === 3) && (edit("isPasswordExpire"))}
                  </td>
                  <td>{user.passwordExpire}
                    {(show === 0) && (<button onClick={() => setShow(4)}>Edit</button>)}
                    {(show === 4) && (edit("passwordExpire"))}
                  </td>
                  <td>{user.maxFailLogin}
                    {(show === 0) && (<button onClick={() => setShow(5)}>Edit</button>)}
                    {(show === 5) && (edit("maxFailLogin"))}
                  </td>
                  <td>{user.isLoginLockOn ? 'tak' : 'nie'}
                    {(show === 0) && (<button onClick={() => setShow(6)}>Edit</button>)}
                    {(show === 6) && (edit("isLoginLockOn"))}
                  </td>
                  <td>{user.loginLockOnTime}
                    {(show === 0) && (<button onClick={() => setShow(7)}>Edit</button>)}
                    {(show === 7) && (edit("loginLockOnTime"))}
                  </td>
                  <td>{user.sessionTime}
                    {(show === 0) && (<button onClick={() => setShow(8)}>Edit</button>)}
                    {(show === 8) && (edit("sessionTime"))}
                  </td>
                  <td>{user.isOneTimePasswordSet ? 'tak' : 'nie'}
                    {(show === 0) && (<button onClick={() => setShow(9)}>Edit</button>)}
                    {(show === 9) && (edit("isOneTimePasswordSet"))}
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