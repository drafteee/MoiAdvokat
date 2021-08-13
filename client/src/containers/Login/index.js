import React, { useState, useEffect } from "react";
import { Row, Form, Input, Button, Col, Card } from "antd";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import "antd/lib/card/style/index.css";
import "antd/lib/tabs/style/index.css";
import "./style.css";
import { userActions } from "../../store/actions";
import { userConstants } from "../../store/constants";
import UnblockModal from "./Unblock";
import Notification from "../../helpers/Notifications";
import ConfirmModal from "../MobilePhoneNumber/Confirm";
import i18n from "./localization";

const Login = () => {
  const [form] = Form.useForm();
  const [unblockVisible, setUnblockVisible] = useState(false);
  const [isConfirmVisible, setConfirmVisible] = useState(false);
  const [mobilePhoneNumberToConfirm, setMobilePhoneNumberToConfirm] = useState(
    null
  );
  const [phoneCodeToConfirm, setPhoneCodeToConfirm] = useState(null);
  const dispatch = useDispatch();
  const {
    isLoginLoading,
    loginError,
    confirmPhoneError,
    confirmPhoneSuccess,
  } = useSelector((state) => state.userReducer);
  const isRu = useSelector((state) => state.globalReducer.isRu);
  const validateMessages = {
    required: i18n.requiredField[isRu],
  };

  useEffect(() => {
    if (loginError) {
      if (loginError.code === 423) {
        
      } else if (loginError.code === 412) {
        setPhoneCodeToConfirm(loginError.dataObject.phoneCode);
        setMobilePhoneNumberToConfirm(loginError.dataObject.mobilePhoneNumber);
        
      } else {
      }
    }
    dispatch({ type: userConstants.ClearLoginData });
  }, [loginError]);

  useEffect(() => {
    // if (confirmPhoneError && isConfirmVisible)
      // notice("error", confirmPhoneError.errors);
  }, [confirmPhoneError]);

  useEffect(() => {
    if (confirmPhoneSuccess && isConfirmVisible) {
      // notice("success", i18n.confirmPhoneSuccess[isRu]);
      setConfirmVisible(false);
    }
  }, [confirmPhoneSuccess]);

  const onFinish = (data) => {
    dispatch(userActions.login(data));
  };

  const onSuccess = (code) => {
    let data = {
      code: code,
      phoneCode: phoneCodeToConfirm,
      mobilePhoneNumber: mobilePhoneNumberToConfirm,
    };
    dispatch(userActions.confirmPhone(data));
  };

  return (
    <>
      <h2>{i18n.header[isRu]}</h2>
      <Row align="middle" justify="center">
        <Col className="login-row">
          <Card
            className="login-card"
            actions={[
              <Link to="/restorePassword">
                <Button size="small" type="link">
                  {i18n.restorePassword[isRu]}
                </Button>
              </Link>,
              <Link to="/register">
                <Button size="small" type="link">
                  {i18n.register[isRu]}
                </Button>
              </Link>
            ]}
          >
            <Form
              className="login-layuot"
              form={form}
              onFinish={onFinish}
              validateMessages={validateMessages}
            >
              <Form.Item
                name="userName"
                label={i18n.userField[isRu]}
                normalize={(value) => (value ? value.trim() : value)}
                rules={[
                  {
                    required: true,
                  },
                  {
                    min: 2,
                    max: 32,
                    message: i18n.userFieldLength[isRu],
                  },
                  {
                    pattern: /^(\+?[a-zA-Z0-9]+)$/,
                    message: i18n.userFieldMessage[isRu],
                  },
                ]}
              >
                <Input maxLength={35} />
              </Form.Item>
              <Form.Item
                name="password"
                label="Пароль"
                rules={[
                  {
                    required: true,
                  },
                ]}
                hasFeedback
              >
                <Input.Password />
              </Form.Item>
              <Form.Item>
                <Button
                  className="btn-form-float-right"
                  type="primary"
                  htmlType="submit"
                  loading={isLoginLoading}
                >
                  {i18n.login[isRu]}
                </Button>
              </Form.Item>
            </Form>
          </Card>
          <UnblockModal
            visible={unblockVisible}
            setVisible={(visible) => setUnblockVisible(visible)}
          />
        </Col>
      </Row>
      <ConfirmModal
        visible={isConfirmVisible}
        setVisible={(visible) => setConfirmVisible(visible)}
        mobilePhoneNumber={mobilePhoneNumberToConfirm}
        phoneCode={phoneCodeToConfirm}
        onSuccess={(code) => onSuccess(code)}
        сloseButtonVisible={confirmPhoneError ? true : false}
      />
    </>
  );
};

export default Login;
