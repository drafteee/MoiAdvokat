import { requests } from "../../../../api/agent";

export default {
    getStatuses: () => requests.get('/OrderStatus/GetAllCurrent')
};
