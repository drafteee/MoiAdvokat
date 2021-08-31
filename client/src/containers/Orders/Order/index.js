import React, { useEffect } from 'react'
import { Col, Row, Typography } from 'antd'

import OrderCard from '../../../components/Orders/OrderCard'

import { useDispatch, useSelector } from 'react-redux'
import { orderActions } from '../store/actions'

import 'antd/lib/row/style/css'
import 'antd/lib/col/style/css'

const Order = props => {
  const id = props.match.params.id

  const { getOneOrderLoading, oneOrder } = useSelector(state => state.orderReducer)
  const { isRu } = useSelector(state => state.globalReducer)

  const dispatch = useDispatch()

  useEffect(() => {
    if (id) {
      dispatch(orderActions.getOrder({ id }))
    }
  }, [id])

  return (
    <>
      <Row>
        <Col xs={24}
          md={{
            span: 16,
            offset: 4
          }}
          lg={{
            span: 12,
            offset: 6
          }}
          xxl={{
            span: 8,
            offset: 8
          }}>
          <OrderCard
            order={oneOrder}
            orderLoading={getOneOrderLoading}
            isRu={isRu} />
        </Col>
      </Row>
    </>
  )
}

export default Order