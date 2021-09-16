import { specializationsConstants } from "./constants";
import { defAction } from '../../../../helpers/defaultAction'
import api from "./api";

export const specializationsActions = {
  getSpecializations
};

function getSpecializations() {
  const dispatchObj = {
    constants: specializationsConstants.GetSpecializations,
    service: {
      func: api.getSpecializations
    }
  };

  return defAction(dispatchObj);
}
