import React from "react";
import { Layout, Menu } from "antd";
import { NavMenuItem } from "./NavMenuItem";
const { Header } = Layout;

export const Head = ({ setBody }) => {
  const navMenu = ["Главная", "Продукция", "Акции", "Контакты"];
  return (
    <div>
      <Header>
        <div className="logo" />
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={["1"]}
          items={navMenu.map((item, index) => {
            const key = index + 1;
            return {
              key,
              label: <NavMenuItem item={item} />,
            };
          })}
          onClick={(item) => {
            setBody(navMenu[item.key - 1]);
          }}
        />
      </Header>
    </div>
  );
};
