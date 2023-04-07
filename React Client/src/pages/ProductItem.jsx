function ProductItem({ item, addCart, onDelete, onEdit, removeCart }) {

    console.log(item)

    return (
        <div className="card">
            <h3> {item.name} </h3>
            <div className="between">
                <span className="price"> ${item.price} </span>
                <span className="category"> {item.category.name} </span>
            </div>
            <p> {item.description} </p>
            <hr />
            <div className="flex sm">
                {
                    addCart && <button onClick={() => addCart(item) } className="sm"> Add to Cart </button>
                }
                {
                    onDelete && <button onClick={() => onDelete(item)} className="sm"> Delete </button>
                }
                {
                    onEdit && <button onClick={() => onEdit(item)} className="sm"> Edit </button>
                }
                {
                    removeCart && <button onClick={() => removeCart(item)} className="sm"> Remove </button>
                }
            </div>
        </div>
    )
}

export default ProductItem