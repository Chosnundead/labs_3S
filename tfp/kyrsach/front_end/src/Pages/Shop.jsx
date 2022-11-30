import React, { useContext, useState } from "react";
import { Col, Row, Space, Menu, Card } from "antd";
import Input from "antd/lib/input/Input";
import { Content } from "antd/lib/layout/layout";
import {
  AppstoreOutlined,
  ContainerOutlined,
  DesktopOutlined,
  MailOutlined,
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  PieChartOutlined,
} from "@ant-design/icons";
import { shopData } from "../Utils/db";
import { ScreenContext } from "../Utils/ScreenContext";
import { CardItem } from "../Components/CardItem";

const { Meta } = Card;
function getItem(label, key, icon, children, type) {
  return {
    key,
    icon,
    children,
    label,
    type,
  };
}

export const Shop = () => {
  const { screenSize } = useContext(ScreenContext);
  const items = [
    getItem("Option 1", "1", <PieChartOutlined />),
    getItem("Option 2", "2", <DesktopOutlined />),
    getItem("Option 3", "3", <ContainerOutlined />),
    getItem("Navigation One", "sub1", <MailOutlined />, [
      getItem("Option 5", "5"),
      getItem("Option 6", "6"),
      getItem("Option 7", "7"),
      getItem("Option 8", "8"),
    ]),
    getItem("Navigation Two", "sub2", <AppstoreOutlined />, [
      getItem("Option 9", "9"),
      getItem("Option 10", "10"),
      getItem("Submenu", "sub3", null, [
        getItem("Option 11", "11"),
        getItem("Option 12", "12"),
      ]),
    ]),
  ];
  const [input, setInput] = useState("");
  return (
    <div>
      <Content
        style={{
          padding: screenSize >= 768 ? "50px 50px" : "0 0",
        }}
      >
        <div
          style={{
            borderRadius: screenSize >= 768 ? "13px" : "0px",
            minHeight: "280px",
            padding: "24px",
            background: "#fff",
          }}
        >
          <Row style={{ justifyContent: "center" }}>
            <Col flex={screenSize >= 768 ? "200px" : "auto"}></Col>
            <Col flex="auto">
              <Input
                style={{ width: "300px" }}
                placeholder="Поиск..."
                value={input}
                onChange={(e) => {
                  setInput(e.target.value);
                }}
              ></Input>
            </Col>
          </Row>
          <Row style={{ marginTop: "24px", flexWrap: "nowrap" }}>
            <Col flex={screenSize >= 768 ? "200px" : "auto"}>
              <Menu
                defaultSelectedKeys={["1"]}
                defaultOpenKeys={["sub1"]}
                mode="inline"
                theme="white"
                inlineCollapsed={false}
                items={items}
              />
            </Col>
            <Col flex="auto" style={{ padding: "24px" }}>
              <Space
                size={[8, 16]}
                wrap
                style={{ justifyContent: "space-evenly" }}
              >
                {shopData.map((item) => {
                  return <CardItem item={item}></CardItem>;
                })}
                <Card
                  hoverable
                  style={{
                    width: 240,
                  }}
                  cover={
                    <img
                      alt="example"
                      src="https://os.alipayobjects.com/rmsportal/QBnOOoLaAfKPirc.png"
                    />
                  }
                >
                  <Meta
                    title="Europe Street beat"
                    description="www.instagram.com"
                  />
                </Card>
              </Space>
            </Col>
          </Row>
        </div>
      </Content>
    </div>
  );
};
