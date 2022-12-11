import React, { useState, useEffect } from 'react';

const Logger = () => {
	const [logger, setLogger] = useState([]);

	const columns = [
		{
			Index: 1,
			Header: 'LogTime',
			Accessor: 'logTime',
		},
		{
			Index: 2,
			Header: 'Action',
			Accessor: 'action',
		},
		{
			Index: 3,
			Header: 'Message',
			Accessor: 'message',
		},
		{
			Index: 4,
			Header: 'UserName',
			Accessor: 'userName',
		},
	];

	useEffect(() => {
		getLog();

		async function getLog() {
			await fetch('http://localhost:7277/api/log', {
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
					setLogger(res);
				})
				.catch(error => console.log(error));
		}
	}, []);

	return (
		<div className="logger-content">
			<div className="setting-tab">
				<div className="tab-header"></div>
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
							{logger.map(item => (
								<tr key={item.id}>
									<td>{item.logTime}</td>
									<td>{item.action}</td>
									<td>{item.message}</td>
									<td>{item.userName}</td>
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

export default Logger;
