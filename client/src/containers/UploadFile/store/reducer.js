import { uploadFileConstants } from "./constants";

const initialState =
{
    isSending: false,
    error: null,
    chosenFiles: [],
    uploadedFileIds: [],
    chosenFilesNames: []
}

export default function (state = initialState, action) {
    switch (action.type) {
        case uploadFileConstants.UploadFiles.REQUEST:
            return {
                ...state,
                isSending: true,
                uploadedFileIds: []
            }
        case uploadFileConstants.UploadFiles.SUCCESS:
            return {
                ...state,
                isSending: false,
                chosenFiles: [],
                chosenFilesNames: [],
                uploadedFileIds: action.payload
            }
        case uploadFileConstants.UploadFiles.FAILURE:
            return {
                ...state,
                isSending: false,
                error: action.error
            }
        case uploadFileConstants.UploadFiles.CLEAR:
            return {
                ...state,
                uploadedFileIds: []
            }
        case uploadFileConstants.CLEAR_FILES:
            return {
                ...state,
                chosenFiles: [],
                chosenFilesNames: [],
                uploadedFileIds: []
            }
        case uploadFileConstants.CHOOSE_FILE:
            return {
                ...state,
                chosenFiles: [
                    ...state.chosenFiles,
                    action.payload
                ],
                chosenFilesNames: [
                    ...state.chosenFilesNames,
                    action.payload.name
                ]
            }
        case uploadFileConstants.CHOOSE_FILES:
            return {
                ...state,
                chosenFiles: [
                    ...action.payload
                ],
                chosenFilesNames: [
                    ...action.payload.map(x => x.name)
                ]
            }
        case uploadFileConstants.REMOVE_FILE:
            const chosenFileIndex = state.chosenFiles.findIndex(x => x.uid === action.payload.uid)
            const newChosenFiles = [...state.chosenFiles]
            newChosenFiles.splice(chosenFileIndex, 1)

            const chosenFileNameIndex = state.chosenFilesNames.findIndex(x => x === action.payload.name)
            const newChosenFilesNames = [...state.chosenFilesNames]
            newChosenFilesNames.splice(chosenFileNameIndex, 1)

            return {
                ...state,
                chosenFiles: newChosenFiles,
                chosenFilesNames: newChosenFilesNames
            }

        case uploadFileConstants.DeleteFiles.REQUEST:
            return {
                ...state,
                isSending: true,
            }
        case uploadFileConstants.DeleteFiles.SUCCESS:
            return {
                ...state,
                isSending: false,
            }
        case uploadFileConstants.DeleteFiles.FAILURE:
            return {
                ...state,
                isSending: false,
                error: action.error
            }
        default:
            return state
    }
}