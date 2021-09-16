import React, { useState } from 'react'
import { useSelector } from 'react-redux'

import CreateOrUpdate from '../CreateOrUpdate'
import ListAllBase from '../../Base/ListAll'

import i18n from './localization'

const ListAllLawyers = props => {
    const { isRu } = useSelector(state => state.globalReducer)

    const columns = [
        {
            title: i18n.fullName[isRu],
            dataIndex: 'fullName',
            key: 'fullName',
            render: (_, record) => `${record.LastName} ${record.FirstName} ${record.MiddleName ?? ""}`
        },
        {
            title: i18n.licenseNumber[isRu],
            dataIndex: 'LicenseNumber',
            key: 'LicenseNumber'
        },
        {
            title: i18n.dateOfIssue[isRu],
            dataIndex: 'DateOfIssue',
            key: 'DateOfIssue',
            render: item => new Date(item).toLocaleDateString()
        }
    ]

    return (
        <>
            <CreateOrUpdate />
            <ListAllBase controller="Lawyer" columns={columns} />
        </>
    )
}

export default ListAllLawyers