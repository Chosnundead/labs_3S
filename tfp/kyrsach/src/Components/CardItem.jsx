import React from "react";
import { Card } from "antd";
const { Meta } = Card;

export const CardItem = ({ item }) => {
  return (
    <Card
      hoverable
      style={{
        width: 240,
      }}
      cover={<img alt="example" src={item.image} />}
    >
      <Meta
        title={item.title}
        description={item.description}
        style={{ justifyContent: "center" }}
      />
    </Card>
  );
};
