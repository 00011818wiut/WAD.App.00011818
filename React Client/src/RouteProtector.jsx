import { Navigate } from "react-router-dom"
import { getAuth, isAuthorized } from "./storage"

function RouteProtector({ children }) {
    
    console.log(getAuth());

    if (!isAuthorized()) {
        return <Navigate to="/login"/>
    }

    return children
}

export default RouteProtector