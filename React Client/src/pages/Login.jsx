import axios from "axios"
import { Link, useNavigate } from "react-router-dom"
import { saveAuth } from "../storage"
import { rawFormData } from "../util"

function Login() {

    const navigate = useNavigate()

    async function submit(event) {
        event.preventDefault()

        const data = rawFormData(new FormData(event.target))
        try {
            const response = await axios.post('http://localhost:5177/api/login', data)
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
                <h2> Login </h2>
                <label> Email </label>
                <input type="email" name="email"/>

                <label> Password </label>
                <input type="password" name="password"/>

                <button type="submit"> Login </button>

                <Link to="/register"> Create an account </Link>
            </form>
        </div>
    )
}

export default Login