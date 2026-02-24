import React from "react";
import Header from "./Header.jsx";
import Body from "./Body.jsx";

export default function MainPage({products, onAdd, cart}) {

    return (
        <>
            <Header cart={cart} />
            <Body products={products} onAdd={onAdd} />
        </>
    );
}
