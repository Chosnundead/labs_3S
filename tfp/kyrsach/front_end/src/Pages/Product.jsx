import React, { useContext } from "react";
import { Col, Row, Image, Typography, Divider, Layout } from "antd";
import { Content } from "antd/lib/layout/layout";
import { useParams } from "react-router-dom";
import { ScreenContext } from "../Utils/ScreenContext";
import { MyCarousel } from "../Components/MyCarousel";
import { shopData } from "../Utils/db";
import background from "../Images/background.jpg";
const { Text, Paragraph } = Typography;

export const Product = () => {
  const { screenSize } = useContext(ScreenContext);
  const params = useParams();
  const item = shopData.filter((i) => {
    return params.id == i.id;
  })[0];
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
            <Row gutter={[16, 16]}>
              <Col span={12}>
                <MyCarousel id={params.id} />
              </Col>
              <Col span={12} style={{ textAlign: "left" }}>
                <Paragraph
                  style={
                    screenSize >= 768
                      ? { fontSize: "36px", fontWeight: "bold" }
                      : { fontSize: "24px", fontWeight: "550" }
                  }
                >
                  {item.title}
                </Paragraph>
                <div
                  style={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignContent: "center",
                    alignItems: "center",
                  }}
                >
                  {item.sale.is ? (
                    <Paragraph
                      style={{
                        marginBottom: 0,
                      }}
                    >
                      <Text
                        style={{
                          fontSize: "24px",
                          fontWeight: "300",
                          textDecoration: "line-through",
                          textDecorationThickness: "300",
                          color: "rgba(27, 27, 27, 0.88)",
                        }}
                      >
                        {(item.sale.amount * item.price + item.price).toFixed(
                          2
                        )}
                      </Text>
                      <Text
                        style={{
                          fontSize: "24px",
                          color: "rgba(228, 42, 42, 0.88)",
                          marginLeft: "24px",
                        }}
                      >
                        {item.price.toFixed(2)}BYN
                      </Text>
                    </Paragraph>
                  ) : (
                    <Paragraph
                      style={{
                        fontSize: "24px",
                        color: "rgba(27, 27, 27, 0.88)",
                        marginBottom: 0,
                      }}
                    >
                      {item.price}BYN
                    </Paragraph>
                  )}
                  <Image src={item.manufacturer.image} height={40}></Image>
                </div>
                <Divider orientation="left" orientationMargin="0">
                  Краткое описание
                </Divider>
                <Text style={{ fontSize: "15px" }}>{item.description}</Text>
                {item.info.map((i, index) => {
                  return (
                    <div key={index}>
                      <Divider style={{ margin: "6px 0px 6px 0px" }}></Divider>
                      <div
                        style={{
                          display: "flex",
                          justifyContent: "space-between",
                          alignContent: "center",
                          alignItems: "center",
                        }}
                      >
                        <Text strong={true} style={{ fontSize: "16px" }}>
                          {i.title}
                        </Text>
                        <Text style={{ fontSize: "15px" }}>
                          {i.description}
                        </Text>
                      </div>
                    </div>
                  );
                })}
              </Col>
            </Row>
            <Row>
              <Col>
                <Divider></Divider>
                <Paragraph
                  style={{
                    marginBottom: "24px",
                    fontSize: "24px",
                    fontWeight: "500",
                  }}
                >
                  Описание
                </Paragraph>
                <Paragraph style={{ fontSize: "15px" }}>
                  {item.fullDescription}
                </Paragraph>
              </Col>
            </Row>
          </div>
        </Content>
      </Layout>
    </div>
  );
};
