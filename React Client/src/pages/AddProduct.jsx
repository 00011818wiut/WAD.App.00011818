import { useState, useEffect } from "react"
import axios from "axios"
import { rawFormData } from "../util"
import { getAuth } from '../storage'
import { useNavigate } from "react-router-dom"

function AddProduct() {

    const [categories, setCategories] = useState([])
    const navigate = useNavigate()

    useEffect(() => {
        axios.get('http://localhost:5177/api/categories')
            .then(response => {
                setCategories(response.data)
            })
    }, [])

    async function submit(event) {
        event.preventDefault()

        const data = rawFormData(new FormData(event.target))

        try {
            const response = await axios.post('http://localhost:5177/api/products', data, {
                headers: {
                    "accessToken": getAuth().token
                }
            })

            navigate('/my-products')
        }
        catch(err) {
            alert(err.message)
        }
    }

    return (
        <div className="container sm">
            <h2> Create pruduct </h2>
            <form onSubmit={submit}>
                <label>Name</label>
                <input type="text" name="name" />

                <label>Description</label>
                <input type="text" name="description" />

                <label>Price</label>
                <input type="text" name="price" />

                <label>Category</label>
                <select name="categoryId">
                    {
                        categories.map(category => (
                            <option key={category.id} value={category.id}> {category.name} </option>
                        ))
                    }
                </select>

                <button type="submit"> Create </button>
            </form>
        </div>
    )
}

export default AddProduct