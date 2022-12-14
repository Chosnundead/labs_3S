import React, { useContext } from "react";
import GoogleMapReact from "google-map-react";
import { Col, Row, Image, Typography, Divider, Layout } from "antd";
import { Marker } from "../Components/Marker";
import { ScreenContext } from "../Utils/ScreenContext";
import { PhoneOutlined, MailOutlined, AimOutlined } from "@ant-design/icons";
import background from "../Images/background.jpg";
const { Text, Paragraph } = Typography;
const { Content } = Layout;

export const About = () => {
  const { screenSize } = useContext(ScreenContext);
  const map = {
    center: {
      lat: 52.064561,
      lng: 23.759156,
    },
    zoom: 16,
  };
  return (
    <div>
      <Layout
        style={{
          background: "rgba(0, 0, 0, 0.5)",
          backgroundImage: `url(${background})`,
          backgroundRepeat: "no-repeat",
          backgroundSize: "cover",
        }}
      >
        <Content
          style={{
            padding: screenSize >= 768 ? "50px 50px" : "0 0",
          }}
        >
          <div
            style={{
              borderRadius: screenSize >= 768 ? "13px" : "0px",
              minHeight: "280px",
              padding: "24px",
              background: "#fff",
            }}
          >
            <Row>
              <Col span={8} style={{ paddingRight: "24px" }}>
                <Text
                  style={
                    screenSize >= 768
                      ? { fontSize: "36px", fontWeight: "bold" }
                      : { fontSize: "24px", fontWeight: "550" }
                  }
                >
                  г. Брест
                </Text>
                <Divider style={{ margin: "6px 0px 6px 0px" }}></Divider>
                <div style={{ textAlign: "left" }}>
                  <Paragraph>
                    <AimOutlined />
                    <Text strong ellipsis={false} style={{ marginLeft: "6px" }}>
                      ул. Карьерная, д. 12, тц. Фиеста Вива
                    </Text>
                  </Paragraph>
                  <Paragraph>
                    <a
                      href="tel:80339185555"
                      style={{ textDecoration: "none", color: "black" }}
                    >
                      <PhoneOutlined />
                      <Text strong style={{ marginLeft: "6px" }}>
                        8 033 918-55-55
                      </Text>
                    </a>
                  </Paragraph>
                  <Paragraph>
                    <MailOutlined />
                    <Text strong style={{ marginLeft: "6px" }}>
                      info@warmehaus.by
                    </Text>
                  </Paragraph>
                </div>
              </Col>
              <Col span={16} style={{ width: "1920px", height: "540px" }}>
                <GoogleMapReact
                  bootstrapURLKeys={{
                    key: "AIzaSyDyKpps4G9RAJtkepBrbbkcuy7SnoVINNw",
                  }}
                  yesIWantToUseGoogleMapApiInternals
                  defaultCenter={map.center}
                  defaultZoom={map.zoom}
                >
                  <Marker lat={map.center.lat} lng={map.center.lng} />
                </GoogleMapReact>
              </Col>
            </Row>
          </div>
        </Content>
      </Layout>
    </div>
  );
};
