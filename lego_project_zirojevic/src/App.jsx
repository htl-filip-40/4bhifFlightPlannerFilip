import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './css/App.css'
import Cart from "./components/Cart.jsx";
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import MainPage from "./components/MainPage.jsx";
import Police from "./assets/police.png";
import Fire from "./assets/fire.png";
import Train from "./assets/train.png";

function App() {


    const [cart, setCart] = useState([]);

    const products = [
        { id: 1, name: "City Police Station", img: Police },
        { id: 2, name: "City Fire Truck", img: Fire },
        { id: 3, name: "City Passenger Train", img: Train },
    ];


    function addToCart(product) {
        setCart([...cart, product]);
    }


  return (
    <div>
        <Routes>
            <Route path={"/"} element={<MainPage products={products} onAdd={addToCart} cart={cart}/>}></Route>
            <Route path={"/cart"} element={<Cart products={products}></Cart>}></Route>
            <Route path={"/products"} element={<h1>Products</h1>}></Route>
        </Routes>
    </div>
  )
}

export default App
