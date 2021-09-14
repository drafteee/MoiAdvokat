import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'

import { Form, Input, Select } from 'antd'

import { addressActions } from './store/actions'
import validationMessage from '../../helpers/validationMessage'

import i18n from './localization'

const AddressInputForm = ({ form }) => {
    const { countries, administrativeTerritories } = useSelector(state => state.addressReducer)
    const { isRu } = useSelector(state => state.globalReducer)
    
    const [countryId, setCountryId] = useState(null)

    const dispatch = useDispatch()

    useEffect(() => {
        dispatch(addressActions.getCountries())
    }, [])

    useEffect(() => {
        if (countryId) {
            dispatch(addressActions.getAdministrativeTerritories({
                countryId
            }))
        }
    }, [countryId])

    const handleCountryChange = value => {
        form.setFieldsValue({
            CountryId: value
        })
        setCountryId(value)
    }

    const handleAdministrativeTerritoryChange = value => {
        form.setFieldsValue({
            AdministrativeTerritoryId: value
        })
    }

    return (
        <>
            <Form.Item
                name="Postcode"
                label={i18n.postcode[isRu]}
                rules={[{
                    required: true,
                    message: validationMessage(i18n.postcode, isRu)
                }]}>
                <Input />
            </Form.Item>
            <Form.Item
                name="CountryId"
                label={i18n.country[isRu]}
                rules={[{
                    required: true,
                    message: validationMessage(i18n.country, isRu)
                }]}>
                <Select onChange={handleCountryChange}>
                    {countries.map(country => (
                        <Select.Option value={country.Id}>
                            {country.NameRus}
                        </Select.Option>
                    ))}
                </Select>
            </Form.Item>
            <Form.Item
                name="AdministrativeTerritoryId"
                label={i18n.administrativeTerritory[isRu]}
                rules={[{
                    required: true,
                    message: validationMessage(i18n.administrativeTerritory, isRu)
                }]}>
                <Select onChange={handleAdministrativeTerritoryChange}>
                    {administrativeTerritories.map(at => (
                        <Select.Option value={at.Id}>
                            {at.Name}
                        </Select.Option>
                    ))}
                </Select>
            </Form.Item>
            <Form.Item
                name="Street"
                label={i18n.street[isRu]}
                rules={[{
                    required: true,
                    message: validationMessage(i18n.street, isRu)
                }]}>
                <Input />
            </Form.Item>
            <Form.Item
                name="House"
                label={i18n.house[isRu]}
                rules={[{
                    required: true,
                    message: validationMessage(i18n.house, isRu)
                }]}>
                <Input />
            </Form.Item>
            <Form.Item
                name="Office"
                label={i18n.office[isRu]}
                rules={[{
                    required: true,
                    message: validationMessage(i18n.office, isRu)
                }]}>
                <Input />
            </Form.Item>
            <Form.Item
                name="Notice"
                label={i18n.notice[isRu]}
                rules={[{
                    required: true,
                    message: validationMessage(i18n.notice, isRu)
                }]}>
                <Input />
            </Form.Item>
        </>
    )
}

export default AddressInputForm