import React, { useState } from 'react';
import Logger from '../components/logger/logger';
import User from '../components/user/user';

const AdminPanel = () => {
	const [isEditAddOpen, setIsEditAddOpen] = useState(false);
	const [editId, setEditId] = useState();
	const [formFlag, setFormFlag] = useState('');
	const [settingFlagForm, setSettingFlagForm] = useState('');

	const showEditAdd = settingFlag => {
		setSettingFlagForm(settingFlag);
		setIsEditAddOpen(!isEditAddOpen);
	};

	const closeEditAdd = () => setIsEditAddOpen(!isEditAddOpen);

	return (
		<div className="admin-panel-content">
			<Logger /> <User />
		</div>
	);
};

export default AdminPanel;
