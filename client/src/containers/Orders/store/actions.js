import { orderConstants } from "./constants";
import { defAction } from '../../../helpers/defaultAction'
import api from "./api";


export const orderActions = {
  getOrders,
  submitOrder,
  respondOrder,
  getStarterInfoForSubmit,
  getOrderResponses
};


function getOrderResponses(params) {
  console.log('action', params)

  const dispatchObj = {
    constants: orderConstants.GetOrderResponses,
    service: {
      func: api.getResponses,
      params
    }
  };

  return defAction(dispatchObj);
}

function respondOrder(respondObj) {
  const dispatchObj = {
    constants: orderConstants.RespondOrder,
    service: {
      func: api.orderRespond,
      params: respondObj
    },
    sucParams: respondObj
  };

  return defAction(dispatchObj);
}

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
