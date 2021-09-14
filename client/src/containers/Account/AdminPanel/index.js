import React from 'react'
import { Tabs, Button, Row, Col } from 'antd';
import Lawyers from '../../Lawyers';
const { TabPane } = Tabs;

const AdminPanel = () => {
  return (
    <Tabs defaultActiveKey="1" tabPosition='left'>
      <TabPane tab="Адвокаты" key="1">
        <Row>
          <Col span={14}>
            <Lawyers/>
          </Col>
          <Col span={8}>
            <SubmitLawyer></SubmitLawyer>
          </Col>
        </Row>
      </TabPane>
      <TabPane tab="Заказы" key="2">
      </TabPane>
      <TabPane tab="Подписки" key="3">
      </TabPane>
    </Tabs>
  )
}

export default AdminPanel