import React from 'react';
import Log from '../components/log/log';
import User from '../components/user/user';

const AdminPanel = () => {
	return (
		<div className="admin-panel-content">
			<Log /> <User />
		</div>
	);
};

export default AdminPanel;
