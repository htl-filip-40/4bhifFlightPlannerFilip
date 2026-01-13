import { useState } from 'react';
import { Routes, Route, Link } from 'react-router-dom';
import StorePage from './StorePage';
import ProductDetailsPage from './ProductDetailsPage';

export default function App() {
    // State für den Warenkorb
    const [cart, setCart] = useState([]);

    // Funktion zum Hinzufügen
    const addToCart = (product) => {
        setCart([...cart, product]);
        alert(`${product.name} wurde in den Wagen gelegt!`);
    };

    return (
        <div className="app">
            {/* HEADER: Immer sichtbar */}
            <header style={{ background: "#FFD700", padding: "15px", display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                <h1 style={{ margin: 0 }}>LEGO Store</h1>
                <span style={{ fontSize: "1.2rem", fontWeight: "bold" }}>
          🛒 Items: {cart.length}
        </span>
            </header>

            {/* ROUTING BEREICH */}
            <div style={{ padding: "20px" }}>
                <Routes>
                    {/* Startseite */}
                    <Route path="/" element={<StorePage addToCart={addToCart} />} />

                    {/* Detailseite (Die :id ist der Platzhalter) */}
                    <Route path="/products/:id" element={<ProductDetailsPage addToCart={addToCart} />} />
                </Routes>
            </div>
        </div>
    );
}