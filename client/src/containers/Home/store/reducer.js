import {
  homeConstants
} from './constants'

const initialState =
{
  isSending: false,
  isRu: 1,
  statisticsObject: null
}

export default function (state = initialState, action) {
  switch (action.type) {
    case homeConstants.SEND_FEEDBACK_REQUEST:
      return {
        ...state,
        isSending: true,
        feedback: action.payload
      }
    case homeConstants.SEND_FEEDBACK_SUCCESS:
      return {
        ...state,
        isSending: false,
        currentItem: action.payload
      }
    case homeConstants.SEND_FEEDBACK_FAILURE:
      return {
        ...state,
        isSending: false,
        error: action.payload
      }
    case homeConstants.GetStatistics.REQUEST:
      return {
        ...state,
        isSending: true
      }
    case homeConstants.GetStatistics.SUCCESS:
      return {
        ...state,
        isSending: false,
        statisticsObject: action.payload.result
      }
    case homeConstants.GetStatistics.FAILURE:
      return {
        ...state,
        isSending: false,
        error: action.payload
      }
    default:
      return state
  }
}