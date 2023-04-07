import axios from "axios"
import { useNavigate, Link } from "react-router-dom"
import { saveAuth } from "../storage"
import { rawFormData } from "../util"

function Register() {

    const navigate = useNavigate()

    async function submit(event) {
        event.preventDefault()

        const data = rawFormData(new FormData(event.target))
        try {
            const response = await axios.post('http://localhost:5177/api/register', data)
            if (response.status == 200) {
                saveAuth(response.data)
                navigate('/')
            }
            else {
                alert(response.data)
            }
        }
        catch(err) {
            alert(err.response.data)
        }
    }

    return (
        <div className="container center">
            <form onSubmit={submit}>
                <h2> Register </h2>
                
                <label> Name </label>
                <input type="text" name="name" />

                <label> Surname </label>
                <input type="text" name="surname"/>

                <label> Email </label>
                <input type="email" name="email"/>

                <label> Password </label>
                <input type="password" name="password"/>

                <label> Confirm Password </label>
                <input type="password" name="confirmPassword"/>

                <button type="submit"> Register </button>

                <Link to="/login"> Have account </Link>
            </form>
        </div>
    )
}

export default Register