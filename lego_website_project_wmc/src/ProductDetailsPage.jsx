import { useParams, Link } from 'react-router-dom';
import { PRODUCTS } from './data'; // Oder wo auch immer deine Daten liegen

export default function ProductDetailsPage({ addToCart }) {
    const { id } = useParams();
    const product = PRODUCTS.find((p) => p.id === parseInt(id));

    if (!product) {
        return <h2>Product not found! <Link to="/">Go Home</Link></h2>;
    }

    return (
        <div style={{ border: "2px solid #FFD700", padding: "30px", borderRadius: "10px", maxWidth: "600px", margin: "0 auto" }}>
            <Link to="/" style={{ textDecoration: "none", color: "blue" }}>← Back to Store</Link>

            <h1 style={{ fontSize: "2.5rem" }}>{product.name}</h1>

            {/* --- HIER WURDE GEÄNDERT --- */}
            <div style={{ marginBottom: "20px", textAlign: "center" }}>
                <img
                    src={product.image}
                    alt={product.name}
                    style={{ maxWidth: "100%", height: "auto", borderRadius: "10px" }}
                />
            </div>
            {/* --------------------------- */}

            <p style={{ fontSize: "1.2rem" }}>{product.description}</p>
            <h3 style={{ color: "green" }}>Price: {product.price} €</h3>

            <button
                onClick={() => addToCart(product)}
                style={{ background: "#FFD700", border: "none", padding: "10px 20px", fontSize: "1rem", cursor: "pointer", fontWeight: "bold" }}
            >
                Add to Cart
            </button>
        </div>
    );
}