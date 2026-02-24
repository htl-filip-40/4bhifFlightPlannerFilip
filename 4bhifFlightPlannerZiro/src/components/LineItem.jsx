// LineItem.jsx
import React from "react";
import "../css/LineItem.css";

export default function LineItem({ item, setQuantity, resetQuantity }) {
    const itemTotal = item.price * item.quantity;

    function onChangeQuantity(event) {
        setQuantity(item.id, event.currentTarget.value)
    }



    return (
        <div className="line-item">

            <div className="item-info">
                <img src={item.img} alt={item.name} className="item-image" />

                <div className="item-text">
                    <h3>{item.name}</h3>
                    <a href="#">Personalize</a>
                    <span className={item.quantity > 0 ? "inStock" : "outOfstock"}>In stock</span>
                </div>
            </div>

            {/* Spalte 2: Menge */}
            <div className="item-qty">
                <input type="number" value={item.quantity} onChange={onChangeQuantity} className="item-qty" />
                <span className="trash" onClick={() => resetQuantity(item.id)}>🗑️</span>
            </div>

            {/* Spalte 3: Preis */}
            <div className="item-price">
                {item.quantity} @ ${item.price.toFixed(2)}
            </div>

            {/* Spalte 4: Item Total */}
            <div className="item-total">
                ${itemTotal.toFixed(2)}
            </div>

        </div>
    );
}
