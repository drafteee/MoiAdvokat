import React from 'react'
import { Alert, Avatar, Card, Skeleton, Typography } from 'antd'

import i18n from '../../../containers/Orders/Execute/localizations'

import 'antd/lib/card/style/css'
import 'antd/lib/skeleton/style/css'
import 'antd/lib/avatar/style/css'
import 'antd/lib/typography/style/css'
import 'antd/lib/alert/style/css'

import './style.css'

const { Text } = Typography

const OrderCard = ({ order, orderLoading, isRu }) => {
    return (
        <Card>
            <Skeleton
                avatar
                active={true}
                loading={orderLoading}>
                {order ? (
                    <>
                        <Card.Meta
                            avatar={
                                <Avatar size="large">{order.NameClient[0]}</Avatar>
                            }
                            title={`${order.NameClient} (${order.PhoneNumber})`}
                            description={`${order.Header} (${order.SpecializationsStr})`} />

                        <div className="order-card">
                            <Text>{`${order.Description}`}</Text>
                            <Text>{`До ${new Date(order.EndDueDate).toLocaleDateString()}`}</Text>
                            <Text>{`Цена: ${order.Price}`}</Text>
                            {order.FinishDate ? (
                                <>
                                    <Alert type="success" message={i18n.orderExecuted[isRu]} />
                                    <Text>{`Исполнено: ${new Date(order.FinishDate).toLocaleDateString()}`}</Text>
                                </>
                            ) : null}
                        </div>
                    </>
                ) : null}
            </Skeleton>
        </Card>
    )
}

export default OrderCard