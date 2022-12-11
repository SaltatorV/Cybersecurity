import React, { useState, useEffect } from 'react';
import * as MdIcons from 'react-icons/md';
import { Link } from 'react-router-dom';

const User = ({ showEditAdd, setFormFlag, setEditId }) => {
	const [users, setUsers] = useState([]);

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

	const showWindowHandler = (flag, settingFlag, id) => {
		showEditAdd(settingFlag);
		setEditId(id);
		setFormFlag(flag);
	};

	return (
		<div className="user-content">
			<div className="setting-tab">
				<div className="setting-header">
					User
					<Link to="#" onClick={() => showWindowHandler('add', 'user')}>
						<MdIcons.MdOutlineAddBox />
					</Link>
				</div>
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
											<MdIcons.MdOutlineEditNote />
										</Link>
									</td>
								</tr>
							))}
						</tbody>
					</table>
				</div>
				<div className="tab-footer"></div>
			</div>
		</div>
	);
};

export default User;
