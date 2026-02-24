// Cart.jsx
import React from "react";
import "../css/Cart.css";
import LineItem from "./LineItem";
import Glass from "../assets/glass.png"
import Wallet from "../assets/wallet.png"
import {Link} from "react-router-dom";

export default function Cart() {
    const [items, setItems] = React.useState([{
        id: 1,
        name: "Tumbler Glass",
        price: 19,
        quantity: 3,
        img: Glass,
        stock: true,
    },

    {
        id: 2,
        name: "Organized Wallet",
        price: 36,
        quantity: 2,
        img: Wallet,
        stock: true,
    }])

    const showLineItems = items.map((item, i) => {
        return <LineItem key={i} item={item} resetQuantity={resetQuantity} setQuantity={setQuantity} /> ;
    })

    function setQuantity(id, quantity) {
        setItems(items.map((item) => {
            if(item.id === id) {
                return {
                    ...item,
                    quantity: quantity,
                }
            }
            return item;
        }))
    }


    function resetQuantity(id) {
        setItems(items.map((item) => {
            if(item.id === id) {
                return {
                    ...item,
                    quantity: 0,
                }
            }
            return item;
        }))
    }


    return (
        <div className="cart">
            <h2>Your Shopping Cart</h2>
            <Link to={"/"}>Zurück zur Homepage</Link>
            <header className="cart-header">
                <span className="col-item">Item</span>
                <span className="col-qty">Quantity</span>
                <span className="col-price">Price</span>
                <span className="col-total">Item Total</span>
            </header>

            <div>

            </div>

            <div>
                {showLineItems}
            </div>
        </div>
    );
}
