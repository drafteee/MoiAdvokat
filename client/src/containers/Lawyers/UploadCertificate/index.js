import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Alert, Button, Form } from 'antd'

import UploadFile from '../../UploadFile'

import notice from '../../../components/Notice'
import { uploadFileActions } from '../../UploadFile/store/actions'
import { lawyerActions } from '../store/actions'

import i18n from './localizations'
import i18nGlobal from '../../../localization'

import 'antd/lib/alert/style/css'

const UploadCertificate = props => {
    const id = props.match.params.id

    const { chosenFiles, uploadedFileIds } = useSelector(state => state.fileReducer)
    const { isRu } = useSelector(state => state.globalReducer)
    const { uploadCertificateSuccess, certificateCheckResult, error } = useSelector(state => state.lawyerReducer)

    const dispatch = useDispatch()

    useEffect(() => {
        if (id) {
            dispatch(lawyerActions.checkIfCertificateExists({
                id
            }))
        }
    }, [id])

    useEffect(() => {
        if (uploadedFileIds && uploadedFileIds.length > 0) {
            dispatch(lawyerActions.uploadCertificate({
                EntityId: id,
                FilesIds: uploadedFileIds
            }))
        }
    }, [uploadedFileIds])

    useEffect(() => {
        if (uploadCertificateSuccess === true) {
            notice("success", i18n.uploadCertificateSuccess[isRu], "", 5)
            dispatch(uploadFileActions.clearFiles())
        } else if (uploadCertificateSuccess === false) {
            notice("error", i18n.uploadCertificateError[isRu], error, 5)
        }
    }, [uploadCertificateSuccess])

    const onFinish = values => {
        if (chosenFiles && chosenFiles.length > 0) {
            dispatch(uploadFileActions.uploadFiles())
        } else {
            notice("error", i18n.haveToChooseFilesError[isRu], "", 5)
        }
    }

    return (
        !certificateCheckResult ? (
            <Form
                onFinish={onFinish}>
                <Form.Item name="files">
                    <UploadFile multiple={false} />
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit">
                        {i18nGlobal.send[isRu]}
                    </Button>
                </Form.Item>
            </Form>
        ) : (
            <Alert type="error" message={i18n.sertificateExistsError[isRu]} showIcon />
        )
    )
}

export default UploadCertificate