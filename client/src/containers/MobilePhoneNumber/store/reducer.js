import { mobileConstants } from "./constants";

const initialState = {
  phoneCodes: null,
  // checkCodeLoading: false,
  isCodeSended: null,
  // isCodeValid: null,
  getCodesError: null,
  sendCodeError: null,
  // checkCodeError: null,
  changeNumberLoading: false,
  isNumberChanged: null,
  changeNumberError: null,
};

export default function(state = initialState, action) {
  switch (action.type) {
    case mobileConstants.GetCodes.REQUEST:
      return {
        ...state,
        getCodesError: null,
      };
    case mobileConstants.GetCodes.SUCCESS:
      return {
        ...state,
        phoneCodes: action.payload.result,
      };
    case mobileConstants.GetCodes.FAILURE:
      return {
        ...state,
        getCodesError: action.payload.err.data,
      };
    case mobileConstants.SendCode.REQUEST:
      return {
        ...state,
    //    isCodeSended: null,
        sendCodeError: null,
      };
    case mobileConstants.SendCode.SUCCESS:
      return {
        ...state,
        isCodeSended: true,
      };
    case mobileConstants.SendCode.FAILURE:
      return {
        ...state,
      //  isCodeSended: false,
        sendCodeError: action.payload.err.data,
      };
    // case mobileConstants.CheckCode.REQUEST:
    //   return {
    //     ...state,
    //     isCodeValid: null,
    //     checkCodeLoading: true,
    //     checkCodeError: null,
    //   };
    // case mobileConstants.CheckCode.SUCCESS:
    //   return {
    //     ...state,
    //     isCodeValid: true,
    //     checkCodeLoading: false,
    //   };
    // case mobileConstants.CheckCode.FAILURE:
    //   return {
    //     ...state,
    //     isCodeValid: false,
    //     checkCodeLoading: false,
    //     checkCodeError: action.payload.err.data,
    //   };
    case mobileConstants.ChangeNumber.REQUEST:
      return {
        ...state,
        changeNumberLoading: true,
        changeNumberError: null,
        isNumberChanged: null,
      };
    case mobileConstants.ChangeNumber.SUCCESS:
      return {
        ...state,
        changeNumberLoading: false,
        isNumberChanged: true,
      };
    case mobileConstants.ChangeNumber.FAILURE:
      return {
        ...state,
        changeNumberLoading: false,
        changeNumberError: action.payload.err.data,
        isNumberChanged: false,
      };
    case mobileConstants.ClearFields.REQUEST:
      return {
        ...state,
        isCodeSended: null,
        getCodesError: null,
        sendCodeError: null,
        changeNumberLoading: false,
        isNumberChanged: null,
        changeNumberError: null,
      };
    default:
      return state;
  }
}
