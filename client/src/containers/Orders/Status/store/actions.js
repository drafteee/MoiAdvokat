import { orderStatusesConstants } from "./constants";
import { defAction } from '../../../../helpers/defaultAction'
import api from "./api";


export const orderStatusesActions = {
  getStatuses
};

function getStatuses() {
  const dispatchObj = {
    constants: orderStatusesConstants.GetStatuses,
    service: {
      func: api.getStatuses
    }
  };

  return defAction(dispatchObj);
}
