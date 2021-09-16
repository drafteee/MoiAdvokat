import {
  lawyerConstants
} from './constants'

const initialState =
{
  lawyers: [],
  isLoading: false,
  error: null,
  uploadCertificateSuccess: null,
  certificateCheckResult: null
}

export default function (state = initialState, action) {
  switch (action.type) {
    case lawyerConstants.GetLawyers.REQUEST:
      return {
        ...state,
        isLoading: true
      }
    case lawyerConstants.GetLawyers.SUCCESS:
      return {
        ...state,
        isLoading: false,
        lawyers: action.payload.result
      }
    case lawyerConstants.GetLawyers.FAILURE:
      return {
        ...state,
        isLoading: false,
        error: action.payload
      }

    case lawyerConstants.UploadCertificate.REQUEST:
      return {
        ...state,
        uploadCertificateSuccess: null
      }
    case lawyerConstants.UploadCertificate.SUCCESS:
      return {
        ...state,
        uploadCertificateSuccess: true,
        certificateCheckResult: true
      }
    case lawyerConstants.UploadCertificate.FAILURE:
      return {
        ...state,
        uploadCertificateSuccess: false,
        error: action.payload
      }

    case lawyerConstants.CheckIfCertificateExists.REQUEST:
      return {
        ...state,
        certificateCheckResult: null
      }
    case lawyerConstants.CheckIfCertificateExists.SUCCESS:
      return {
        ...state,
        certificateCheckResult: action.payload.result
      }
    case lawyerConstants.CheckIfCertificateExists.FAILURE:
      return {
        ...state,
        error: action.payload
      }

    default:
      return state
  }
}