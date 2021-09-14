import api from "./api";
import { uploadFileConstants } from "./constants";
import { failure, request, success } from "../../../helpers/dispatchFunctions";
import { defAction } from "../../../helpers/defaultAction";

export const uploadFileActions = {
    uploadFiles,
    chooseFile,
    chooseFiles,
    removeFile,
    clearFiles,
    deleteFiles
};

function uploadFiles() {
    return (dispatch, getState) => {
        dispatch(request(uploadFileConstants.UploadFiles.REQUEST))

        const fd = new FormData()

        getState().fileReducer.chosenFiles.forEach((file) => {
            fd.append("Files", file, file.name)
        })

        api.uploadFile(fd).then(res => {
            dispatch(success(uploadFileConstants.UploadFiles.SUCCESS, res))
        }).catch(err => {
            dispatch(failure(uploadFileConstants.UploadFiles.FAILURE, err))
        })
    }
}

function chooseFile(file) {
    return dispatch => {
        dispatch({
            type: uploadFileConstants.CHOOSE_FILE,
            payload: file
        })
    }
}

function chooseFiles(fileList) {
    return dispatch => {
        dispatch({
            type: uploadFileConstants.CHOOSE_FILES,
            payload: fileList
        })
    }
}

function removeFile(file) {
    return dispatch => {
        dispatch({
            type: uploadFileConstants.REMOVE_FILE,
            payload: file
        })
    }
}

function clearFiles() {
    return (dispatch) =>
        dispatch({
            type: uploadFileConstants.CLEAR_FILES
        })
}

function deleteFiles(params) {
    const dispatchObj = {
        constants: uploadFileConstants.DeleteFiles,
        service: {
            func: api.deleteFiles,
            params: params
        },
    }

    return defAction(dispatchObj)
}
