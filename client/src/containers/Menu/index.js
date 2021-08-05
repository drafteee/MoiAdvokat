import React, { memo, useState, useEffect } from "react";
import { Link, useRouteMatch } from "react-router-dom";
import { Layout, Menu, Row, Col, Button, Badge } from "antd";
const { Header } = Layout;

const Navigation = memo(() => {
  const location = useRouteMatch("/:slug");
  const isHome = location === null;

  return (
    <Header
      className={isHome ? "header" : ""}
      style={{
        padding: "0px",
      }}
    >
      dsadsad11
      <Row>
        <Col span={7} className="logo-title">
          <Link to="/">
            {/* <img
              decoding="async"
              loading="lazy"
              className="logo"
              src={logo}
              alt="fds"
              // onLoad={() => setIsLoadedLogo(true)}
            /> */}
            <div>{"TEST LINK"}</div>
          </Link>
        </Col>
      </Row>
    </Header>
  );
});

export default Navigation;
