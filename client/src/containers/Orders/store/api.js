import { requests } from "../../../api/agent";

export default {
    getOrders: () => requests.get('/Order/GetOrders'),
    getResponses: (params) => requests.getWithParams('/OrderResponses/GetResponses', params),
    submitOrder: params => requests.post('/Order/SubmitOrder', params),
    orderRespond: params => requests.post('/OrderResponses/RespondOrder', params),
    getStarterInfoForSubmit: () => requests.get('/Order/GetStarterInfoForSubmit')
};
