import React, { useCallback, useEffect, useState } from "react";
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import { Table, Tag, Space, Modal, Button, Form, Input, InputNumber } from 'antd';
import { orderActions } from "./store/actions";

import 'antd/lib/table/style/index.css'
import 'antd/lib/dropdown/style/index.css'
import 'antd/lib/radio/style/index.css'
import 'antd/lib/tooltip/style/index.css'
import 'antd/lib/tag/style/index.css'
import 'antd/lib/pagination/style/index.css'
import 'antd/lib/notification/style/index.css'
import 'antd/lib/modal/style/index.css'
import 'antd/lib/form/style/index.css'
import 'antd/lib/input-number/style/index.css'

const layout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 16,
  },
};

const Orders = () => {

const columns = [
  {
    title: 'NameClient',
    dataIndex: 'NameClient',
    key: 'nameClient',
    render: text => <a>{text}</a>,
  },
  {
    title: 'StartDate',
    dataIndex: 'StartDate',
    key: 'startDate',
  },
  {
    title: 'EndDueDate',
    dataIndex: 'EndDueDate',
    key: 'endDueDate',
  },
  {
    title: 'Action',
    key: 'action',
    render: (text, record) => (
      <Space size="middle">
        <a>Invite {record.FirstName}</a>
        <a>Delete</a>
        <Button type="primary" onClick={showModal}>
          Отозваться
      </Button>
      </Space>
    ),
  },
];


  const [isModalVisible, setIsModalVisible] = useState(false);

  const dispatch = useDispatch();
  const orderList =  useSelector(state => state.orderReducer.orders)
  const getAll = useCallback(
    () => {
      dispatch(orderActions.getOrders())
    },
    [dispatch],
  )
  useEffect(() => {
    getAll()
  }, [])


  const showModal = () => {
    setIsModalVisible(true);
  };

  const handleOk = () => {
    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  const onFinish = (values) => {
    console.log(values);
  };

  return (
    <>
    <Table columns={columns} dataSource={orderList} />
    
      <Modal title="Basic Modal" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel}>
      <Form {...layout} name="nest-messages" onFinish={onFinish}>
        <Form.Item
          name={['user', 'name']}
          label="Name"
          rules={[
            {
              required: true,
            },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name={['user', 'email']}
          label="Email"
          rules={[
            {
              type: 'email',
            },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name={['user', 'age']}
          label="Age"
          rules={[
            {
              type: 'number',
              min: 0,
              max: 99,
            },
          ]}
        >
          <InputNumber />
        </Form.Item>
        <Form.Item name={['user', 'website']} label="Website">
          <Input />
        </Form.Item>
        <Form.Item name={['user', 'introduction']} label="Introduction">
          <Input.TextArea />
        </Form.Item>
        <Form.Item wrapperCol={{ ...layout.wrapperCol, offset: 8 }}>
          <Button type="primary" htmlType="submit">
            Submit
          </Button>
        </Form.Item>
      </Form>
      </Modal>
    </>
  )
}

export default Orders