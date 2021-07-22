import { requests } from "../../api/agent";

export default {
    getStatistics: () => requests.get('/Statistics')
};
