import { Link } from "react-router-dom";
import { Menu } from "antd";

import React from "react";
import { Layout, Row, Col } from "antd";
import './style.css'
const { Footer } = Layout;

export default () => {
  return (
    <>
      <Footer className="footer">
        <nav>
          <Row justify="center">
            <Col className="menu-Col">
              <div className="foot-nav">
                <Menu selectable={false} mode="horizontal">
                  <Menu.Item>
                    <Link
                      style={{
                        color: "white",
                        padding: 40,
                      }}
                      to="#"
                    >
                      Test
                    </Link>
                  </Menu.Item>
                </Menu>
              </div>
            </Col>
          </Row>
        </nav>

        <Row justify="center">
          <a href="QuartetDangeonMaster" className="link-ipps">
            QuartetDangeonMaster
          </a>
        </Row>
      </Footer>
    </>
  );
};
