import React, {useState, useCallback, useEffect} from 'react'
import { Card, Col, Row, Result  } from 'antd';
import DatePickerMulti from "react-multi-date-picker"
import DatePanel from "react-multi-date-picker/plugins/date_panel"
import {orderActions} from '../store/actions'
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import 'antd/lib/card/style/index.css'
import 'antd/lib/result/style/index.css'

const ListOrderResponse = (params) => {
  console.log(params)
  const dispatch = useDispatch();
  const responses =  useSelector(state => state.orderReducer.orderResponses)
  const getAll = useCallback(
    (params) => {
      dispatch(orderActions.getOrderResponses(params))
    },
    [dispatch],
  )
  useEffect(() => {
    getAll({
      OrderId: params.match.params.orderId
    })
  }, [])

  return (
    <Row gutter={16}>
      {
        responses.length > 0 ? responses.map(item=> {
          return(
            <Col span={8}>
              <Card title={item.Lawyer.FirstName} bordered={false}>
                {item.Price}
              </Card>
            </Col>
          )
        }) :
        <Result
          title="Адвокаты не отозвались на ваш заказ!"
        />
      }
    </Row>
  )
}
export default ListOrderResponse