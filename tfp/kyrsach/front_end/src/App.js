import React, { useState } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Head } from "./Components/Head";
import { Foot } from "./Components/Foot";
import { ScreenContext } from "./Utils/ScreenContext";
import { Main } from "./Pages/Main";
import { Shop } from "./Pages/Shop";

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
            <Route path="/shop" element={<Shop></Shop>} />
          </Routes>
          <Foot></Foot>
        </BrowserRouter>
      </ScreenContext.Provider>
    </div>
  );
}

export default App;
