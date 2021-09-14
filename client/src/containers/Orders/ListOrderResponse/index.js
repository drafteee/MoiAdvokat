import React, {useState, useCallback, useEffect} from 'react'
import { Card, Col, Row, Result, Button  } from 'antd';
import DatePickerMulti from "react-multi-date-picker"
import DatePanel from "react-multi-date-picker/plugins/date_panel"
import {orderActions} from '../store/actions'
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import { Calendar } from "react-multi-date-picker"
import 'antd/lib/card/style/index.css'
import 'antd/lib/result/style/index.css'
import './style.css'

const ListOrderResponse = (params) => {
  const dispatch = useDispatch();
  const responses =  useSelector(state => state.orderReducer.orderResponses)
  const chooseItem =  useSelector(state => state.orderReducer.orderResponses.find(x=> x.IsChoosen))
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

  const chooseLawyer = (lawyerId, id) => {
    dispatch(orderActions.chooseLawyer({
      Id: id,
      LawyerId: lawyerId,
      OrderId: params.match.params.orderId
    }))
  }

  return (
    <Row gutter={16}>
      {
        responses.length > 0 ? responses.map(item=> {
          return(
            <Col span={8}>
              <Card title={item.Lawyer.FirstName} 
              extra={
                !chooseItem && <Button onClick={()=> chooseLawyer(item.LawyerId, item.Id)}>Выбрать</Button>
                ||
                chooseItem.LawyerId == item.LawyerId && <Button disabled={item.IsChoosen} onClick={()=> chooseLawyer(item.LawyerId, item.Id)}>Выбрать</Button>
              }
              bordered={false}
              >
                <Row justify='space-around' align='middle' className="row-response">
                  <Col>
                    <span>Цена: {item.Price}</span>
                  </Col>
                  <Col>
                    <Calendar disabled value={item.Dates.map(date=> new Date(date))} multiple />
                  </Col>
                </Row>
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