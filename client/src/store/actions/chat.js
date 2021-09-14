import { defAction } from "../../helpers/defaultAction";
import { chatConstants } from "../constants/";
import agent from "../../api/agent";

export const chatActions = {
  getMessagesByOrder,
  sendMessage,
};

function getMessagesByOrder(values) {
  const dispatchObj = {
    constants: chatConstants.getMessagesByOrder,
    service: {
      func: agent.Chat.getMessagesByOrder,
      params: values,
    },
  };
  return defAction(dispatchObj);
}

function sendMessage(values) {
    const dispatchObj = {
      constants: chatConstants.sendMessage,
      service: {
        func: agent.Chat.sendMessage,
        params: values,
      },
    };
    return defAction(dispatchObj);
  }