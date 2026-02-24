import React from "react";
import "../css/ProductCard.css";

export default function ProductCard({ img, name,onAdd }) {
    return (
        <div className="product-card">
            <img src={img} className="product-img" />

            <h3>{name}</h3>

            <button className="btn">View Details</button>
            <button className="btn" onClick={onAdd}>Add to cart</button>
        </div>
    );
}
