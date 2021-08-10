import React, { useCallback, useEffect } from "react";
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import { Table, Tag, Space } from 'antd';
import { orderActions } from "./store/actions";
import 'antd/lib/table/style/index.css'
import 'antd/lib/dropdown/style/index.css'
import 'antd/lib/radio/style/index.css'
import 'antd/lib/tooltip/style/index.css'
import 'antd/lib/tag/style/index.css'
import 'antd/lib/pagination/style/index.css'
import 'antd/lib/notification/style/index.css'

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
      </Space>
    ),
  },
];


const Orders = () => {
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

  return (
    <Table columns={columns} dataSource={orderList} />
  )
}

export default Orders