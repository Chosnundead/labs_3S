import React, { useContext } from "react";
import { Layout, Typography, Col } from "antd";
import { Content } from "antd/lib/layout/layout";
import { ScreenContext } from "../Utils/ScreenContext";
import background from "../Images/background.jpg";
const { Text, Paragraph } = Typography;

export const Error = () => {
  const { screenSize } = useContext(ScreenContext);
  return (
    <Layout
      style={{
        background: "rgba(0, 0, 0, 0.5)",
        backgroundImage: `url(${background})`,
        backgroundRepeat: "no-repeat",
        backgroundSize: "cover",
      }}
    >
      <Content
        style={{
          padding: screenSize >= 768 ? "50px 50px" : "0 0",
        }}
      >
        <Col
          style={{
            borderRadius: screenSize >= 768 ? "13px" : "0px",
            padding: "24px",
            background: "#fff",
            height: 590,
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <Text
            style={{
              fontSize: screenSize >= 768 ? "50px" : "40px",
              fontWeight: screenSize >= 768 ? "900" : "bold",
              zIndex: "1",
              color: "darkred",
            }}
          >
            Страница не найдена!
          </Text>
          <Text
            style={{
              fontSize: screenSize >= 768 ? "200px" : "166px",
              fontWeight: screenSize >= 768 ? "900" : "bold",
              position: "absolute",
              color: "lightgrey",
            }}
          >
            Ошибка 404!
          </Text>
        </Col>
      </Content>
    </Layout>
  );
};
