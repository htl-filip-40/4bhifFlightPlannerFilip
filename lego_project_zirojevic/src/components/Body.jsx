import React from "react";
import "../css/Body.css";
import ProductCard from "./ProductCard";

// Images
import Police from "../assets/police.png";
import Fire from "../assets/fire.png";
import Train from "../assets/train.png";

export default function Body({ products ,onAdd}) {

    return (
        <div className="body">
            <h2 className="body-title">Product Overview</h2>

            <div className="body-grid">
                {products.map((p) => (
                    <ProductCard key={p.id} img={p.img} name={p.name} onAdd={() => onAdd(p)}/>
                ))}
            </div>
        </div>
    );
}
