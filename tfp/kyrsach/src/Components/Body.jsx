import React from "react";
import { BodyMain } from "./BodyMain";
import { BodyShop } from "./BodyShop";

export const Body = ({ body, screenSize }) => {
  return (
    <div>
      {body === "Главная" ? (
        <BodyMain screenSize={screenSize}></BodyMain>
      ) : null}
      {body === "Продукция" ? (
        <BodyShop screenSize={screenSize}></BodyShop>
      ) : null}
    </div>
  );
};
