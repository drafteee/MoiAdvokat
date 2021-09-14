import React, { useEffect, useMemo, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { SERVER_SIGNALR_URL } from "../../Global";
import { List, Row, Col, Avatar, Typography, Input, Button } from "antd";
import "antd/lib/list/style/index.css";
import "antd/lib/avatar/style/index.css";
import "antd/lib/input/style/index.css";
import "./style.css";
import { useDispatch, useSelector } from "react-redux";
import { chatActions } from "../../store/actions/chat";
import convertDBDateToString from "../../helpers/convertDBDatetoString";

const { Text } = Typography;
const ChatTestPage = (props) => {
  const dispatch = useDispatch();
  const { orderId } = props.match.params;
  const user = useSelector((state) => state.userReducer.user);
  const { messages, sendMessageResult } = useSelector(
    (state) => state.chatReducer
  );
  const [inputMessage, setInputMessage] = useState("");

  const [hub, setHub] = useState(() => {
    const hub = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:49542/signalR/`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect([0, 0, 1000, 5000, 10000])
      .build();
    hub.keepAliveIntervalInMilliseconds = 1000 * 60 * 10;
    hub.serverTimeoutInMilliseconds = 1000 * 60 * 10;
    hub.start();
    // hub.onreconnected((err) => {
    //   hub.invoke("SetConnection", user.id).catch((err) => console.log(err));
    // });
    console.log(SERVER_SIGNALR_URL);
    // hub.on("getMessages", (sender, recepient) => {
    //   console.log(sendr, recepient);
    // });
    // hub.on("sendMessages", (sender, recepient) => {
    //   console.log(sendr, recepient);
    // });
    hub.on("sendMessages", (sender, recepient) => {
      console.log(sender, recepient);
    });
    return hub;
  });

  useEffect(() => {
    dispatch(chatActions.getMessagesByOrder({ OrderId: orderId }));
    return () => {
      hub.stop();
    };
  }, []);

  useEffect(() => {
    if (sendMessageResult && sendMessageResult.Success) {
      dispatch(chatActions.getMessagesByOrder({ OrderId: orderId }));
    }
  }, [sendMessageResult]);

  const displaydMessages = useMemo(() => {
    return messages.map((item, index) => {
     
      return {
        messageContent: item.MessageContent,
        isNotCurrent: user.UserName !== item.UserFromName,
        dateCreate: convertDBDateToString(item.CreatedOn),
        key: index,
      };
    });
  }, [messages]);

  function sendMessage() {
    dispatch(
      chatActions.sendMessage({
        OrderId: orderId,
        MessageContent: inputMessage,
      })
    );
    setInputMessage("");
  }

  function textAreaChange(e) {
    setInputMessage(e.target.value);
  }

  return (
    <>
      <div className="container-home infinite-container">
        <List>
          {displaydMessages.map((message, i) => {
            let className = "userMessage";
            let align = "end";
            let text = message.messageContent;
            let avatar = null;
            if (message.isNotCurrent) {
              align = "start";
              avatar = (
                <Avatar
                  style={{
                    backgroundColor: "#87d068",
                    verticalAlign: "middle",
                  }}
                  size="large"
                >
                  {"А"}
                </Avatar>
              );
              className = "";
            }

            return (
              <List.Item key={i}>
                <Row type="flex" justify={align} style={{ width: "100%" }}>
                  <Col>
                    <Row>
                      <List.Item.Meta
                        className={className}
                        avatar={avatar}
                        title={
                          <Text type="secondary">{message.dateCreate}</Text>
                        }
                        description={text}
                      />
                    </Row>
                  </Col>
                </Row>
              </List.Item>
            );
          })}
        </List>
      </div>
      <br />
      <Input.TextArea
        autosize={{ minRows: 4, maxRows: 6 }}
        onChange={textAreaChange}
        placeholder="Введите сообщение"
        value={inputMessage}
      />
      <br />
      <br />
      <Button
        type="primary"
        onClick={sendMessage}
        style={{ marginBottom: "2%" }}
      >
        Отправить сообщение
      </Button>
    </>
  );
};

export default ChatTestPage;
