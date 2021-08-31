import React, { useState, useEffect } from 'react';
import { Line } from '@ant-design/charts';

const OrderStatistic = ({orders}) => {
  var config = {
    data: orders ?? [],
    padding: 'auto',
    xField: 'Date',
    yField: 'Count',
    xAxis: { tickCount: 5 }
  };
  return <Line {...config} />;
};

export default OrderStatistic;