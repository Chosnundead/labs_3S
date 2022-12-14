import React from "react";
import sale from "../Images/sale.png";

export const DualImage = ({ ...props }) => {
  return (
    <div>
      <img {...props}></img>
      <img
        style={{
          position: "absolute",
          zIndex: "1",
          display: "block",
          top: "0",
          right: "0",
          marginTop: "-15px",
          marginRight: "-15px",
        }}
        width={50}
        height={50}
        src={sale}
      ></img>
    </div>
  );
};
