import React from "react";
import { Layout, Menu } from "antd";
import { useNavigate, useLocation } from "react-router-dom";
import { NavMenuItem } from "./NavMenuItem";
const { Header } = Layout;

export const Head = () => {
  const navMenu = ["Главная", "Продукция", "Статьи", "Контакты"];
  const location = useLocation();
  const navigate = useNavigate();
  return (
    <div>
      <Header>
        <div
          style={{
            float: "left",
            width: "120px",
            height: "31px",
            margin: "16px 24px 16px 0",
            background: "rgba(255, 255, 255, 0.3)",
          }}
        />
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={() => {
            if (location.pathname.includes("shop")) return ["2"];
            if (location.pathname.includes("posts")) return ["3"];
            if (location.pathname.includes("about")) return ["4"];
            return ["1"];
          }}
          items={navMenu.map((item, index) => {
            const key = index + 1;
            return {
              key,
              label: <NavMenuItem item={item} />,
            };
          })}
          onClick={(item) => {
            switch (navMenu[item.key - 1]) {
              case "Главная":
                navigate("");
                break;
              case "Продукция":
                navigate("/shop");
                break;
              case "Контакты":
                navigate("/about");
                break;
              case "Статьи":
                navigate("/posts");
                break;
              default:
                navigate("");
                break;
            }
          }}
        />
      </Header>
    </div>
  );
};
