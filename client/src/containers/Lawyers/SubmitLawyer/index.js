import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'

import { Form, Input, Button, DatePicker, Row, Col, Select } from 'antd'

import { lawyerActions } from '../store/actions'

const SubmitLawyer = () => {
  const {
      starterInfoForSubmit
  } = useSelector(state => state.orderReducer)

  const [form] = Form.useForm()
  const dispatch = useDispatch()

  function submitOrder(params) {
      dispatch(orderActions.submitOrder(params))
  }

  useEffect(() => {
      dispatch(orderActions.getStarterInfoForSubmit())
  }, [])

  function onFinish(values) {
      submitOrder({
          ...values,
          SpecializationsIds: form.getFieldValue("SpecializationsIds")
      })
  }

  const onSpecializationsChange = value => {
      form.setFieldsValue({
          SpecializationsIds: value
      })
  }

  return (
    <Form
      form={form}
      onFinish={onFinish}
      scrollToFirstError={true}
      layout="vertical">
      <Form.Item
          name="nameClient"
          label="Имя"
          rules={[{
              required: true,
              message: "Пожалуйста, введите Ваше имя"
          }]}>
          <Input placeholder="Введите Ваше имя" />
      </Form.Item>
      <Form.Item
          name="specializations"
          label="Специализации"
          rules={[{
              required: true,
              message: "Пожалуйста, выберите одну или несколько специализаций"
          }]}>
          <Select
              onChange={onSpecializationsChange}
              mode="multiple"
              placeholder="Выберите одну или несколько специализаций">
              {starterInfoForSubmit && starterInfoForSubmit.Specializations
                  ? starterInfoForSubmit.Specializations.map(spec => {
                      return (
                          <Select.Option key={spec.Code} value={spec.Id}>
                              {spec.Name}
                          </Select.Option>
                      )
                  })
                  : null}
          </Select>
      </Form.Item>
      <Form.Item
          name="header"
          label="Заголовок"
          rules={[{
              required: true,
              max: 40,
              message: "Пожалуйста, введите краткое описание заказа"
          }]}>
          <Input placeholder="Введите краткое описание заказа (макс. 40 символов)" />
      </Form.Item>
      <Form.Item
          name="description"
          label="Описание"
          rules={[{
              required: true,
              message: "Пожалуйста, введите описание заказа"
          }]}>
          <Input.TextArea placeholder="Введите описание заказа" />
      </Form.Item>
      <Form.Item
          name="price"
          label="Цена"
          rules={[{
              required: true,
              message: "Пожалуйста, введите цену заказа",
              min: 0
          }]}>
          <Input placeholder="Введите цену заказа" type="number" />
      </Form.Item>
      <Form.Item
          name="endDueDate"
          label="Срок исполнения"
          rules={[{
              required: true,
              message: "Пожалуйста, введите срок исполнения заказа"
          }]}>
          <DatePicker
              style={{ width: "100%" }}
              disabledDate={date => date < Date.now()}
              format="DD.MM.YYYY"
              placeholder="Введите срок исполнения заказа" />
      </Form.Item>
      <MobilePhoneNumber
          name="phoneNumber"
          errorMessage="Пожалуйста, введите Ваш мобильный номер"
          placeholder="+375123456789" />
      <Form.Item>
          <Button type="primary" htmlType="submit">Сохранить</Button>
      </Form.Item>
  </Form>
  )
}

export default SubmitLawyer