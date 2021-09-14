import { chatConstants } from "../constants";

const initialState = {
  messages: [],
  sendMessageResult: null,
};

export default function(state = initialState, action) {
  switch (action.type) {
    case chatConstants.getMessagesByOrder.REQUEST:
      return {
        ...state,
        messages: [],
      };
    case chatConstants.getMessagesByOrder.SUCCESS:
      return {
        ...state,
        messages: action.payload.result,
      };
    case chatConstants.getMessagesByOrder.FAILURE:
      return {
        ...state,
        messages: [],
      };
    case chatConstants.sendMessage.REQUEST:
      return {
        ...state,
        sendMessageResult: null,
      };
    case chatConstants.sendMessage.SUCCESS:
      return {
        ...state,
        sendMessageResult: action.payload.result,
      };
    case chatConstants.sendMessage.FAILURE:
      return {
        ...state,
        sendMessageResult: null,
      };
    default:
      return state;
  }
}
