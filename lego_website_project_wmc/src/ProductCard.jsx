import { Link } from 'react-router-dom';

export default function ProductCard({ product, addToCart }) {
    return (
        <div style={{ border: "1px solid #ccc", padding: "15px", borderRadius: "8px", width: "200px", textAlign: "center", background: "#fff" }}>


            <div style={{ height: "120px", marginBottom: "10px", display: "flex", alignItems: "center", justifyContent: "center" }}>
                <img
                    src={product.image}
                    alt={product.name}
                    style={{ maxHeight: "100%", maxWidth: "100%", objectFit: "contain" }}
                />
            </div>
            {/* --------------------------- */}

            <h3>{product.name}</h3>
            <p>{product.price} €</p>

            <Link to={`/products/${product.id}`}>
                <button style={{ marginBottom: "8px", cursor: "pointer" }}>View Details</button>
            </Link>
            <br />

            <button
                onClick={() => addToCart(product)}
                style={{ background: "black", color: "white", cursor: "pointer", border: "none", padding: "5px 10px" }}
            >
                Add to cart
            </button>
        </div>
    );
}