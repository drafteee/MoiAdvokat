import React from 'react'
import { Upload } from "antd"
import notice from "../components/Notice"
import i18nGlobal from '../localization'

const validFormats = [
    'application/pdf',
    'application/vnd.openxmlformats-officedocument.wordprocessingml.document', //docx
    'application/msword', //doc
]

function validateFile(file) {
    const isValidFormat = validFormats.indexOf(file.type) != -1
    const isOkSize = file.size / 1024 / 1024 < 100
    const errorMessage = `${!isValidFormat ? i18nGlobal.fileTypeError[isRu] : ``}\n${!isOkSize ? i18nGlobal.downloadFileSizeError[isRu] : ``}`.trim()
    if (errorMessage) {
        const description = (<span style={{ whiteSpace: "pre-wrap" }}>{errorMessage}</span>)
        notice("error", i18nGlobal.attachFilesTitle[isRu], description)
    }
    return isOkSize && isValidFormat ? false : Upload.LIST_IGNORE
}

export default validateFile