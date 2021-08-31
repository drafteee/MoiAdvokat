import React, { memo, useState, useEffect } from "react";
import { Link, useRouteMatch } from "react-router-dom";
import { Layout, Menu, Row, Col, Button, Badge } from "antd";
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import {
  LogoutOutlined
} from '@ant-design/icons';
import {
	userActions
}from "../../store/actions"
import './style.css'
import history from "../../helpers/history";
const { Header } = Layout;

const urls = {
  null: ["1"],
  "/account": ["2"],
  "/lawyers": ["3"]
}

const Navigation = memo(() => {
  const dispatch = useDispatch()
  const user = useSelector(state=> state.userReducer.user)
  const location = useRouteMatch("/:slug");
  const isHome = location === null;
  console.log(isHome, urls[location], location)
  return (
    <Header className="header">
      <div className="logo" onClick={()=> history.push(user ? 'account' : '/login')}>
        <span>{user ? 'Кабинет' : 'Логин'}</span>
        <LogoutOutlined onClick={(e) => {
          e.stopPropagation()
          dispatch(userActions.logout())
        }
        }/>
      </div>
      <Menu theme="dark" mode="horizontal" selectedKeys={!isHome ? urls[location.url] : ["1"]}>
        <Menu.Item key="1"><Link to="/">Домой</Link></Menu.Item>
        <Menu.Item key="2"><Link to="/account">Аккаунт</Link></Menu.Item>
        <Menu.Item key="3"><Link to="/lawyers">Адвокаты</Link></Menu.Item>
      </Menu>
    </Header>
  );
});

export default Navigation;
