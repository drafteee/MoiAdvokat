import React from 'react'
import {
  Button, Tooltip, Popconfirm
} from 'antd'
import 'antd/lib/tooltip/style/index.css'
import "antd/lib/popover/style/index.css";
import './style.css'
import { CheckOutlined, CloseOutlined } from '@ant-design/icons';

const ApproveButtons = ({
  isRu, isApproveSending, approveFunc, id, ecp
}) => {

  return (
    <Button.Group
      className='approve_buttons_group'
      style={{
        width: '100%'
      }}>
      <Tooltip
        title={isRu ? 'Принять' : 'Прыняць'}
        placement='left'>
        <Popconfirm
          title={isRu ? "Прикрепить ЭЦП?" : "Прымацаваць ЭЛП?"}
          onConfirm={() => approveFunc(true, id, true)}
          onCancel={() => approveFunc(true, id, false)}
          okText={isRu ? "Да" : "Так"}
          cancelText={isRu ? "Нет" : "Не"}
        >
          <Button
            disabled={isApproveSending}
            className='button_Ok'
          //onClick={() => approveFunc(true, id)}
          >
            <CheckOutlined />
          </Button>
        </Popconfirm>
      </Tooltip>

      <Tooltip
        title={isRu ? 'Отклонить' : 'Адхіліць'}
        placement='right'>
        <Popconfirm
          title={isRu ? "Прикрепить ЭЦП?" : "Прымацаваць ЭЛП?"}
          onConfirm={() => approveFunc(false, id, true)}
          onCancel={() => approveFunc(false, id, false)}
          okText={isRu ? "Да" : "Так"}
          cancelText={isRu ? "Нет" : "Не"}
        >
          <Button
            disabled={isApproveSending}
            className='button_None'
            //onClick={() => approveFunc(false, id)}

          >
            <CloseOutlined />
          </Button>
        </Popconfirm>
      </Tooltip>

    </Button.Group>
  )
}
export default ApproveButtons