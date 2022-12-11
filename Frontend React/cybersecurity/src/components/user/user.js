import React, { useState, useEffect } from 'react';
import * as MdIcons from 'react-icons/md';
import { Link } from 'react-router-dom';

const User = () => {
	const [users, setUsers] = useState([]);

	const columns = [
		{
			Index: 1,
			Header: 'Id',
			Accessor: 'id',
		},
		{
			Index: 2,
			Header: 'Login',
			Accessor: 'login',
		},
		{
			Index: 3,
			Header: 'Rola',
			Accessor: 'roleName',
		},
		{
			Index: 4,
			Header: 'IsPasswordExpire',
			Accessor: 'isPasswordExpire',
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
				<div className="tab-header"></div>
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
									{columns.map(key => {
										const value = item[key.Accessor];
										return <td key={key.Index}>{value}</td>;
									})}
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
				<div className="tab-footer">Pagination</div>
			</div>
		</div>
	);
};

export default User;
