import { requests } from "../../../api/agent";

export default {
    getOrders: () => requests.get('/Order/GetOrders')
};
