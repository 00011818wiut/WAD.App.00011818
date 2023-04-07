import axios from 'axios'
import { getAuth } from "../storage"
import { useState, useEffect } from "react"
import ProductItem from './ProductItem'
import { useNavigate } from 'react-router-dom'

function MyProducts() {

    const [state, setState] = useState([])
    const navigate = useNavigate()

    useEffect(() => {
        axios.get('http://localhost:5177/api/products/user', {
            headers: {
                'accessToken': getAuth().token
            }
        })
            .then(response => {
                setState(response.data)
            })
    }, [])

    async function onDelete(item) {
        try {
            await axios.delete('http://localhost:5177/api/products/' + item.id, {
                headers: {
                    'accessToken': getAuth().token
                }
            })
            setState(state => state.filter(i => i.id != item.id))
        }
        catch(err){
            console.timeLog(err)
        }
    }

    function onEdit(item) {
        navigate('/edit', { 
            state: item
        })
    }

    return (
        <>
            <div className="flex between">
                <h3> My Products </h3>
                <a href="/add" className="btn"> Add Product </a>
            </div>

            <div className="cards">
                {
                    state.map(product => (
                        <ProductItem 
                            key={product.id}
                            item={product} 
                            onDelete={onDelete}
                            onEdit={onEdit}
                        />)
                    )
                }
            </div>
        </>
    )
}

export default MyProducts