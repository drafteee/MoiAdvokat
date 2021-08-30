import { orderConstants } from "./constants";
import { defAction } from '../../../helpers/defaultAction'
import api from "./api";


export const orderActions = {
  getOrders,
  submitOrder,
  getStarterInfoForSubmit,
  getOrder
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

function submitOrder(params) {
  const dispatchObj = {
    constants: orderConstants.SubmitOrder,
    service: {
      func: api.submitOrder,
      params
    }
  }

  return defAction(dispatchObj)
}

function getStarterInfoForSubmit() {
  const dispatchObj = {
    constants: orderConstants.GetStarterInfoForSubmit,
    service: {
      func: api.getStarterInfoForSubmit
    }
  }

  return defAction(dispatchObj)
}

function getOrder(params) {
  const dispatchObj = {
    constants: orderConstants.GetOrder,
    service: {
      func: api.getOrder,
      params
    }
  }

  return defAction(dispatchObj)
}
