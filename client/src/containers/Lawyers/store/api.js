import { requests } from "../../../api/agent";

export default {
    getAll: () => requests.get('/Lawyer/GetAll')
};
