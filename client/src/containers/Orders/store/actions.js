import { orderConstants } from "./constants";
import { defAction } from '../../../helpers/defaultAction'
import api from "./api";


export const orderActions = {
  getOrders
};


function getOrders() {
  const dispatchObj = {
    constants: orderConstants.GetOrders,
    service: {
      func: api.getOrders
    }
  };

  return defAction(dispatchObj);
}
