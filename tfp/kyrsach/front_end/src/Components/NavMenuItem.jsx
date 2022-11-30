import React from "react";
import {
  HomeOutlined,
  ShoppingOutlined,
  StockOutlined,
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
    case "Акции":
      return (
        <div>
          <StockOutlined />
          <span style={{ marginLeft: "6px" }}>Акции</span>
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
