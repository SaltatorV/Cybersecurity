import React, { useState, useEffect } from 'react';
import * as MdIcons from 'react-icons/md';
import { Link } from 'react-router-dom';
import EditUserPanel from './EditUserPanel';
import AddUserPanel from './AddUserPanel';
const User = ({ showEditAdd, setFormFlag, setEditId }) => {
	const [users, setUsers] = useState([]);
	const [selectEditUser,setSelectEditUser] = useState('');
	const [addUserOpenPanel,setAddUserOpenPanel] = useState(false);

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
			await fetch('http://localhost:7277/api/account/user', {
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
					setUsers(res);
				})
				.catch(error => console.log(error));
		}
	}, []);

	

	return (
		<div className="user-content">
			
			<div className="setting-tab">
				<div className="setting-header">
					User
					<Link to="#" onClick={() => setAddUserOpenPanel(!addUserOpenPanel)}>
						<MdIcons.MdOutlineAddBox />
					</Link>
				</div>
				{(addUserOpenPanel===true)&&(<AddUserPanel  close={setAddUserOpenPanel.bind(this)}/>)}
				<div className="scroll-section">
					<table cellSpacing="0">
						<thead>
							<tr>
								{columns.map(column => (
									<th key={column.Index}>{column.Header}</th>
								))}
								<th className="td-edit">Edit</th>
							</tr>
						</thead>
						<tbody>
							{users.map(item => (
								<tr key={item.id}>
									<td>{item.login}</td>
									<td>{item.roleName}</td>
									<td>{item.isPasswordExpire ? 'tak' : 'nie'}</td>
									<td>{item.passwordExpire}</td>
									<td>{item.maxFailLogin}</td>
									<td>{item.isLoginLockOn ? 'tak' : 'nie'}</td>
									<td>{item.loginLockOnTime}</td>
									<td>{item.sessionTime}</td>
									<td>{item.isOneTimePasswordSet ? 'tak' : 'nie'}</td>
									<td className="td-edit">
										<Link to="#">
											<MdIcons.MdOutlineEditNote onClick={()=>setSelectEditUser(item.id)}/>
										</Link>
									</td>
								</tr>
							))}
						</tbody>
					</table>
				</div>
				<div className="tab-footer"></div>
			</div>
			{(selectEditUser!=='')&&(<EditUserPanel id={selectEditUser} close={setSelectEditUser.bind(this)}/>)}
			
			
		</div>
	);
};

export default User;
