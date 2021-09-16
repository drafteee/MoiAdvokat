import {
  orderStatusesConstants
} from './constants'

const initialState =
{
  orderStatuses: [],
  getStatusesError: null
}

export default function (state = initialState, action) {
  switch (action.type) {
    case orderStatusesConstants.GetStatuses.REQUEST:
      return {
        ...state,
        orderStatuses: []
      }
    case orderStatusesConstants.GetStatuses.SUCCESS:
      return {
        ...state,
        orderStatuses: action.payload.result
      }
    case orderStatusesConstants.GetStatuses.FAILURE:
      return {
        ...state,
        getStatusesError: action.payload
      }

    default:
      return state
  }
}