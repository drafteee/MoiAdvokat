import React, { useState } from 'react'
import { useSelector } from 'react-redux'
import { Button, Modal } from 'antd'

import CreateOrUpdateOneForm from '.'

import i18nGlobal from '../../../../localization'

import 'antd/lib/button/style/css'
import 'antd/lib/modal/style/css'

const CreateOrUpdateOneModal = ({ withAddress, onFinish, setUpdateTrigger, children }) => {
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
                    setUpdateTrigger={setUpdateTrigger}>
                    {children}
                </CreateOrUpdateOneForm>
            </Modal>
        </>
    )
}

export default CreateOrUpdateOneModal