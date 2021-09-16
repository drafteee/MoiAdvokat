import React, { useState } from 'react'
import { useSelector } from 'react-redux'
import { CheckOutlined, CloseOutlined } from '@ant-design/icons'

import CreateOrUpdate from '../CreateOrUpdate'
import ListAllBase from '../../Base/ListAll'

import i18n from './localization'

const ListAllOrders = props => {
    const { isRu } = useSelector(state => state.globalReducer)

    const columns = [
        {
            title: i18n.isResponse[isRu],
            dataIndex: 'IsResponse',
            key: 'IsResponse',
            render: value => value ? <CheckOutlined /> : <CloseOutlined />
        },
        {
            title: i18n.description[isRu],
            dataIndex: 'Description',
            key: 'Description'
        },
        {
            title: i18n.nameClient[isRu],
            dataIndex: 'NameClient',
            key: 'NameClient'
        },
        {
            title: i18n.phoneNumber[isRu],
            dataIndex: 'PhoneNumber',
            key: 'PhoneNumber'
        },
        {
            title: i18n.endDueDate[isRu],
            dataIndex: 'EndDueDate',
            key: 'EndDueDate',
            render: value => new Date(value).toLocaleDateString()
        },
        {
            title: i18n.lawyer[isRu],
            dataIndex: 'Lawyer',
            key: 'Lawyer',
            render: value => {
                return value
                    ? `${value.LastName} ${value.FirstName} ${value.MiddleName ?? ""}`
                    : <></>
            }
        },
        {
            title: i18n.price[isRu],
            dataIndex: 'Price',
            key: 'Price'
        },
        {
            title: i18n.startDate[isRu],
            dataIndex: 'StartDate',
            key: 'StartDate',
            render: value => new Date(value).toLocaleDateString()
        },
        {
            title: i18n.finishDate[isRu],
            dataIndex: 'FinishDate',
            key: 'FinishDate',
            render: value => new Date(value).toLocaleDateString()
        },
        {
            title: i18n.status[isRu],
            dataIndex: 'Status',
            key: 'Status',
            render: value => isRu ? value.NameRus : value.NameKaz
        },
        {
            title: i18n.user[isRu],
            dataIndex: 'User',
            key: 'User',
            render: (_, record) => record.UserName
        },
        {
            title: i18n.specializations[isRu],
            dataIndex: 'SpecializationsStr',
            key: 'SpecializationsStr'
        },
    ]

    return (
        <>
            <CreateOrUpdate />
            <ListAllBase controller="Order" columns={columns} />
        </>
    )
}

export default ListAllOrders