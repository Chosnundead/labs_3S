import React from "react";
import styles from "./Cart.module.css";
import { ShoppingCartOutlined } from "@ant-design/icons";

export const Cart = ({ ...props }) => {
  return (
    <div {...props}>
      <div className={`${styles.cart}`}>
        <ShoppingCartOutlined style={{ color: "#fff", fontSize: 21 }} />
      </div>
    </div>
  );
};
