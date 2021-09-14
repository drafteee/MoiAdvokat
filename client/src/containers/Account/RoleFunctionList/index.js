import React, { useEffect, useMemo, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { userActions } from "../../../store/actions/user";
import { Row, Col, Table, Button } from "antd";
import "antd/lib/table/style/index.css";
import "antd/lib/pagination/style/index.css";
import "antd/lib/checkbox/style/index.css";
import "antd/lib/radio/style/index.css";

const RoleFunctionList = () => {
  const [selectedRole, setSelectedRole] = useState(undefined);
  const [selectedFunctions, setSelectedFunctions] = useState([]);
  const { rolesVM, functionsVM } = useSelector((state) => state.userReducer);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(userActions.getRoles());
    dispatch(userActions.getFunctions());
  }, []);

  useEffect(() => {
    if (rolesVM.length) {
      const role = rolesVM.find((val) => val.Id === selectedRole);
      const roleFunctions = role.Functions.map((val) => val.Id);
      setSelectedFunctions(roleFunctions);
    }
  }, [selectedRole]);

  const roles = useMemo(() => {
    return rolesVM.map((item) => {
      return {
        name: item.Name,
        key: item.Id,
      };
    });
  }, [rolesVM]);

  const functions = useMemo(() => {
    return functionsVM.map((item) => {
      return {
        name: item.Name,
        description: item.Description,
        key: item.Id,
      };
    });
  }, [functionsVM]);

  const roleColumns = [
    {
      title: "RoleName",
      dataIndex: "name",
      render: (text) => <a>{text}</a>,
    },
  ];
  const functionColums = [
    {
      title: "FunctionName",
      dataIndex: "name",
      render: (text) => <a>{text}</a>,
    },
    {
      title: "Description",
      dataIndex: "description",
      render: (text) => <a>{text}</a>,
    },
  ];

  const roleSelection = {
    onChange: (e) => setSelectedRole(e[0]),
  };

  const funcSelected = {
    selectedRowKeys: selectedFunctions,
    onChange: (e) => {
      rolesVM.map((val) => {
        if (val.Id === selectedRole) {
          val.Functions = functionsVM.filter((val) => {
            return e.includes(val.Id);
          });
        }
      });
      console.log(rolesVM);
      setSelectedFunctions(e);
    },
  };

  const handleSave = () => {
    dispatch(userActions.updateRoleFunctions(rolesVM));
  };

  return (
    <>
      <Row justify="space-between" align="top" gutter={16}>
        <Col span={10}>
          <Table
            rowSelection={{
              type: "radio",
              ...roleSelection,
            }}
            columns={roleColumns}
            dataSource={roles}
          />
        </Col>

        <Col>
          {selectedRole && (
            <Table
              rowSelection={{
                type: "checkbox",
                ...funcSelected,
              }}
              columns={functionColums}
              dataSource={functions}
            />
          )}
        </Col>
      </Row>
      <Button onClick={handleSave}>Save</Button>
    </>
  );
};

export default RoleFunctionList;
