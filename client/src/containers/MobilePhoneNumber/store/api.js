import { requests } from "../../../api/agent";

export default {
  getInternationalPhoneCodes: () =>
    requests.get("/phone/getInternationalPhoneCodes"),
  sendCode: (values) => requests.post("/phone/sendCodeToPhone", values),
  changeNumber: (values) => requests.post("/phone/changeMobilePhone", values),
  preChecking: (values) => requests.post("/phone/preChecking", values),
};
