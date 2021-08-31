import { requests } from "../../../api/agent";

export default {
    getOrders: () => requests.get('/Order/GetOrders'),
    submitOrder: params => requests.post('/Order/SubmitOrder', params),
    getStarterInfoForSubmit: () => requests.get('/Order/GetStarterInfoForSubmit'),
    getOrder: params => requests.getWithParams('/Order/GetOne', params),
    executeOrder: params => requests.post('/Order/Execute', params)
};
