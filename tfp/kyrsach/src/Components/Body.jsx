import React from "react";
import { BodyMain } from "./BodyMain";

export const Body = ({ body, screenSize }) => {
  return (
    <div>
      {body === "Главная" ? (
        <BodyMain screenSize={screenSize}></BodyMain>
      ) : null}
    </div>
  );
};
