import React, { useContext } from "react";
import { Typography, Col, Row, Layout, Carousel } from "antd";
import { ScreenContext } from "../Utils/ScreenContext";
import background from "../Images/background.jpg";

const contentStyle = {
  height: "160px",
  color: "#fff",
  lineHeight: "160px",
  textAlign: "center",
  background: "#364d79",
};
const { Content } = Layout;
const { Text } = Typography;

export const Main = () => {
  const { screenSize } = useContext(ScreenContext);
  return (
    <div>
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
          <div
            style={{
              borderRadius: screenSize >= 768 ? "13px" : "0px",
              minHeight: "280px",
              padding: "24px",
              background: "#fff",
            }}
          >
            <Text
              style={
                screenSize >= 768
                  ? { fontSize: "36px", fontWeight: "bold" }
                  : { fontSize: "24px", fontWeight: "550" }
              }
            >
              Теплый электрический пол и терморегуляторы WÄRMEHAUS PREMIUM
            </Text>
            <Row
              wrap={screenSize >= 768 ? false : true}
              style={{ marginTop: "24px" }}
            >
              <Col flex={screenSize >= 768 ? "200px" : "auto"}>
                <Text strong>О нашей продукции:</Text>
              </Col>
              <Col flex="auto">
                <Text>
                  Рады представить Вам нашу продукцию: кабельные и электрические
                  системы обогрева(электрический теплый пол для квартир, домов,
                  коттеджей), терморегуляторы, системы обогрева трубопроводов,
                  водостоков, крыш, кровель и открытых площадок, системы
                  стаивания снега и льда на крышах, в водосточных и сливных
                  трубах, дорожках, лестницах, террасах, балконах, пандусах,
                  эстакадах и иных открытых площадках.
                </Text>
              </Col>
            </Row>
            <Row style={{ marginTop: "24px" }} gutter={[24, 24]}>
              <Col span={8}>
                <Text strong>Почему нас</Text>
                <br></br>
                <Text>
                  Lorem ipsum dolor sit, amet consectetur adipisicing elit. Iste
                  sequi adipisci odit, omnis necessitatibus voluptas. Doloribus,
                  nemo, doloremque iste atque libero nam vitae pariatur officiis
                  aliquid optio, distinctio tempora eius.
                </Text>
              </Col>
              <Col span={8}>
                <Text strong>Ебать мы лучшие</Text>
                <br></br>
                <Text>
                  Lorem ipsum dolor sit amet consectetur adipisicing elit. Nihil
                  itaque aut ad! Maxime accusantium quas illo veniam natus
                  nobis, autem dolores provident quae repellendus. Quod unde
                  numquam magnam. Sequi, pariatur.
                </Text>
              </Col>
              <Col span={8}>
                <Text strong>Заебись</Text>
                <br></br>
                <Text>
                  Lorem ipsum dolor sit amet consectetur adipisicing elit.
                  Pariatur, voluptas dolore. Similique maxime deserunt
                  voluptatem natus id perferendis, qui beatae sed, obcaecati
                  quisquam ea quis, corrupti nam possimus. Dolor, obcaecati?
                </Text>
              </Col>
            </Row>
          </div>
        </Content>
        <Content>
          <Row>
            <Col>
              <Carousel autoplay>
                <div>
                  <h3 style={contentStyle}>1</h3>
                </div>
                <div>
                  <h3 style={contentStyle}>2</h3>
                </div>
                <div>
                  <h3 style={contentStyle}>3</h3>
                </div>
                <div>
                  <h3 style={contentStyle}>4</h3>
                </div>
              </Carousel>
            </Col>
          </Row>
        </Content>
      </Layout>
    </div>
  );
};
