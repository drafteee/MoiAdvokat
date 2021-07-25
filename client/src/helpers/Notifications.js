import { notification } from "antd";
import "antd/lib/notification/style/index.css";

const infoNotice = (title, description = "", duration = null) => {
  notification["info"]({
    message: title,
    description: description,
    placement: "bottomLeft",
    duration: duration,
  });
};

const successNotice = (title, description = "", duration = null) => {
  return notification["success"]({
    message: title,
    description: description,
    placement: "bottomLeft",
    duration: duration === null ? 5 : duration,
  });
};

const warningNotice = (title, description = "", duration = null) => {
  notification["warning"]({
    message: title,
    description: description,
    placement: "bottomLeft",
    duration: duration,
  });
};

const errorNotice = (title, description = "", duration = null) => {
  notification["error"]({
    message: title,
    description: description,
    placement: "bottomLeft",
    duration: duration,
  });
};

export default {
  infoNotice,
  successNotice,
  warningNotice,
  errorNotice,
};
