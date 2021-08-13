import { mobileConstants } from "./constants";
import { defAction } from "../../../helpers/defaultAction";
import api from "./api";

export const mobileActions = {
  getInternationalPhoneCodes,
  sendCode,
  // checkCode,
  changeNumber,
  clearFields,
};

function getInternationalPhoneCodes() {
  const dispatchObj = {
    constants: mobileConstants.GetCodes,
    service: {
      func: api.getInternationalPhoneCodes,
    },
  };

  return defAction(dispatchObj);
}

function sendCode(params) {
  const dispatchObj = {
    constants: mobileConstants.SendCode,
    service: {
      func: api.sendCode,
      params: params,
    },
  };

  return defAction(dispatchObj);
}

// function checkCode(params) {
//   const dispatchObj = {
//     constants: mobileConstants.CheckCode,
//     service: {
//       func: api.checkCode,
//       params: params,
//     },
//   };

//   return defAction(dispatchObj);
// }

function changeNumber(params) {
  const dispatchObj = {
    constants: mobileConstants.ChangeNumber,
    service: {
      func: api.changeNumber,
      params: params,
    },
  };

  return defAction(dispatchObj);
}

function clearFields() {
  const dispatchObj = {
    constants: mobileConstants.ClearFields,
  };

  return defAction(dispatchObj);
}
