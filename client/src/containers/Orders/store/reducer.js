import {
  orderConstants
} from './constants'

const initialState =
{
  orders: [],
  isLoading: false,
  error: null,
  submitOrderSuccess: null,
  starterInfoForSubmit: {},
  getStarterInfoForSubmitSuccess: null,
  getOneOrderLoading: false,
  oneOrder: null
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

    case orderConstants.SubmitOrder.REQUEST:
      return {
        ...state,
        submitOrderSuccess: null
      }
    case orderConstants.SubmitOrder.SUCCESS:
      return {
        ...state,
        submitOrderSuccess: true
      }
    case orderConstants.SubmitOrder.FAILURE:
      return {
        ...state,
        submitOrderSuccess: false,
        error: action.payload
      }

    case orderConstants.GetStarterInfoForSubmit.REQUEST:
      return {
        ...state,
        starterInfoForSubmit: {},
        getStarterInfoForSubmitSuccess: null
      }
    case orderConstants.GetStarterInfoForSubmit.SUCCESS:
      return {
        ...state,
        getStarterInfoForSubmitSuccess: true,
        starterInfoForSubmit: action.payload.result
      }
    case orderConstants.GetStarterInfoForSubmit.FAILURE:
      return {
        ...state,
        getStarterInfoForSubmitSuccess: false,
        error: action.payload
      }

    case orderConstants.GetOrder.REQUEST:
      return {
        ...state,
        getOneOrderLoading: true,
        oneOrder: null
      }
    case orderConstants.GetOrder.SUCCESS:
      return {
        ...state,
        getOneOrderLoading: false,
        oneOrder: action.payload.result
      }
    case orderConstants.GetOrder.FAILURE:
      return {
        ...state,
        getOneOrderLoading: false,
        error: action.payload
      }
    
    default:
      return state
  }
}