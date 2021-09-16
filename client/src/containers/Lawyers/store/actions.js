import { lawyerConstants } from "./constants";
import { defAction } from '../../../helpers/defaultAction'
import api from "./api";


export const lawyerActions = {
  getAll,
  getAllCurrent,
  uploadCertificate,
  checkIfCertificateExists
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

function uploadCertificate(params) {
  const dispatchObj = {
    constants: lawyerConstants.UploadCertificate,
    service: {
      func: api.uploadCertificate,
      params
    }
  }

  return defAction(dispatchObj)
}

function checkIfCertificateExists(params) {
  const dispatchObj = {
    constants: lawyerConstants.CheckIfCertificateExists,
    service: {
      func: api.checkIfCertificateExists,
      params
    }
  }

  return defAction(dispatchObj)
}
