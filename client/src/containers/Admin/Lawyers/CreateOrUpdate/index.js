import React from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { DatePicker, Form, Input } from 'antd'

import CreateOrUpdateOneModal from '../../Base/CreateOrUpdateOne/modal'

import { baseActions } from '../../Base/store/actions'
import validationMessage from '../../../../helpers/validationMessage'

import i18n from './localization'

const CreateOrUpdate = ({ setUpdateTrigger }) => {
    const { isRu } = useSelector(state => state.globalReducer)

    const dispatch = useDispatch()

    const onFinish = values => {
        const {
            LastName,
            FirstName,
            MiddleName,
            LicenseNumber,
            DateOfIssue,
            Postcode,
            CountryId,
            AdministrativeTerritoryId,
            Street,
            House,
            Office,
            Notice
        } = values

        dispatch(baseActions.createOrUpdate({
            LastName,
            FirstName,
            MiddleName,
            LicenseNumber,
            DateOfIssue,
            Address: {
                Postcode,
                Country: {
                    Id: CountryId
                },
                AdministrativeTerritory: {
                    Id: AdministrativeTerritoryId
                },
                Street,
                House,
                Office,
                Notice
            },
            controller: "Lawyer"
        }))
    }

    const formChildren = () => {
        return (
            <>
                <Form.Item
                    name="LastName"
                    label={i18n.lastName[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.lastName, isRu)
                    }]}>
                    <Input />
                </Form.Item>
                <Form.Item
                    name="FirstName"
                    label={i18n.firstName[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.firstName, isRu)
                    }]}>
                    <Input />
                </Form.Item>
                <Form.Item
                    name="MiddleName"
                    label={i18n.middleName[isRu]}>
                    <Input />
                </Form.Item>
                <Form.Item
                    name="LicenseNumber"
                    label={i18n.licenseNumber[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.licenseNumber, isRu)
                    }]}>
                    <Input />
                </Form.Item>
                <Form.Item
                    name="DateOfIssue"
                    label={i18n.dateOfIssue[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.dateOfIssue, isRu)
                    }]}>
                    <DatePicker
                        style={{ width: "100%" }}
                        format="DD.MM.YYYY"
                        disabledDate={date => date > Date.now()}
                        placeholder="" />
                </Form.Item>
            </>
        )
    }

    return (
        <CreateOrUpdateOneModal
            withAddress
            onFinish={onFinish}
            setUpdateTrigger={setUpdateTrigger}>
            {formChildren()}
        </CreateOrUpdateOneModal>
    )
}

export default CreateOrUpdate