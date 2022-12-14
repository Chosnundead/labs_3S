import React from "react";
import { Image } from "antd";
import { shopData } from "../Utils/db";
import Carousel from "better-react-carousel";

export const MyCarousel = ({ id, ...params }) => {
  const item = shopData.filter((i) => {
    return id == i.id;
  })[0];
  return (
    <div {...params}>
      <Carousel cols={1} rows={1} gap={10} loop>
        {item.images.map((i, index) => {
          return (
            <Carousel.Item key={index}>
              <Image src={i} width={640} style={{ height: 640 }} />
            </Carousel.Item>
          );
        })}
      </Carousel>
    </div>
  );
};
