import React from 'react'
import { Result, Button } from 'antd';
import 'antd/lib/result/style/index.css'
import history from '../../helpers/history';

export const UnauthorizedAccess = () => {
  return (
    <Result
      status="403"
      title="403"
      subTitle="Извините, вам нужно авторизоваться."
      extra={<Button type="primary" onClick={()=> history.push('login')}>Логин</Button>}
  />
  )
}
