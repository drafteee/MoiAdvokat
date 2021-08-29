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
import 'antd/lib/date-picker/style/index.css'
import RespondForm from "./RespondForm";

const Orders = () => {

const columns = [
  {
    title: 'Имя',
    dataIndex: 'NameClient',
    key: 'nameClient',
    render: text => <a>{text}</a>,
  },
  {
    title: 'Дата поста',
    dataIndex: 'StartDate',
    key: 'startDate',
  },
  {
    title: 'Дата окончания',
    dataIndex: 'EndDueDate',
    key: 'endDueDate',
  },
  {
    title: 'Действия',
    key: 'action',
    render: (text, record) => (
      <Space size="middle">
        {
          !record.IsResponse && <Button type="primary" onClick={() => showModal(record)}>
          Отозваться
      </Button>
        }
        
      </Space>
    ),
  },
];


  const [isModalVisible, setIsModalVisible] = useState(false);
  const [currentId, setCurrentId] = useState(null);

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


  const showModal = (record) => {
    setCurrentId(record.Id)
    setIsModalVisible(true);
  };




  return (
    <>
    <Table columns={columns} dataSource={orderList} />
    <RespondForm isModalVisible={isModalVisible} setIsModalVisible={setIsModalVisible} currentId={currentId}/>
      
    </>
  )
}

export default Orders