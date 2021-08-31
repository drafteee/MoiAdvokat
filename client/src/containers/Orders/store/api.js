import { requests } from "../../../api/agent";

export default {
    getOrders: () => requests.get('/Order/GetOrders'),
    getResponses: (params) => requests.getWithParams('/OrderResponses/GetResponses', params),
    submitOrder: params => requests.post('/Order/SubmitOrder', params),
    orderRespond: params => requests.post('/OrderResponses/RespondOrder', params),
    chooseLawyer: params => requests.post('/OrderResponses/ChooseLawyer', params),
    getStarterInfoForSubmit: () => requests.get('/Order/GetStarterInfoForSubmit'),
    getOrder: params => requests.getWithParams('/Order/GetOne', params)
};
