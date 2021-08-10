import React, { useState, useEffect } from "react";

import { Form, Modal, Input, Button, Row } from "antd";
import { mobileActions } from "../store/actions";
import { useDispatch, useSelector } from "react-redux";

import LocalLoading from "../../../components/Loading/Local";
import notice from "../../../components/Notice";
import i18n from "../localization";

import "antd/lib/modal/style/index.css";

//seconds; should be similar with appsettings.json;
const _time = 60;

const ConfirmMobile = (props) => {
  const {
    mobilePhoneNumber,
    phoneCode,
    setVisible,
    onSuccess,
    сloseButtonVisible,
    visible,
  } = props;
  const dispatch = useDispatch();
  const isRu = useSelector((state) => state.globalReducer.isRu);
  const { isCodeSended, sendCodeError, changeNumberLoading } = useSelector(
    (state) => state.mobileReducer
  );
  const [disabled, setDisabled] = useState(true);
  const [IsRepeated, setIsRepeated] = useState(false);
  const [time, setTime] = useState(_time);

  useEffect(() => {
    if (visible) sendCode();
  }, [visible]);

  useEffect(() => {
    if (isCodeSended && !IsRepeated) createTimeout();
  }, [isCodeSended]);

  useEffect(() => {
   // if (sendCodeError && visible) {
      // notice("error", sendCodeError.errors);
    //  setVisible(false);
    //}
  }, [sendCodeError]);

  function createTimeout() {
    let timerId = setInterval(() => setTime((prevTime) => prevTime - 1), 1000);
    setTimeout(() => {
      clearInterval(timerId);
      setDisabled(false);
    }, _time * 1000);
  }

  const repeat = () => {
    sendCode();
    setDisabled(true);
    setIsRepeated(true);
    setTime(_time);
    createTimeout();
  };

  const sendCode = () => {
    dispatch(
      mobileActions.sendCode({
        mobilePhoneNumber,
        phoneCode,
        toConfirm: true,
      })
    );
  };

  const onFinish = (values) => {
    onSuccess(values.code);
  };

  return (
    <Modal
      title={i18n.confirmPhone[isRu]}
      visible={visible}
      closable={сloseButtonVisible}
      onCancel={() => {
        сloseButtonVisible && setVisible(false);
      }}
      footer={[]}
    >
      {isCodeSended ? (
        <Row justify="center">
          <Form onFinish={onFinish}>
            <Form.Item
              name="code"
              label={i18n.confirmCode[isRu]}
              rules={[
                {
                  required: true,
                  message: i18n.requiredField[isRu],
                },
              ]}
            >
              <Input autoComplete="off" />
            </Form.Item>
            <Form.Item>
              <Button
                type="primary"
                htmlType="submit"
                loading={changeNumberLoading}
              >
                {i18n.confirmPhoneNumber[isRu]}
              </Button>
            </Form.Item>
            <Form.Item>
              <Button key="repeat" disabled={disabled} onClick={() => repeat()}>
                {i18n.repeatCode[isRu][0]}
                {time > 0 &&
                  `${i18n.repeatCode[isRu][1]}${time}${i18n.repeatCode[isRu][2]}`}
              </Button>
            </Form.Item>
          </Form>
        </Row>
      ) : (
        <LocalLoading text={i18n.codeLoading[isRu]} />
      )}
    </Modal>
  );
};

export default ConfirmMobile;
