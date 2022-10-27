import React from "react";
import { useState } from "react";
import { Layout } from "antd";
import { Body } from "../Components/Body";
import { Head } from "../Components/Head";
import { Foot } from "../Components/Foot";

export const MainPage = () => {
  const [body, setBody] = useState("Главная");
  const [screenSize, setScreenSize] = useState(
    document.documentElement.clientWidth
  );
  window.addEventListener("resize", (e) => {
    setScreenSize(document.documentElement.clientWidth);
  });
  return (
    <div>
      <Layout className="layout">
        <Head setBody={setBody}></Head>
        <div>
          {body}
          {screenSize}
        </div>
        <Body body={body} screenSize={screenSize}></Body>
        <Foot></Foot>
      </Layout>
    </div>
  );
};
