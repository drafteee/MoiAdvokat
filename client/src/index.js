import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import store from "./store";
import App from './App'
import 'antd/lib/menu/style/index.css'
import 'antd/lib/style/index.css'
import 'antd/lib/button/style/index.css'
import 'antd/lib/layout/style/index.css'
import 'antd/lib/form/style/index.css'
import 'antd/lib/input/style/index.css'
import 'antd/lib/select/style/index.css'
import 'antd/lib/notification/style/index.css'
import 'antd/lib/grid/style/index.css'
import 'antd/lib/date-picker/style/index.css'
import 'antd/lib/row/style/css'
import 'antd/lib/col/style/css'
import 'antd/lib/card/style/css'

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById("root")
);