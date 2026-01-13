import { PRODUCTS } from './data';
import ProductCard from './ProductCard';

export default function StorePage({ addToCart }) {
    return (
        <div>
            <h2>Product Overview</h2>
            <div style={{ display: "flex", gap: "20px", flexWrap: "wrap" }}>

                {PRODUCTS.map((product) => (
                    <ProductCard
                        key={product.id}
                        product={product}
                        addToCart={addToCart}
                    />
                ))}

            </div>
        </div>
    );
}