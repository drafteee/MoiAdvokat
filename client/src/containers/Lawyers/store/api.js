import { requests } from "../../../api/agent";

export default {
    getAll: () => requests.get('/Lawyer/GetAll'),
    getAllCurrent: () => requests.get('/Lawyer/GetAllCurrent')
};
