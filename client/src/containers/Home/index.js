import React, { useCallback, useEffect } from "react";
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import { Form, Input, Button, DatePicker, Row, Col, Select, Card } from 'antd'
import { homeActions } from "./store/actions";
import LazyComponent from "../../components/LazyComponent";
import { homeLoadables } from "../../loadables";
import { useSpring, animated } from '@react-spring/web'
import './style.css'
import SpecializationStatistic from "./SpecializationStatistic";
import OrderStatistic from "./OrderStatistic";


const Home = () => {
  const dispatch = useDispatch();
  const statisticsObject = useSelector(state => state.homeReducer.statisticsObject)
  useEffect(() => {
    dispatch(homeActions.getStatistics())
  }, [])

  const { numberClients } = useSpring({
    from: { numberClients: 0 },
    to: {numberClients: statisticsObject ? statisticsObject.CountClients : 0},
    delay: 200,
  })

  const { numberLawyers } = useSpring({
    from: { numberLawyers: 0 },
    to: {numberLawyers: statisticsObject ? statisticsObject.CountLawyers : 0},
    delay: 200,
  })

  return(
    <>
    <Row className="numbers">
      <Col className='number-card'>
        <animated.div>{numberClients.to(n => n.toFixed(0))}</animated.div>
      </Col>
      <Col className='number-card'>
        <animated.div>{numberLawyers.to(n => n.toFixed(0))}</animated.div>
      </Col>
    </Row>
    <Row justify='center'>
      <Col span={8}>
        <Card title="Специализации">
        <SpecializationStatistic specializations={statisticsObject?.ListSpecialization}/>
        </Card>
        
      </Col>
      <Col offset={1} span={13}>
        <Card title="Количество заказов">
        <OrderStatistic orders={statisticsObject?.ListOrders}/>
        </Card>
      </Col>
    </Row>
    </>
  );
};

export default Home;
