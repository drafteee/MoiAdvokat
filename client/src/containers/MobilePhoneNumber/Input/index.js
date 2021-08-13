import React, { useEffect, useState } from "react";
import { Input, Form, Select } from "antd";
import { useDispatch, useSelector } from "react-redux";
import { mobileActions } from "../store/actions";
import notice from "../../../components/Notice";
import "antd/lib/select/style/index.css";
import "./style.css";
import i18n from "../localization";
import i18nGlobal from "../../../localization";

const MobilePhoneNumber = (props) => {
  const dispatch = useDispatch();
  const [phoneCode, setPhoneCode] = useState(null);
  const { initialNumber, initialPhoneCode, form } = props;
  let [mobilePhoneCodes, setPhoneCodes] = useState([]);
  const { phoneCodes, getCodesError } = useSelector(
    (state) => state.mobileReducer
  );
  const isRu = useSelector((state) => state.globalReducer.isRu);

  useEffect(() => {
    if (!phoneCodes) {
      dispatch(mobileActions.getInternationalPhoneCodes());
    }
    if (initialPhoneCode) {
      form.setFieldsValue({ phoneCode: initialPhoneCode });
      setPhoneCode(initialPhoneCode);
    }
  }, []);

  useEffect(() => {
    if (phoneCodes && !initialPhoneCode) {
      let BY = phoneCodes.find((code) => {
        return code.value == "+375";
      });
      form.setFieldsValue({ phoneCode: BY });
      setPhoneCode(BY);
    }
    if (phoneCodes && phoneCodes.length) setPhoneCodes(phoneCodes);
  }, [phoneCodes]);

  useEffect(() => {
    if (getCodesError) notice("error", getCodesError.errors);
  }, [getCodesError]);

  const changePhoneCode = (id) => {
    let temp = getPhoneCode(id);
    setPhoneCode(temp);
    form.setFieldsValue({ phoneCode: temp });
  };

  const searchPhoneCode = (value) => {
    let temp = [];
    phoneCodes.forEach((code) => {
      if (
        code.value.includes(value) ||
        code.reCountry.name.toUpperCase().includes(value.toUpperCase())
      )
        temp.push(code);
    });
    setPhoneCodes(temp);
  };

  useEffect(() => {
    form.validateFields(["mobilePhoneNumber"]);
  }, [phoneCode]);

  const getPhoneCode = (id) => {
    let temp = phoneCodes.find((obj) => {
      return obj.id === id;
    });
    return temp;
  };

  return (
    phoneCodes &&
    phoneCode && (
      <>
        <Form.Item
          name="mobilePhoneNumber"
          label={i18nGlobal.phone[isRu]}
          initialValue={initialNumber && initialNumber}
          rules={[
            {
              required: true,
              pattern: new RegExp(`^[0-9]{${phoneCode.numberOfDigits}}$`),
              message: `${i18n.phoneFieldMessage[isRu][0]}${phoneCode.numberOfDigits}${i18n.phoneFieldMessage[isRu][1]}`,
            },
          ]}
        >
          <Input
            autoComplete="off"
            addonBefore={
              <Form.Item
                noStyle
                name="phoneCodeId"
                initialValue={
                  initialNumber ? initialPhoneCode.id : phoneCode.id
                }
              >
                <Select
                  showSearch
                  style={{ minWidth: "80px" }}
                  notFoundContent={null}
                  defaultActiveFirstOption={false}
                  filterOption={false}
                  onSearch={(value) => {
                    searchPhoneCode(value);
                  }}
                  onChange={(e) => changePhoneCode(e)}
                >
                  {mobilePhoneCodes.map((e, i) => {
                    return (
                      <Option
                        key={i}
                        value={e.id}
                        title={`(${e.reCountry.name}) ${e.value}`}
                      >
                        {e.value}
                      </Option>
                    );
                  })}
                </Select>
              </Form.Item>
            }
            placeholder={"X".repeat(phoneCode.numberOfDigits)}
          />
        </Form.Item>
        <Form.Item name="phoneCode" hidden={true}></Form.Item>
      </>
    )
  );
};

export default MobilePhoneNumber;
