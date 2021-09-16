import React, { useState } from 'react'
import { useSelector } from 'react-redux'
import { Button, Modal } from 'antd'

import CreateOrUpdateOneForm from '.'

import i18nGlobal from '../../../../localization'

import 'antd/lib/button/style/css'
import 'antd/lib/modal/style/css'

const CreateOrUpdateOneModal = ({ form, withAddress, onFinish, children }) => {
    const { isRu } = useSelector(state => state.globalReducer)

    const [isModalVisible, setIsModalVisible] = useState(false)

    return (
        <>
            <Button
                type="primary"
                onClick={() => setIsModalVisible(true)}>
                {i18nGlobal.add[isRu]}
            </Button>

            <Modal
                title={i18nGlobal.edit[isRu]}
                visible={isModalVisible}
                footer={null}
                onCancel={() => setIsModalVisible(false)}>
                <CreateOrUpdateOneForm
                    withAddress={withAddress}
                    onFinish={onFinish}
                    setIsModalVisible={setIsModalVisible}
                    form={form}>
                    {children}
                </CreateOrUpdateOneForm>
            </Modal>
        </>
    )
}

export default CreateOrUpdateOneModal