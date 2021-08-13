import React, { useState, useEffect } from "react";
import { Form, Modal, Button, Input } from "antd";
import { userActions } from "../../../store/actions/user";
import agent from "../../../api/agent";
import { useDispatch, useSelector } from "react-redux";
import "antd/lib/modal/style/index.css";
import notice from "../../../components/Notice";
import ConfirmModal from "../../MobilePhoneNumber/Confirm";
import i18n from "../localization";

const Unblock = (props) => {
  const { setVisible, visible } = props;
  const dispatch = useDispatch();
  const [form] = Form.useForm();
  const [isConfirmVisible, setConfirmVisible] = useState(false);
  const [data, setData] = useState({});
  const { unblockSuccess, unblockError } = useSelector(
    (state) => state.userReducer
  );
  const isRu = useSelector((state) => state.globalReducer.isRu);

  useEffect(() => {
    if (unblockError && isConfirmVisible) notice("error", unblockError.errors);
  }, [unblockError]);

  useEffect(() => {
    if (unblockSuccess && isConfirmVisible) {
      notice("success", i18n.unblockSuccess[isRu]);
      setConfirmVisible(false);
      setVisible(false);
    }
  }, [unblockSuccess]);

  const onFinish = (values) => {
    agent.User.unblockPreCheking({ ...values })
      .then((res) => {
        if (res) {
          setData(values);
          setConfirmVisible(true);
        }
      })
      .catch((error) => {
        notice("error", error.data.errors);
      });
  };

  const onSuccess = (code) => {
    let values = data;
    values.code = code;
    dispatch(userActions.unblock(values));
  };

  return (
    <Modal
      title={i18n.phoneNumberRegister[isRu]}
      visible={visible}
      closable={true}
      onCancel={() => setVisible(false)}
      footer={[]}
    >
      <Form form={form} onFinish={onFinish}>
        <Form.Item
          name="mobilePhoneNumber"
          label="Телефон"
          normalize={(value) => (value ? value.trim() : value)}
          rules={[
            {
              required: true,
              min: 8,
              max: 20,
              message: i18n.phoneNumberLength[isRu],
            },
            {
              pattern: /^(\+?[0-9]+)$/,
              message: i18n.unlockMobileMessage[isRu],
            },
          ]}
        >
          <Input maxLength={22} placeholder="XXXXXXXXXXXXXX" />
        </Form.Item>
        {/* <MobilePhoneNumber form={form} /> */}
        <Form.Item>
          <Button type="primary" htmlType="submit">
            {i18n.codeConfirm[isRu]}
          </Button>
        </Form.Item>
      </Form>
      <ConfirmModal
        visible={isConfirmVisible}
        setVisible={(visible) => setConfirmVisible(visible)}
        mobilePhoneNumber={form.getFieldValue("mobilePhoneNumber")}
        phoneCode={form.getFieldValue("phoneCode")}
        onSuccess={(code) => onSuccess(code)}
        сloseButtonVisible={unblockError ? true : false}
      />
    </Modal>
  );
};

export default Unblock;
