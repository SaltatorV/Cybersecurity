import React from 'react';
import Logger from '../components/logger/logger';
import User from '../components/user/user';

const AdminPanel = () => {
	return (
		<div className="admin-panel-content">
			<Logger /> <User />
		</div>
	);
};

export default AdminPanel;
