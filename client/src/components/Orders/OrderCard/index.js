import React from 'react'
import { Avatar, Card, Skeleton, Typography } from 'antd'

import 'antd/lib/card/style/css'
import 'antd/lib/skeleton/style/css'
import 'antd/lib/avatar/style/css'
import 'antd/lib/typography/style/css'

import './style.css'

const { Text } = Typography

const OrderCard = ({ order, orderLoading }) => {
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
                            <Text>{`${order.Description}\n`}</Text>
                            <Text>{`До ${new Date(order.EndDueDate).toLocaleDateString()}\n`}</Text>
                            <Text>{`Цена: ${order.Price}\n`}</Text>
                        </div>
                    </>
                ) : null}
            </Skeleton>
        </Card>
    )
}

export default OrderCard