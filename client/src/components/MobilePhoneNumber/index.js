import { Form, Input } from 'antd'
import React from 'react'

const MobilePhoneNumber = ({ name, errorMessage, placeholder }) => {
    return (
        <Form.Item
            name={name}
            label="Мобильный номер"
            rules={[{
                required: true,
                message: errorMessage,
                pattern: "^[0-9\-\+]{9,15}$"
            }]}>
            <Input placeholder={placeholder} />
        </Form.Item>
    )
}

export default MobilePhoneNumber