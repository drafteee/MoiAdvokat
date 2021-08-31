import { requests } from "../../../api/agent";
import { request } from "../../../helpers/dispatchFunctions";

export default {
    uploadFile: params => requests.post('/File/Post', params),
    deleteFiles: params => requests.post('/File/DeleteFiles', params)
}