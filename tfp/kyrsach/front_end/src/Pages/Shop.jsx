import React, { useContext, useState } from "react";
import {
  Col,
  Row,
  Space,
  Menu,
  Card,
  Slider,
  Typography,
  Checkbox,
  Select,
  Cascader,
  Modal,
  Layout,
  InputNumber,
} from "antd";
import Input from "antd/lib/input/Input";
import { Content } from "antd/lib/layout/layout";
import { useNavigate } from "react-router-dom";
import {
  AppstoreOutlined,
  TeamOutlined,
  MailOutlined,
  WalletOutlined,
  ShoppingCartOutlined,
} from "@ant-design/icons";
import { shopData } from "../Utils/db";
import { ScreenContext } from "../Utils/ScreenContext";
import { CardItem } from "../Components/CardItem";
import { Cart } from "../Components/Cart";
import background from "../Images/background.jpg";
const { Text, Paragraph } = Typography;
const { Option } = Select;

const { Meta } = Card;
function getItem(label, key, icon, children, type) {
  return {
    key,
    icon,
    children,
    label,
    type,
  };
}

export const Shop = () => {
  const { screenSize } = useContext(ScreenContext);
  const minMaxArr = [
    ...shopData.map((item) => {
      return item.price;
    }),
  ];
  const manufacturerArr = [
    ...shopData.map((item) => {
      return item.manufacturer.title;
    }),
  ];
  const infoNameArr = [].concat(
    ...shopData.map((item) => {
      return item.info.map((itemIn) => {
        return itemIn.title;
      });
    })
  );
  const infoArr = {};
  infoNameArr
    .filter(function (item, pos) {
      return infoNameArr.indexOf(item) == pos;
    })
    .forEach((item) => {
      infoArr[item] = [];
    });
  shopData.forEach((item) => {
    item.info.forEach(({ title, description }) => {
      if (infoArr[title].indexOf(description) == -1) {
        infoArr[title].push(description);
      }
    });
  });
  const infoSortedArr = [];
  Object.keys(infoArr).forEach(function (key) {
    // console.log(key);
    infoSortedArr.push({
      label: key,
      value: key,
      children: infoArr[key].map((item) => {
        return { label: item, value: item };
      }),
    });
  });
  const [minMax, setMinMax] = useState([
    Math.min(...minMaxArr),
    Math.max(...minMaxArr),
  ]);
  const [cart, setCart] = useState({});
  const [sort, setSort] = useState({
    minPrice: minMax[0],
    maxPrice: minMax[1],
    isSale: false,
    manufacturer: [],
    info: [],
  });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const items = [
    getItem(
      <div>
        <Cascader
          style={{
            width: "100%",
          }}
          options={infoSortedArr}
          onChange={(e) => {
            setSort({ ...sort, info: e });
          }}
          multiple
          maxTagCount="responsive"
        />
      </div>,
      "1",
      <Text>
        <WalletOutlined style={{ paddingRight: 10 }} />
        Описание
      </Text>
    ),
    getItem(
      <div>
        <Slider
          range
          min={minMax[0]}
          max={minMax[1]}
          defaultValue={[minMax[0], minMax[1]]}
          onChange={(value) => {
            setSort({ ...sort, minPrice: value[0], maxPrice: value[1] });
          }}
        />
      </div>,
      "2",
      <Text>
        <WalletOutlined style={{ paddingRight: 10 }} />
        Цена
      </Text>
    ),
    getItem(
      <div>
        <Checkbox
          onChange={(item) => {
            setSort({ ...sort, isSale: item.target.checked });
          }}
        ></Checkbox>
      </div>,
      "3",
      <Text>
        <WalletOutlined style={{ paddingRight: 10 }} />
        Скидка
      </Text>
    ),
    getItem(
      <div>
        <Select
          mode="multiple"
          style={{ width: "100%" }}
          placeholder="Поиск..."
          onChange={(item) => {
            setSort({ ...sort, manufacturer: item });
          }}
          // optionLabelProp="label"
          maxTagCount="responsive"
        >
          {manufacturerArr
            .filter(function (item, pos) {
              return manufacturerArr.indexOf(item) == pos;
            })
            .map((item) => {
              return <Option value={item} label={item}></Option>;
            })}
        </Select>
      </div>,
      "4",
      <Text>
        <TeamOutlined style={{ paddingRight: 10 }} />
        Производитель
      </Text>
    ),
  ];
  const [input, setInput] = useState("");
  const redirect = useNavigate();
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
            <Cart
              onClick={(e) => {
                setIsModalOpen(true);
              }}
            ></Cart>
            <Modal
              title="Корзина товаров"
              open={isModalOpen}
              onCancel={() => {
                setIsModalOpen(false);
              }}
              onOk={() => {
                setIsModalOpen(false);
              }}
            >
              {/* <p>Some contents...</p>
        <p>Some contents...</p>
        <p>Some contents...</p> */}
              {/* {cart.map((item, index) => {
          return (
            <p>
              {item} : {cart[item]}
            </p>
          );
        })} */}
              {Object.keys(cart).length == 0 ? (
                <Paragraph
                  strong
                  style={{ marginBottom: 0, textAlign: "center" }}
                >
                  Пусто
                </Paragraph>
              ) : (
                <div>
                  {Object.getOwnPropertyNames(cart)
                    .map(function (e) {
                      return [e, cart[e]];
                    })
                    .map((item) => {
                      return (
                        <Paragraph>
                          <Paragraph strong style={{ marginBottom: 0 }}>
                            {shopData[item[0]].title}
                          </Paragraph>
                          <Paragraph style={{ marginBottom: 0 }}>
                            Количество: {item[1]}
                          </Paragraph>
                          <Paragraph style={{ marginBottom: 0 }}>
                            Цена: {shopData[item[0]].price * item[1]}BYN
                          </Paragraph>
                        </Paragraph>
                      );
                    })}
                  <Paragraph>
                    <Paragraph strong style={{ marginBottom: 0 }}>
                      Итого:
                    </Paragraph>
                    <Paragraph style={{ marginBottom: 0 }}>
                      Количество:{" "}
                      {Object.getOwnPropertyNames(cart)
                        .map(function (e) {
                          return cart[e];
                        })
                        .reduce((sum, a) => sum + a)}
                    </Paragraph>
                    <Paragraph style={{ marginBottom: 0 }}>
                      Цена:{" "}
                      {Object.getOwnPropertyNames(cart)
                        .map(function (e) {
                          return shopData[e].price * cart[e];
                        })
                        .reduce((sum, a) => sum + a)}
                      BYN
                    </Paragraph>
                  </Paragraph>
                </div>
              )}
              {/* {Object.getOwnPropertyNames(cart)
          .map(function (e) {
            return [e, cart[e]];
          })
          .map((item) => {
            return (
              <Paragraph>
                <Paragraph strong style={{ marginBottom: 0 }}>
                  {shopData[item[0]].title}
                </Paragraph>
                <Paragraph style={{ marginBottom: 0 }}>
                  Количество: {item[1]}
                </Paragraph>
                <Paragraph style={{ marginBottom: 0 }}>
                  Цена: {shopData[item[0]].price * item[1]}$
                </Paragraph>
              </Paragraph>
            );
          })}
        <Paragraph>
          <Paragraph strong style={{ marginBottom: 0 }}>
            Итого:
          </Paragraph>
          <Paragraph style={{ marginBottom: 0 }}>
            Количество:{" "}
            {Object.getOwnPropertyNames(cart)
              .map(function (e) {
                return cart[e];
              })
              .reduce((sum, a) => sum + a)}
          </Paragraph>
          <Paragraph style={{ marginBottom: 0 }}>
            Цена:{" "}
            {Object.getOwnPropertyNames(cart)
              .map(function (e) {
                return shopData[e].price * cart[e];
              })
              .reduce((sum, a) => sum + a)}
            $
          </Paragraph>
        </Paragraph> */}
            </Modal>

            <div
              style={{
                borderRadius: screenSize >= 768 ? "13px" : "0px",
                minHeight: "280px",
                padding: "24px",
                background: "#fff",
              }}
            >
              <Row style={{ justifyContent: "center" }}>
                <Col flex={screenSize >= 768 ? "200px" : "auto"}></Col>
                <Col flex="auto">
                  <Input
                    style={{ width: 320 }}
                    placeholder="Поиск..."
                    value={input}
                    onChange={(e) => {
                      setInput(e.target.value);
                    }}
                  ></Input>
                </Col>
              </Row>
              <Row style={{ marginTop: "24px", flexWrap: "nowrap" }}>
                <Col flex={screenSize >= 768 ? "300px" : "auto"}>
                  <Menu
                    defaultSelectedKeys={["1"]}
                    defaultOpenKeys={["sub1"]}
                    mode="inline"
                    theme="white"
                    inlineCollapsed={false}
                    items={items}
                  />
                </Col>
                <Col flex="auto" style={{ padding: "24px" }}>
                  <Space
                    size={[16, 16]}
                    wrap
                    style={{ justifyContent: "space-evenly" }}
                  >
                    {shopData
                      .filter((item) => {
                        return (
                          item.title
                            .toLowerCase()
                            .includes(input.toLowerCase()) &&
                          item.price >= sort.minPrice &&
                          item.price <= sort.maxPrice &&
                          (item.sale.is == sort.isSale || !sort.isSale) &&
                          (sort.manufacturer.length == 0 ||
                            sort.manufacturer.indexOf(
                              item.manufacturer.title
                            ) != -1) &&
                          (sort.info.length == 0 ||
                            sort.info.every((i) => {
                              if (i.length == 1) {
                                return item.info.some((j) => {
                                  return j.title == i[0];
                                });
                              } else {
                                return item.info.some((j) => {
                                  return sort.info.some((k) => {
                                    return (
                                      j.title == k[0] && j.description == k[1]
                                    );
                                  });
                                });
                              }
                            }))
                        );
                      })
                      .map((item) => {
                        return (
                          <CardItem
                            key={item.id}
                            // onClick={() => {
                            //   redirect(`/shop/${item.id}`);
                            // }}
                            item={item}
                            actions={[
                              <div
                                style={{
                                  display: "flex",
                                  justifyContent: "center",
                                  alignContent: "center",
                                  alignItems: "center",
                                  height: 30,
                                }}
                              >
                                <ShoppingCartOutlined
                                  key="addToCart"
                                  style={{ paddingRight: 10 }}
                                  // onClick={() => {
                                  //   setCart({
                                  //     ...cart,
                                  //     [item.id]: Number(
                                  //       document.getElementById(item.id).value
                                  //     ),
                                  //   });
                                  //   // setCart({
                                  //   //   ...cart,
                                  //   //   [item.id]: document.getElementById(item.id)
                                  //   //     .value,
                                  //   // });
                                  // }}
                                />
                                <InputNumber
                                  style={{ height: 30 }}
                                  // id={item.id}
                                  min={0}
                                  defaultValue={0}
                                  // value={cart[item.id].onInput}
                                  onChange={(e) => {
                                    if (e == 0) {
                                      let newCart = {};
                                      for (const [key, value] of Object.entries(
                                        cart
                                      )) {
                                        if (item.id != key) {
                                          newCart[key] = value;
                                        }
                                      }
                                      setCart({
                                        ...newCart,
                                      });
                                      return;
                                    }
                                    setCart({
                                      ...cart,
                                      [item.id]: e,
                                    });
                                  }}
                                ></InputNumber>
                              </div>,
                              <div
                                style={{
                                  display: "flex",
                                  justifyContent: "center",
                                  alignContent: "center",
                                  alignItems: "center",
                                  height: 30,
                                }}
                              >
                                <TeamOutlined
                                  onClick={() => {
                                    redirect(`/shop/${item.id}`);
                                  }}
                                  key="goTo"
                                />
                              </div>,
                            ]}
                          ></CardItem>
                        );
                      })}
                  </Space>
                </Col>
              </Row>
            </div>
          </div>
        </Content>
      </Layout>
    </div>
  );
};
