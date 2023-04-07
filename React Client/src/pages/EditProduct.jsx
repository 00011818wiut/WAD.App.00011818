import { useState, useEffect } from "react"
import axios from "axios"
import { rawFormData } from "../util"
import { getAuth } from '../storage'
import { useLocation, useNavigate } from "react-router-dom"

function EditProduct() {

    const [categories, setCategories] = useState([])
    const navigate = useNavigate()

    const item = useLocation().state

    console.log(item)

    useEffect(() => {
        axios.get('http://localhost:5177/api/categories')
            .then(response => {
                setCategories(response.data)
            })
    }, [])

    async function submit(event) {
        event.preventDefault()

        if (!item) {
            return
        }

        const data = rawFormData(new FormData(event.target))

        try {
            const response = await axios.put('http://localhost:5177/api/products/' + item.id, data, {
                headers: {
                    "accessToken": getAuth().token
                }
            })

            console.log(response)
            navigate('/my-products')
        }
        catch(err) {
            alert(err.message)
        }
    }

    return (
        <div className="container sm">
            <h2> Update pruduct </h2>
            <form onSubmit={submit}>
                <label>Name</label>
                <input type="text" name="name" defaultValue={item?.name ?? ''}/>

                <label>Description</label>
                <input type="text" name="description" defaultValue={item?.description ?? ''} />

                <label>Price</label>
                <input type="text" name="price" defaultValue={item?.price ?? 0}/>

                <label>Category</label>
                <select name="categoryId" defaultValue={item?.categoryId ?? 1}>
                    {
                        categories.map(category => (
                            <option key={category.id} value={category.id}> {category.name} </option>
                        ))
                    }
                </select>

                <button type="submit"> Update </button>
            </form>
        </div>
    )
}

export default EditProduct