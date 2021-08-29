import React, {useState, useCallback, useEffect} from 'react'
import { Table, Tag, DatePicker, Modal, Button, Form, Input, InputNumber, notification  } from 'antd';
import DatePickerMulti from "react-multi-date-picker"
import DatePanel from "react-multi-date-picker/plugins/date_panel"
import {orderActions} from '../store/actions'
import { useSelector, shallowEqual, useDispatch } from "react-redux";

const layout = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 14,
  },
};

const RespondForm = ({isModalVisible, setIsModalVisible, currentId}) => {
  const [dates, setValues] = useState([])
  const respondOrderSuccess =  useSelector(state => state.orderReducer.respondOrderSuccess)

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  useEffect(() => {
    if(respondOrderSuccess){
      setIsModalVisible(false);
    }else{
      notification.error({
        message: 'Ошибка',
        description:
          'Произошла ошибка.'
      });
      setIsModalVisible(false);
    }
  }, [respondOrderSuccess])
  
  const dispatch = useDispatch();
  const respondOrder = useCallback(
    (params) => {
      dispatch(orderActions.respondOrder(params))
    }
    ,
    [dispatch],
  )

  const onFinish = (values) => {
    respondOrder({
      OrderId: currentId,
      Price: values.price,
      Dates: values.dates.reduce((accum, next)=> {
        return [
          ...accum,
          next.format()
        ]
      }, [])
    })
  };

  return (
    <>
      <Modal title="Отозваться на заказ" footer={null} visible={isModalVisible} onCancel={handleCancel} width={400}>
      <Form  name="respond-form" onFinish={onFinish} layout='horizontal'>
        <Form.Item
          name="price"
          label="Цена"
          rules={[
            {
              required: true,
            },
          ]}
        >
          <InputNumber
            defaultValue="1"
            min="0"
            max="10"
            step="0.000001"
            stringMode
          />
        </Form.Item>
        <Form.Item
          name="dates"
          label="Свободные даты"
          rules={[
            {
              required: true,
            },
          ]}
        >
          <DatePickerMulti 
      multiple
      value={dates} 
      onChange={setValues}
      plugins={[
        <DatePanel />
      ]}
    />
        </Form.Item>
        <Form.Item wrapperCol={{ ...layout.wrapperCol, offset: 8 }}>
          <Button type="primary" htmlType="submit">
            Отозваться
          </Button>
        </Form.Item>
      </Form>
      </Modal>
    </>
  )
}

export default RespondForm