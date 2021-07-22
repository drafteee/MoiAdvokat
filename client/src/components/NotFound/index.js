import React from "react";
import { Button, Row } from "antd";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import i18n from "./localization";

const NotFound = () => {
  const isRu = useSelector(state => state.globalReducer.isRu);
  return (
    <>
      <Row
        type="flex"
        justify="center"
        align="middle"
        style={{
          marginBottom: "2%",
        }}
      />
      <Row
        type="flex"
        justify="center"
        align="middle"
        style={{
          marginBottom: "1%",
        }}
      >
        <h3>{i18n.header[isRu]}</h3>
      </Row>
      <Row
        type="flex"
        justify="center"
        align="middle"
        style={{
          marginBottom: "2%",
        }}
      >
        <p
          style={{
            fontSize: "1.1em",
          }}
        >
          {i18n.message[isRu]}
        </p>
      </Row>
      <Row type="flex" justify="center" align="middle">
        <Link to="/">
          <Button type="primary">{i18n.back[isRu]}</Button>
        </Link>
      </Row>
    </>
  );
};

export default NotFound;
