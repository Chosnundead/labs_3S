import React from "react";
import {
  HomeOutlined,
  ShoppingOutlined,
  StockOutlined,
  ContactsOutlined,
} from "@ant-design/icons";

export const NavMenuItem = ({ item }) => {
  const navMenu = ["Главная", "Продукция", "Акции", "Контакты"];
  switch (item) {
    case "Главная":
      return (
        <div>
          <HomeOutlined />
          <span className="nav-menu-item-text">Главная</span>
        </div>
      );
    case "Продукция":
      return (
        <div>
          <ShoppingOutlined />
          <span className="nav-menu-item-text">Продукция</span>
        </div>
      );
    case "Акции":
      return (
        <div>
          <StockOutlined />
          <span className="nav-menu-item-text">Акции</span>
        </div>
      );
    case "Контакты":
      return (
        <div>
          <ContactsOutlined />
          <span className="nav-menu-item-text">Контакты</span>
        </div>
      );
  }
};
