import { lawyerConstants } from "./constants";
import { defAction } from '../../../helpers/defaultAction'
import api from "./api";


export const lawyerActions = {
  getAll
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
