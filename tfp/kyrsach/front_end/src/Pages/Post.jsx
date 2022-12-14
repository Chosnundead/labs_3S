import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { postsData } from "../Utils/db";
import React, { useContext } from "react";
import { Content } from "antd/lib/layout/layout";
import { ScreenContext } from "../Utils/ScreenContext";
import background from "../Images/background.jpg";
import { Card, Layout, Typography, Col, Space, Row } from "antd";
const { Meta } = Card;
const { Text, Paragraph } = Typography;

export const Post = () => {
  const { screenSize } = useContext(ScreenContext);
  const params = useParams();
  const item = postsData.filter((i) => {
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
          <Col
            style={{
              borderRadius: screenSize >= 768 ? "13px" : "0px",
              padding: "24px",
              background: "#fff",
              height: 590,
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
              alignContent: "center",
            }}
          >
            <Paragraph>
              <Text
                style={
                  screenSize >= 768
                    ? { fontSize: "36px", fontWeight: "bold" }
                    : { fontSize: "24px", fontWeight: "550" }
                }
              >
                {item.title}
              </Text>
            </Paragraph>
            <Paragraph>
              <Text strong>{item.description}</Text>
            </Paragraph>
          </Col>
        </Content>
      </Layout>
    </div>
  );
};
