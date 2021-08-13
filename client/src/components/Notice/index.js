import { notification } from "antd";
import "antd/lib/notification/style/index.css";
import "./style.css";

// types: info, success warning, error;

const notice = (type, title, message = "", duration = null) => {
  notification[type]({
    message: title,
    description: message,
    placement: "bottomLeft",
    duration: type === "success" ? 5 : duration,
  });
};

export default notice;
