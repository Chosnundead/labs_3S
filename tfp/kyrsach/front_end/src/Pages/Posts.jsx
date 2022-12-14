import React, { useContext } from "react";
import { postsData } from "../Utils/db";
import { useNavigate } from "react-router-dom";
import { Content } from "antd/lib/layout/layout";
import { ScreenContext } from "../Utils/ScreenContext";
import background from "../Images/background.jpg";
import { Card, Layout, Typography, Col, Space } from "antd";
const { Meta } = Card;
const { Text, Paragraph } = Typography;

export const Posts = () => {
  const { screenSize } = useContext(ScreenContext);
  const redirect = useNavigate();
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
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <Space
              size={[16, 16]}
              wrap
              style={{ justifyContent: "space-evenly" }}
            >
              {postsData.map((item) => {
                return (
                  <Card
                    hoverable
                    style={{
                      width: 250,
                    }}
                    onClick={(e) => {
                      redirect(`/posts/${item.id}`);
                    }}
                    cover={<img alt="example" src={item.image} />}
                  >
                    <Meta
                      style={{ justifyContent: "center" }}
                      title={<Text>{item.title}</Text>}
                    />
                  </Card>
                );
              })}
            </Space>
          </Col>
        </Content>
      </Layout>
    </div>
  );
};
