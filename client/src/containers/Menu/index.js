import React, { memo, useState, useEffect } from "react";
import { Link, useRouteMatch } from "react-router-dom";
import { Layout, Menu, Row, Col, Button, Badge } from "antd";
import './style.css'
const { Header } = Layout;

const Navigation = memo(() => {
  const location = useRouteMatch("/:slug");
  const isHome = location === null;

  return (
    <Header className="header">
      <div className="logo" />
      <Menu theme="dark" mode="horizontal" defaultSelectedKeys={["1"]}>
        <Menu.Item key="1"><Link to="/">Домой</Link></Menu.Item>
        <Menu.Item key="2"><Link to="/account">Аккаунт</Link></Menu.Item>
        <Menu.Item key="3"><Link to="/lawyers">Адвокаты</Link></Menu.Item>
      </Menu>
    </Header>
  );
});

export default Navigation;
