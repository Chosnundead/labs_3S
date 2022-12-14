import React from "react";
import styles from "./Marker.module.css";

export const Marker = () => {
  return (
    <div>
      <div className={`${styles.pin} ${styles.bounce}`}></div>
    </div>
  );
};
