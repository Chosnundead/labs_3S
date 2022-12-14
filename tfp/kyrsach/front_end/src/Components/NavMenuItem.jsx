import React from "react";
import {
  HomeOutlined,
  ShoppingOutlined,
  FileOutlined,
  ContactsOutlined,
} from "@ant-design/icons";

export const NavMenuItem = ({ item }) => {
  switch (item) {
    case "Главная":
      return (
        <div>
          <HomeOutlined />
          <span style={{ marginLeft: "6px" }}>Главная</span>
        </div>
      );
    case "Продукция":
      return (
        <div>
          <ShoppingOutlined />
          <span style={{ marginLeft: "6px" }}>Продукция</span>
        </div>
      );
    case "Статьи":
      return (
        <div>
          <FileOutlined />
          <span style={{ marginLeft: "6px" }}>Статьи</span>
        </div>
      );
    case "Контакты":
      return (
        <div>
          <ContactsOutlined />
          <span style={{ marginLeft: "6px" }}>Контакты</span>
        </div>
      );
  }
};
