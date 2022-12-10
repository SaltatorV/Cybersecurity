import React from 'react';
import * as MdIcons from 'react-icons/md';
import './settingTable.css';
import { Link } from 'react-router-dom';

function SettingTable({ columns, data, tableClassName, showWindowHandler, searchData }) {
	return (
		<div className="setting-tab">
			<div className="tab-header">
				Search <input type="search" onChange={searchData}></input>
			</div>
			<div className="scroll-section">
				<table className={tableClassName} cellSpacing="0">
					<thead>
						<tr>
							{columns.map(column => (
								<th className={column.className} key={column.index}>
									{column.columnsName}
								</th>
							))}
							<th className="td-edit">Edit</th>
						</tr>
					</thead>
					<tbody>
						{data.map(item => (
							<tr key={item.id}>
								{columns.map(key => {
									const value = item[key.accesor];
									return (
										<td className={key.className} key={key.index}>
											{value}
										</td>
									);
								})}
								<td className="td-edit">
									<Link to="#">
										<MdIcons.MdOutlineEditNote onClick={() => showWindowHandler('edit', item.id)} />
									</Link>
								</td>
							</tr>
						))}
					</tbody>
				</table>
			</div>
			<div className="tab-footer">Pagination</div>
		</div>
	);
}

export default SettingTable;
