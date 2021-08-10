import React, { useCallback, useEffect } from "react";
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import { Table, Tag, Space } from 'antd';
import { lawyerActions } from "./store/actions";
import 'antd/lib/table/style/index.css'
import 'antd/lib/dropdown/style/index.css'
import 'antd/lib/radio/style/index.css'
import 'antd/lib/tooltip/style/index.css'
import 'antd/lib/tag/style/index.css'
import 'antd/lib/pagination/style/index.css'
import 'antd/lib/notification/style/index.css'

const columns = [
  {
    title: 'FirstName',
    dataIndex: 'FirstName',
    key: 'firstName',
    render: text => <a>{text}</a>,
  },
  {
    title: 'LicenseNumber',
    dataIndex: 'LicenseNumber',
    key: 'licenseNumber',
  },
  {
    title: 'DateOfIssue',
    dataIndex: 'DateOfIssue',
    key: 'dateOfIssue',
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


const Lawyers = () => {
  const dispatch = useDispatch();
  const lawyerList =  useSelector(state => state.lawyerReducer.lawyers)
  const getAll = useCallback(
    () => {
      dispatch(lawyerActions.getAll())
    },
    [dispatch],
  )
  useEffect(() => {
    getAll()
  }, [])

  return (
    <Table columns={columns} dataSource={lawyerList} />
  )
}

export default Lawyers