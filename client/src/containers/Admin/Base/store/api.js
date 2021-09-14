import { requests } from '../../../../api/agent'

export default {
    getAll: params => requests.get(`/${params.controller}/GetAll`),
    createOrUpdate: params => requests.post(`/${params.controller}/CreateOrUpdate`, params),
    softDelete: params => requests.post(`/${params.controller}/Delete`, params),
    restore: params => requests.post(`/${params.controller}/Restore`, params),
}