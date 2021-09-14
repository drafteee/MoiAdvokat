import React, { useState, useEffect } from "react";
import { Row, Form, Input, Button, Select, Checkbox } from "antd";
import { useDispatch, useSelector } from "react-redux";
import { userActions } from "../../../store/actions/user";
import Notification from "../../../helpers/Notifications";
import i18n from "./localization";
import "antd/lib/checkbox/style/index.css";

import "./style.css";
import agent from "../../../api/agent";

const { Option } = Select;

const Register = () => {
  const dispatch = useDispatch();
  const [form] = Form.useForm();
  const [data, setData] = useState({});
  const validateMessages = {
    required: i18n.requiredField[isRu],
  };

  const {
    registerLoading,
    registerError,
    preCheckRegisterSuccess,
    preCheckRegisterLoading,
    preCheckRegisterError,
  } = useSelector((state) => state.userReducer);
  const isRu = useSelector((state) => state.globalReducer.isRu);

  useEffect(() => {
    if (preCheckRegisterSuccess) {
      const values = form.getFieldsValue();
      dispatch(
        userActions.register({
          UserName: values.userName,
          Password: values.password,
          Email: values.email,
        })
      );
    }
  }, [preCheckRegisterSuccess, preCheckRegisterLoading]);

  const onFinish = (values) => {
    dispatch(
      userActions.preCheckRegister({
        UserName: values.userName,
        Password: values.password,
        Email: values.email,
      })
    );
  };

  const tailFormItemLayout = {
    wrapperCol: {
      lg: { offset: 12 },
      md: { offset: 10 },
      sm: { offset: 6 },
      xs: { offset: 6 },
    },
  };

  return (
    <>
      <h2>{i18n.registerHeader[isRu]}</h2>
      <Row align="middle" justify="center">
        <Form
          className="register-width"
          form={form}
          onFinish={onFinish}
          validateMessages={validateMessages}
        >
          <Form.Item
            name="userName"
            label={i18n.userName[isRu]}
            rules={[
              {
                pattern: /^(?=.*[a-zA-Z])[a-zA-Z0-9]+$/,
                message: i18n.userNameMessage[isRu],
                required: true,
              },
              {
                min: 2,
                max: 32,
                message: i18n.userNameLength[isRu],
              },
            ]}
            hasFeedback
          >
            <Input
              autoComplete="off"
              maxLength={35}
              placeholder={i18n.userName[isRu]}
            />
          </Form.Item>
          <Form.Item
            name="password"
            label="Пароль"
            rules={[
              {
                required: true,
                pattern: /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z#$^+=!*()@%&]{8,}$/,
                message: i18n.passwordMessage[isRu],
              },
            ]}
            normalize={(value) => (value ? value.trim() : value)}
            hasFeedback
          >
            <Input.Password autoComplete="new-password" maxLength={100} />
          </Form.Item>

          <Form.Item
            name="confirm"
            label={i18n.confirmPassword[isRu]}
            dependencies={["password"]}
            normalize={(value) => (value ? value.trim() : value)}
            hasFeedback
            rules={[
              {
                required: true,
                message: i18n.confirmPasswordRequired[isRu],
              },
              ({ getFieldValue }) => ({
                validator(rule, value) {
                  if (!value || getFieldValue("password") === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(i18n.passwordEquals[isRu]);
                },
              }),
            ]}
          >
            <Input.Password autoComplete="new-password" maxLength={100} />
          </Form.Item>
          <Form.Item
            name="email"
            label="Email"
            rules={[
              {
                type: "email",
                message: i18n.incorrectEmail[isRu],
              },
            ]}
          >
            <Input autoComplete="off" maxLength={100} placeholder="Email" />
          </Form.Item>

          <Form.Item
            className="register-tail"
            style={{ paddingTop: "20px" }}
            {...tailFormItemLayout}
          >
            <Button
              type="primary"
              htmlType="submit"
              loading={registerLoading}
              disabled={false}
            >
              {i18n.register[isRu]}
            </Button>
          </Form.Item>
        </Form>
      </Row>
    </>
  );
};

export default Register;
