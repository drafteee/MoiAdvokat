import {
  lawyerConstants
} from './constants'

const initialState =
{
  lawyer: null,
  lawyers: [],
  isLoading: false,
  error: null,
  uploadCertificateSuccess: null,
  certificateCheckResult: null,
  lawyerConfirmationSuccess: null
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

    case lawyerConstants.GetById.REQUEST:
      return {
        ...state,
        isLoading: true,
        lawyer: null
      }
    case lawyerConstants.GetById.SUCCESS:
      return {
        ...state,
        isLoading: false,
        lawyer: action.payload.result
      }
    case lawyerConstants.GetById.FAILURE:
      return {
        ...state,
        isLoading: false,
        error: action.payload
      }

    case lawyerConstants.ConfirmLawyer.REQUEST:
      return {
        ...state,
        lawyerConfirmationSuccess: null
      }
    case lawyerConstants.ConfirmLawyer.SUCCESS:
      return {
        ...state,
        lawyerConfirmationSuccess: true
      }
    case lawyerConstants.ConfirmLawyer.FAILURE:
      return {
        ...state,
        lawyerConfirmationSuccess: false,
        error: action.payload
      }

    default:
      return state
  }
}