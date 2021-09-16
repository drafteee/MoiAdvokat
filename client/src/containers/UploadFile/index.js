import React, { useCallback, useEffect, useState } from 'react'
import { Button, Popconfirm, Tooltip, Upload } from 'antd'
import { useDispatch, useSelector } from 'react-redux'

import { uploadFileActions } from './store/actions'

import validateFile from '../../helpers/validateFile'
import notice from '../../components/Notice'
import i18n from './localization'
import i18nGlobal from '../../localization'

import 'antd/lib/upload/style/css'
import 'antd/lib/button/style/css'
import 'antd/lib/tooltip/style/css'
import 'antd/lib/popconfirm/style/css'

import './style.css'

const UploadFile = ({ multiple = true }) => {
    const [canDelete, setCanDelete] = useState(false)
    const [file, setFileToRemove] = useState({})

    const { chosenFilesNames, chosenFiles } = useSelector(state => state.fileReducer)
    const { isRu } = useSelector(state => state.globalReducer)

    const dispatch = useDispatch()

    const fileNotChosen = file =>
        chosenFilesNames.length === 0 || chosenFilesNames.findIndex(x => x === file.name) === -1

    const deleteFiles = useCallback((params) => {
        dispatch(uploadFileActions.deleteFiles(params))
    }, [dispatch])

    useEffect(() => {
        if (canDelete && file) {
            if (file.url)
                deleteFiles({ FileIds: [file.uid] })

            dispatch(uploadFileActions.removeFile(file))
            setCanDelete(false)
        }
    }, [canDelete, file])

    const removeFile = file => {
        setFileToRemove(file)
        return false
    }

    const onChange = ({ file, fileList }) => {
        if (fileList.length < chosenFiles.length) {
            dispatch(uploadFileActions.chooseFiles(fileList))
            return
        }
        if (fileNotChosen(file))
            dispatch(uploadFileActions.chooseFile(file))
        else
            notice("error", i18n.chooseFileError[isRu], i18n.fileAlreadyChosen[isRu], 5)
    }

    return (
        <>
            <Upload
                multiple={multiple}
                beforeUpload={file =>
                    validateFile(file, isRu)}
                className="upload-user-custom"
                fileList={chosenFiles}
                listType="picture"
                showUploadList={{
                    showRemoveIcon: true,
                    removeIcon: (
                        <Popconfirm
                            cancelText={i18nGlobal.no[isRu]}
                            okText={i18nGlobal.yes[isRu]}
                            title={i18n.deleteFileConfirmationMessage[isRu]}
                            onConfirm={() => setCanDelete(true)}>
                            <Button
                                danger
                                className="custom-delete-uploaded-file-button"
                                type="ghost">
                                {i18nGlobal.remove[isRu]}
                            </Button>
                        </Popconfirm>
                    )
                }}
                onChange={onChange}
                onRemove={removeFile}>
                <Tooltip title={i18n.downloadFileRulesInfo[isRu]}>
                    <Button>
                        {i18n.chooseFile[isRu]}
                    </Button>
                </Tooltip>
            </Upload>
        </>
    )
}

export default UploadFile