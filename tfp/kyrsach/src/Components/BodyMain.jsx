import React from "react";
import { Layout, Typography } from "antd";
import { Col, Divider, Row } from "antd";
const { Content } = Layout;
const { Title, Paragraph, Text } = Typography;

export const BodyMain = ({ screenSize }) => {
  return (
    <div>
      <Content
        style={{
          padding: "0 50px",
        }}
      >
        <div className="site-layout-content">
          <Title>
            ТЕПЛЫЙ ПОЛ ЭЛЕКТРИЧЕСКИЙ И ТЕРМОРЕГУЛЯТОРЫ WÄRMEHAUS PREMIUM
          </Title>
          <Row wrap={screenSize >= 768 ? false : true}>
            <Col flex={screenSize >= 768 ? "200px" : "auto"}>
              {/* <Text strong>О НАШЕЙ ПРОДУКЦИИ:</Text> */}О НАШЕЙ ПРОДУКЦИИ:
            </Col>
            <Col flex="auto">
              {/* <Text> */}
              Рады представить Вам нашу продукцию : Мы предлагаем кабельные
              электрические системы обогрева, системы электрический теплый пол,
              для квартир, домов, коттеджей, терморегуляторы, системы обогрева
              трубопроводов, водостоков, крыш и кровель, открытых площадок,
              системы стаивания снега и льда на крышах, в водосточных и сливных
              трубах, дорожках, лестницах, террасах, балконах, пандусах,
              эстакадах и иных открытых площадках.
              {/* </Text> */}
            </Col>
          </Row>
        </div>
      </Content>
    </div>
  );
};
