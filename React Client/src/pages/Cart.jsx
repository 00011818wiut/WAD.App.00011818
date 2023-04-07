import { useEffect, useState } from "react"
import { getCart, removeFromCart } from "../storage"
import ProductItem from './ProductItem'

function Cart({ close }) {

    const [animate, setAnimate] = useState('')

    const [items, setItems] = useState(getCart())

    useEffect(() => {
        setAnimate('anim')
    }, [])

    function onClose() {
        setAnimate('')
        setTimeout(() => close(), 200)
    }

    function removeCart(item) {
        setItems(items => items.filter(i => i.id != item.id))
        removeFromCart(item)
    }

    return (
        <div className="cart-container">
            <div className={"cart " + animate}>
                <div className="flex end">
                    <button onClick={onClose}>Close</button>
                </div>

                <div className="cards vertical">
                    {
                        items.map(item => (
                            <ProductItem item={item} removeCart={removeCart}/>
                        ))
                    }
                </div>
            </div>
        </div>
    )
}

export default Cart