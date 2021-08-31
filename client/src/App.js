import React, { useEffect } from "react";
import { useSelector } from "react-redux";
import { Router, useRouteMatch } from "react-router-dom";
import { Layout, Row, Col } from "antd";

import Routes from "./routes";
import history from "./helpers/history";
import Navigation from "./containers/Menu";
import Footer from "./containers/Footer";
const { Content } = Layout
const App = () => {

  useEffect(()=>{
    
  }, [])

  return (
    <Router history={history}>
      <Layout
        style={{
          minHeight: "100vh",
          maxHeight: "100%",
        }}
      >
        <Navigation />
        <Layout>
          <Content
            style={{
              padding: "24px 24px 0px 24px",
            }}
          >
            <Routes />

          </Content>
        </Layout>
        <Footer />
      </Layout>
    </Router>
  );
};

export default App;
