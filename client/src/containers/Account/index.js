import React from 'react'
import { Tabs, Button } from 'antd';
import {
	Link
}from 'react-router-dom'
import 'antd/lib/tabs/style/index.css'
import AdminPanel from './AdminPanel';

const { TabPane } = Tabs;
const Account = () => {
  return (
    <Tabs defaultActiveKey="1" centered>
      <TabPane tab="Личная информация" key="1">
        <Link to="/orders">
          <Button>Подать на статус адвоката</Button>
        </Link>
      </TabPane>
      <TabPane tab="Заказы" key="2">
        <Link to="/orders">
          <Button>Открыть заказы</Button>
        </Link>
        <Link to="/lawyers">
          <Button>Открыть список адвокатов</Button>
        </Link>
      </TabPane>
      <TabPane tab="Отчёты" key="3">
        <Link to="/report">
          <Button>Открыть отчёты</Button>
        </Link>
      </TabPane>
      <TabPane tab="Панель администратора" key="4">
        <AdminPanel></AdminPanel>
      </TabPane>
    </Tabs>
  )
}

export default Account