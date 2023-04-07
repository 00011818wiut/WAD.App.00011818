import { useEffect, useState } from "react"
import axios from 'axios'
import ProductItem from "./ProductItem"
import { addCart, getAuth } from "../storage"

function Products() {

    const [state, setState] = useState([])

    useEffect(() => {
        axios.get('http://localhost:5177/api/products')
            .then(response => {
                setState(response.data)
            })
    }, [])

    return (
        <>
            <h3> Products </h3>
            <div className="cards">
                {
                    state.map(product => (
                        <ProductItem 
                            addCart={addCart}
                            key={product.id}
                            item={product} 
                            />
                    ))
                }
            </div>
        </>
    )
}

export default Products