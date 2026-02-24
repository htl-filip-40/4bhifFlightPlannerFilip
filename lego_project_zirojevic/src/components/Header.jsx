import React from "react";
import "../css/Header.css";
import Logo from "../assets/lego-logo.png";
import { Link } from "react-router-dom";

export default function Header({cart}) {
    return (
        <header className="header">
            <div className="header-left">
                <img src={Logo} className="logo" />
                <h1>Store</h1>
            </div>

            <nav className="header-right">
                <Link to="/products">Produkte</Link>

                <div className="cart">
                    <Link to="/cart">
                        🛒 <span>{cart.length}</span>
                    </Link>
                </div>
            </nav>
        </header>
    );
}