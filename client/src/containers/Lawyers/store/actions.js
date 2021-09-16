import { lawyerConstants } from "./constants";
import { defAction } from '../../../helpers/defaultAction'
import api from "./api";


export const lawyerActions = {
  getAll,
  getAllCurrent
};


function getAll() {
  const dispatchObj = {
    constants: lawyerConstants.GetLawyers,
    service: {
      func: api.getAll
    }
  };

  return defAction(dispatchObj);
}

function getAllCurrent() {
  const dispatchObj = {
    constants: lawyerConstants.GetLawyers,
    service: {
      func: api.getAllCurrent
    }
  };

  return defAction(dispatchObj);
}
