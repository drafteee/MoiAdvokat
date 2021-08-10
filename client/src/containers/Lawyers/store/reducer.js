import {
  lawyerConstants
} from './constants'

const initialState =
{
  lawyers: [],
  isLoading: false,
  error: null
}

export default function (state = initialState, action) {
  switch (action.type) {
    case lawyerConstants.GetLawyers.REQUEST:
      return {
        ...state,
        isLoading: true
      }
    case lawyerConstants.GetLawyers.SUCCESS:
      console.log(action.payload)
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
    default:
      return state
  }
}