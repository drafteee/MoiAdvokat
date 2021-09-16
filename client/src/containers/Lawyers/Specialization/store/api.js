import { requests } from "../../../../api/agent";

export default {
    getSpecializations: () => requests.get('/Specialization/GetAllCurrent')
};
