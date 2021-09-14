import React, { useState, useEffect } from 'react';
import { Pie } from '@ant-design/charts';

const SpecializationStatistic = ({specializations}) => {
  var data = [
    {
      type: '分类一',
      value: 27,
    },
    {
      type: '分类二',
      value: 25,
    },
    {
      type: '分类三',
      value: 18,
    },
    {
      type: '分类四',
      value: 15,
    },
    {
      type: '分类五',
      value: 10,
    },
    {
      type: '其他',
      value: 5,
    },
  ];
  var config = {
    appendPadding: 10,
    data: specializations ?? [],
    angleField: 'Count',
    colorField: 'Name',
    radius: 0.8,
    label: {
      type: 'outer',
      content: '{percentage}',
    },
    interactions: [{ type: 'pie-legend-active' }, { type: 'element-active' }],
  };
  return <Pie {...config} />;
};

export default SpecializationStatistic;