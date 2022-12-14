import React, { useState } from "react";
import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import { Head } from "./Components/Head";
import { Foot } from "./Components/Foot";
import { ScreenContext } from "./Utils/ScreenContext";
import { Main } from "./Pages/Main";
import { Shop } from "./Pages/Shop";
import { Error } from "./Pages/Error";
import { Product } from "./Pages/Product";
import { About } from "./Pages/About";
import { Posts } from "./Pages/Posts";
import { Post } from "./Pages/Post";

function App() {
  window.addEventListener("resize", (e) => {
    setScreenSize(document.documentElement.clientWidth);
  });
  const [screenSize, setScreenSize] = useState(
    document.documentElement.clientWidth
  );
  return (
    <div className="App">
      <ScreenContext.Provider value={{ screenSize: screenSize }}>
        <BrowserRouter>
          <Head></Head>
          <Routes>
            <Route path="" element={<Main></Main>} />
            <Route exact path="/shop" element={<Shop></Shop>} />
            <Route exact path="/shop/:id" element={<Product></Product>} />
            <Route exact path="/posts" element={<Posts></Posts>} />
            <Route exact path="/posts/:id" element={<Post></Post>} />
            <Route exact path="/about" element={<About></About>} />
            <Route path="/error" element={<Error></Error>} />
            <Route path="*" element={<Navigate to="/error" replace />} />
          </Routes>
          <Foot></Foot>
        </BrowserRouter>
      </ScreenContext.Provider>
    </div>
  );
}

export default App;
