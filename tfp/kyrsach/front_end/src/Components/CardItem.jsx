import React from "react";
import { Card } from "antd";
import sale from "../Images/sale.png";
const { Meta } = Card;

export const CardItem = ({ item }) => {
  return (
    <div>
      <img
        //!
        style={{ position: "absolute", zIndex: "1", display: "block" }}
        width={50}
        height={50}
        src={sale}
      ></img>
      <Card
        hoverable
        style={{
          width: 240,
        }}
        cover={<img alt="example" src={item.image} />}
      >
        <Meta
          title={
            <div>
              <div>{item.title}</div>
              {item.sale.is ? (
                <div>
                  <text
                    style={{
                      fontWeight: "300",
                      textDecoration: "line-through",
                      textDecorationThickness: "300",
                      color: "rgba(27, 27, 27, 0.88)",
                    }}
                  >
                    {(item.sale.amount * item.price + item.price).toFixed(2)}
                  </text>
                  <text
                    style={{
                      color: "rgba(228, 42, 42, 0.88)",
                    }}
                  >
                    {" "}
                    {item.price.toFixed(2)}$
                  </text>
                </div>
              ) : (
                <div
                  style={{
                    color: "rgba(27, 27, 27, 0.88)",
                  }}
                >
                  {item.price}$
                </div>
              )}
            </div>
          }
          description={
            <div>
              <div style={{ overflowWrap: "break-word" }}>
                {item.description}
              </div>
            </div>
          }
          style={{ justifyContent: "center" }}
        />
      </Card>
    </div>
  );
};
