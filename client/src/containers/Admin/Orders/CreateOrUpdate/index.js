import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Checkbox, DatePicker, Form, Input, InputNumber, Select } from 'antd'

import CreateOrUpdateOneModal from '../../Base/CreateOrUpdate/modal'

import { baseActions } from '../../Base/store/actions'
import { lawyerActions } from '../../../Lawyers/store/actions'
import { orderStatusesActions } from '../../../Orders/Status/store/actions'
import validationMessage from '../../../../helpers/validationMessage'

import i18n from './localization'

import 'antd/lib/input-number/style/css'
import { specializationsActions } from '../../../Lawyers/Specialization/store/actions'

const CreateOrUpdate = props => {
    const { lawyers } = useSelector(state => state.lawyerReducer)
    const { orderStatuses } = useSelector(state => state.orderStatusesReducer)
    const { specializations } = useSelector(state => state.specializationsReducer)
    const { isRu } = useSelector(state => state.globalReducer)

    const [form] = Form.useForm()

    const dispatch = useDispatch()

    const onFinish = values => {
        const {
            IsResponse,
            Header,
            Description,
            NameClient,
            PhoneNumber,
            EndDueDate,
            LawyerId,
            Price,
            StartDate,
            FinishDate,
            StatusId,
            SpecializationsIds
        } = values

        dispatch(baseActions.createOrUpdate({
            IsResponse,
            Header,
            Description,
            NameClient,
            PhoneNumber,
            EndDueDate,
            LawyerId,
            Price,
            StartDate,
            FinishDate,
            StatusId,
            SpecializationsIds,
            controller: "Order"
        }))
    }

    useEffect(() => {
        dispatch(lawyerActions.getAllCurrent())
        dispatch(orderStatusesActions.getStatuses())
        dispatch(specializationsActions.getSpecializations())
    }, [])

    const handleLawyerChange = value => {
        form.setFieldsValue({
            LawyerId: value
        })
    }

    const handleStatusChange = value => {
        form.setFieldsValue({
            StatusId: value
        })
    }

    const handleUserChange = value => {

    }

    const handleSpecializationsChange = value => {
        form.setFieldsValue({
            SpecializationsIds: value
        })
    }

    const formChildren = () => {
        return (
            <>
                <Form.Item
                    name="IsResponse"
                    label={i18n.isResponse[isRu]}>
                    <Checkbox />
                </Form.Item>
                <Form.Item
                    name="Header"
                    label={i18n.header[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.header, isRu)
                    }]}>
                    <Input />
                </Form.Item>
                <Form.Item
                    name="Description"
                    label={i18n.description[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.description, isRu)
                    }]}>
                    <Input />
                </Form.Item>
                <Form.Item
                    name="NameClient"
                    label={i18n.nameClient[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.nameClient, isRu)
                    }]}>
                    <Input />
                </Form.Item>
                <Form.Item
                    name="PhoneNumber"
                    label={i18n.phoneNumber[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.phoneNumber, isRu)
                    }]}>
                    <Input />
                </Form.Item>
                <Form.Item
                    name="EndDueDate"
                    label={i18n.endDueDate[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.endDueDate, isRu)
                    }]}>
                    <DatePicker
                        style={{ width: "100%" }}
                        format="DD.MM.YYYY"
                        disabledDate={date => date < Date.now()}
                        placeholder="" />
                </Form.Item>
                <Form.Item
                    name="LawyerId"
                    label={i18n.lawyer[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.lawyer, isRu)
                    }]}>
                    <Select onChange={handleLawyerChange}>
                        {lawyers.map(lawyer => (
                            <Select.Option value={lawyer.Id}>
                                {`${lawyer.LastName} ${lawyer.FirstName} ${lawyer.MiddleName ?? ""}`}
                            </Select.Option>
                        ))}
                    </Select>
                </Form.Item>
                <Form.Item
                    name="Price"
                    label={i18n.price[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.price, isRu)
                    }]}>
                    <InputNumber />
                </Form.Item>
                <Form.Item
                    name="StartDate"
                    label={i18n.startDate[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.startDate, isRu)
                    }]}>
                    <DatePicker
                        style={{ width: "100%" }}
                        format="DD.MM.YYYY"
                        disabledDate={date => date > Date.now()}
                        placeholder="" />
                </Form.Item>
                <Form.Item
                    name="FinishDate"
                    label={i18n.finishDate[isRu]}>
                    <DatePicker
                        style={{ width: "100%" }}
                        format="DD.MM.YYYY"
                        disabledDate={date => date < Date.now()}
                        placeholder="" />
                </Form.Item>
                <Form.Item
                    name="StatusId"
                    label={i18n.status[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.status, isRu)
                    }]}>
                    <Select onChange={handleStatusChange}>
                        {orderStatuses.map(os => (
                            <Select.Option value={os.Id}>
                                {isRu ? os.NameRus : os.NameKaz}
                            </Select.Option>
                        ))}
                    </Select>
                </Form.Item>
                {/* <Form.Item
                    name="UserId"
                    label={i18n.user[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.user, isRu)
                    }]}>
                    <Select onChange={handleUserChange}>
                        {}
                    </Select>
                </Form.Item> */}
                <Form.Item
                    name="SpecializationsIds"
                    label={i18n.specializations[isRu]}
                    rules={[{
                        required: true,
                        message: validationMessage(i18n.specializations, isRu)
                    }]}>
                    <Select mode="multiple" onChange={handleSpecializationsChange}>
                        {specializations.map(spec => (
                            <Select.Option value={spec.Id}>
                                {isRu ? spec.Name : spec.NameKaz}
                            </Select.Option>
                        ))}
                    </Select>
                </Form.Item>
            </>
        )
    }

    return (
        <CreateOrUpdateOneModal
            onFinish={onFinish}
            form={form}>
            {formChildren()}
        </CreateOrUpdateOneModal>
    )
}

export default CreateOrUpdate