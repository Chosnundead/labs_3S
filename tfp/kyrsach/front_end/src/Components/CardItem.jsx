import React from "react";
import { Card, Typography } from "antd";
import { DualImage } from "./DualImage";
const { Meta } = Card;
const { Text, Paragraph } = Typography;

export const CardItem = ({ item, ...props }) => {
  return (
    <div>
      <Card
        {...props}
        hoverable
        style={{
          width: 300,
        }}
        cover={
          item.sale.is ? (
            <DualImage
              alt="example"
              src={item.images[0]}
              style={{
                maxHeight: 300,
              }}
              width={300}
            ></DualImage>
          ) : (
            <img
              alt="example"
              src={item.images[0]}
              style={{
                maxHeight: 300,
              }}
              width={300}
            />
          )
        }
      >
        <Meta
          title={
            <div>
              <Paragraph ellipsis={true} style={{ marginBottom: "0px" }}>
                {item.title}
              </Paragraph>
              {item.sale.is ? (
                <Paragraph>
                  <Text
                    style={{
                      fontWeight: "300",
                      textDecoration: "line-through",
                      textDecorationThickness: "300",
                      color: "rgba(27, 27, 27, 0.88)",
                    }}
                  >
                    {(item.sale.amount * item.price + item.price).toFixed(2)}
                  </Text>
                  <Text
                    style={{
                      color: "rgba(228, 42, 42, 0.88)",
                    }}
                  >
                    {" "}
                    {item.price.toFixed(2)}BYN
                  </Text>
                </Paragraph>
              ) : (
                <Paragraph
                  style={{
                    color: "rgba(27, 27, 27, 0.88)",
                  }}
                >
                  {item.price}BYN
                </Paragraph>
              )}
            </div>
          }
          description={
            <div style={{ overflowWarp: "break-word" }}>{item.description}</div>
          }
          style={{ justifyContent: "center" }}
        />
      </Card>
    </div>
  );
};
