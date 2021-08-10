import {
  orderConstants
} from './constants'

const initialState =
{
  orders: [],
  isLoading: false,
  error: null
}

export default function (state = initialState, action) {
  switch (action.type) {
    case orderConstants.GetOrders.REQUEST:
      return {
        ...state,
        isLoading: true
      }
    case orderConstants.GetOrders.SUCCESS:
      console.log(action.payload)
      return {
        ...state,
        isLoading: false,
        orders: action.payload.result
      }
    case orderConstants.GetOrders.FAILURE:
      return {
        ...state,
        isLoading: false,
        error: action.payload
      }
    default:
      return state
  }
}