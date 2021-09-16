import {
  specializationsConstants
} from './constants'

const initialState =
{
  specializations: [],
  getStatusesError: null
}

export default function (state = initialState, action) {
  switch (action.type) {
    case specializationsConstants.GetSpecializations.REQUEST:
      return {
        ...state,
        specializations: []
      }
    case specializationsConstants.GetSpecializations.SUCCESS:
      return {
        ...state,
        specializations: action.payload.result
      }
    case specializationsConstants.GetSpecializations.FAILURE:
      return {
        ...state,
        getStatusesError: action.payload
      }

    default:
      return state
  }
}