import { requests } from "./agent";

const Chat = {
  getMessagesByOrder: (orderId) =>
    requests.getWithParams("/chat/getMessagesByOrder", orderId),
  sendMessage: (message) => requests.post("/chat/sendMessage", message),
};

export default Chat;
