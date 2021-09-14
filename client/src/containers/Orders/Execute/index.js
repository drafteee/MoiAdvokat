import React, { useEffect } from 'react'
import { Button, Form } from 'antd'
import { useDispatch, useSelector } from 'react-redux'

import { orderActions } from '../store/actions'

import OrderCard from '../../../components/Orders/OrderCard'
import UploadFile from '../../UploadFile'

import notice from '../../../components/Notice'
import { uploadFileActions } from '../../UploadFile/store/actions'

import i18n from './localizations'
import i18nGlobal from '../../../localization'

import 'antd/lib/form/style/css'
import 'antd/lib/button/style/css'

const ExecuteOrder = props => {
    const id = props.match.params.id

    const { getOneOrderLoading, oneOrder, executeOrderResult } = useSelector(state => state.orderReducer)
    const { chosenFiles, uploadedFileIds } = useSelector(state => state.fileReducer)
    const { isRu } = useSelector(state => state.globalReducer)

    const dispatch = useDispatch()

    useEffect(() => {
        if (id) {
            dispatch(orderActions.getOrder({ id }))
        }
    }, [id])

    useEffect(() => {
        if (uploadedFileIds && uploadedFileIds.length > 0) {
            dispatch(orderActions.executeOrder({
                EntityId: id,
                FilesIds: uploadedFileIds
            }))
        }
    }, [uploadedFileIds])

    useEffect(() => {
        if (executeOrderResult) {
            dispatch(uploadFileActions.clearFiles())
            dispatch(orderActions.clearExecuteOrderResult())
        }
    }, [executeOrderResult])

    const onFinish = values => {
        if (chosenFiles && chosenFiles.length > 0) {
            dispatch(uploadFileActions.uploadFiles())
        } else {
            notice("error", i18n.haveToChooseFilesError[isRu], "", 5)
        }
    }

    return (
        <>
            <OrderCard
                order={oneOrder}
                orderLoading={getOneOrderLoading}
                isRu={isRu} />

            {oneOrder && !oneOrder.FinishDate ? (
                <Form
                    onFinish={onFinish}>
                    <Form.Item name="files">
                        <UploadFile />
                    </Form.Item>
                    <Form.Item>
                        <Button type="primary" htmlType="submit">
                            {i18nGlobal.send[isRu]}
                        </Button>
                    </Form.Item>
                </Form>
            ) : null}
        </>
    )
}

export default ExecuteOrder