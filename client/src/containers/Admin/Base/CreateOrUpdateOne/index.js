import React, { useEffect } from 'react'
import { useSelector } from 'react-redux'
import { Button, Form } from 'antd'

import AddressInputForm from '../../../AddressInputForm'

import notice from '../../../../components/Notice'
import i18n from './localization'
import i18nGlobal from '../../../../localization'

import 'antd/lib/form/style/css'
import 'antd/lib/input/style/css'


const CreateOrUpdateOneForm = ({ withAddress, onFinish, setIsModalVisible, setUpdateTrigger, children }) => {
    const { isRu } = useSelector(state => state.globalReducer)
    const { createOrUpdateSuccess } = useSelector(state => state.baseReducer)

    const [form] = Form.useForm()

    useEffect(() => {
        if (createOrUpdateSuccess === true) {
            form.resetFields()
            notice("success", i18n.createOrUpdateSuccess[isRu])
            setUpdateTrigger()
            if (setIsModalVisible) setIsModalVisible()
        }
    }, [createOrUpdateSuccess])

    return (
        <>
            <Form
                form={form}
                onFinish={onFinish}
                layout="vertical">
                {children}
                {withAddress ? (
                    <AddressInputForm form={form} />
                ) : null}
                <Form.Item>
                    <Button
                        type="primary"
                        htmlType="submit">
                        {i18nGlobal.save[isRu]}
                    </Button>
                </Form.Item>
            </Form>
        </>
    )
}

export default CreateOrUpdateOneForm