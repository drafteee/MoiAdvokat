import {
  orderConstants
} from './constants'

const initialState =
{
  orders: [],
  orderResponses: [],
  isLoading: false,
  error: null,
  submitOrderSuccess: null,
  respondOrderSuccess: null,
  starterInfoForSubmit: {},
  getStarterInfoForSubmitSuccess: null
}

export default function (state = initialState, action) {
  switch (action.type) {
    case orderConstants.ChooseLawyer.REQUEST:
      return {
        ...state
      }
    case orderConstants.ChooseLawyer.SUCCESS:
      return {
        ...state,
        orderResponses: state.orderResponses.map(item => {

          if(item.LawyerId == action.payload.sucParams.LawyerId){
            return {
              ...item,
              IsChoosen: true
            }
          }
          return item
        })
      }
    case orderConstants.ChooseLawyer.FAILURE:
      return {
        ...state,
        error: action.payload
      }
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
      case orderConstants.GetOrderResponses.REQUEST:
        return {
          ...state,
          isLoading: true
        }
      case orderConstants.GetOrderResponses.SUCCESS:
        return {
          ...state,
          isLoading: false,
          orderResponses: action.payload.result
        }
      case orderConstants.GetOrderResponses.FAILURE:
        return {
          ...state,
          isLoading: false,
          error: action.payload
        }
    case orderConstants.RespondOrder.REQUEST:
      return {
        ...state,
        respondOrderSuccess: null
      }
    case orderConstants.RespondOrder.SUCCESS:
      return {
        ...state,
        respondOrderSuccess: true,
        orders: state.orders.map(item => {

          if(item.Id == action.payload.sucParams.OrderId){
            return {
              ...item,
              IsResponse: true
            }
          }
          return item
        })
      }
    case orderConstants.RespondOrder.FAILURE:
      return {
        ...state,
        respondOrderSuccess: false,
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

    default:
      return state
  }
}