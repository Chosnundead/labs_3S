import React, { useContext } from "react";
import { Typography, Col, Row, Layout, Carousel } from "antd";
import { ScreenContext } from "../Utils/ScreenContext";
import background from "../Images/background.jpg";
import banner_0 from "../Images/banner_0.jpg";
import banner_1 from "../Images/banner_1.jpg";
import banner_2 from "../Images/banner_2.jpg";
import banner_3 from "../Images/banner_3.jpg";

const contentStyle = {
  height: "160px",
  color: "#fff",
  lineHeight: "160px",
  textAlign: "center",
  background: "#364d79",
  height: 333,
};
const { Content } = Layout;
const { Text } = Typography;

export const Main = () => {
  const { screenSize } = useContext(ScreenContext);
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
            <Text
              style={
                screenSize >= 768
                  ? { fontSize: "36px", fontWeight: "bold" }
                  : { fontSize: "24px", fontWeight: "550" }
              }
            >
              Теплый электрический пол и терморегуляторы WÄRMEHAUS PREMIUM и
              других лучших производителей.
            </Text>
            <Row
              wrap={screenSize >= 768 ? false : true}
              style={{ marginTop: "24px" }}
            >
              <Col flex={screenSize >= 768 ? "200px" : "auto"}>
                <Text strong>О нашей продукции:</Text>
              </Col>
              <Col flex="auto">
                <Text>
                  Рады представить Вам нашу продукцию: кабельные и электрические
                  системы обогрева(электрический теплый пол для квартир, домов,
                  коттеджей), терморегуляторы, системы обогрева трубопроводов,
                  водостоков, крыш, кровель и открытых площадок, системы
                  стаивания снега и льда на крышах, в водосточных и сливных
                  трубах, дорожках, лестницах, террасах, балконах, пандусах,
                  эстакадах и иных открытых площадках. Широкий выбор
                  нагревательных матов, нагревательных кабелей, инфракрасной
                  пленки, терморегуляторов и комплектующих от лучших европейских
                  и российских производителей!
                </Text>
              </Col>
            </Row>
            <Row style={{ marginTop: "24px" }} gutter={[24, 24]}>
              <Col span={8}>
                <Text strong>Почему нас выбирают</Text>
                <br></br>
                <Text>
                  Индивидуальный подход к клиенту. Мы небольшая фирма, которая
                  рассматривает свою величину как преимущество, благодаря
                  которому мы можем делать упор на Ваши индивидуальные
                  потребности и гибко на них реагировать. Опыт и
                  профессионализм. Мы осознаём, что каждый продукт отличается от
                  остальных, поэтому требует новых подходов и новых оригинальных
                  инженерных решений. Наш многолетний опыт гарантирует вам
                  уникальные результаты фильтрования. Качество без компромиссов.
                  Мы уделяем внимание каждой мелочи и стремимся достигнуть
                  простоты оборудования, чтобы предотвратить механические
                  поломки; и этот подход оправдывает себя. Этика и честность. На
                  службе общества - так мы понимаем нашу работу. Этому принципу
                  мы подчиняем.
                </Text>
              </Col>
              <Col span={8}>
                <Text strong>Гарантия качества</Text>
                <br></br>
                <Text>
                  Качество для нас выведено в абсолют. Мы считаем, что лучший
                  товар - надёжный товар. Исходя из этого мы представляем
                  сильные гарантии наших продуктов. Также мы предоставляем
                  возможность возврата продуктов, при соблюдении главных условий
                  эксплуатации. А главное, что для нас продажа некачественного
                  товара - главный страх. Мы продаем только надежный товар таких
                  стран как Чехия, Германия, Россия и другие.
                </Text>
              </Col>
              <Col span={8}>
                <Text strong>О компании</Text>
                <br></br>
                <Text>
                  Lorem ipsum dolor sit amet consectetur adipisicing elit.
                  Ratione quam eveniet, a eos facere perspiciatis eius, voluptas
                  voluptatem blanditiis id iusto rerum enim, accusantium in
                  maxime. Quidem, enim officiis aperiam doloribus inventore
                  totam ex, earum modi blanditiis minus aliquid. Natus id
                  nostrum illo harum consectetur corrupti nisi reiciendis
                  voluptates aliquid quae unde, iusto eius numquam. Ipsa eaque
                  ratione repudiandae cum, expedita non, consectetur neque
                  harum, velit impedit nostrum praesentium quibusdam ipsam illum
                  animi? Itaque saepe fugit incidunt dolor aperiam, pariatur
                  provident architecto velit assumenda, repellendus dolorem
                  delectus, esse sunt veritatis? Nam totam libero nemo in velit
                </Text>
              </Col>
            </Row>
          </div>
        </Content>
        <Content>
          <Row>
            <Col>
              <Carousel autoplay>
                <div>
                  <h3 style={contentStyle}>
                    <img
                      src={banner_0}
                      style={{
                        width: "-webkit-fill-available",
                        height: "-webkit-fill-available",
                      }}
                    ></img>
                  </h3>
                </div>
                <div>
                  <h3 style={contentStyle}>
                    <img
                      src={banner_1}
                      style={{
                        width: "-webkit-fill-available",
                        height: "-webkit-fill-available",
                      }}
                    ></img>
                  </h3>
                </div>
                <div>
                  <h3 style={contentStyle}>
                    <img
                      src={banner_2}
                      style={{
                        width: "-webkit-fill-available",
                        height: "-webkit-fill-available",
                      }}
                    ></img>
                  </h3>
                </div>
                <div>
                  <h3 style={contentStyle}>
                    <img
                      src={banner_3}
                      style={{
                        width: "-webkit-fill-available",
                        height: "-webkit-fill-available",
                      }}
                    ></img>
                  </h3>
                </div>
              </Carousel>
            </Col>
          </Row>
        </Content>
      </Layout>
    </div>
  );
};
