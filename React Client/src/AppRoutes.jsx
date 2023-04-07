import { createBrowserRouter, Navigate } from "react-router-dom";
import App from "./pages/App";
import Login from "./pages/Login";
import Products from "./pages/Products";
import Register from "./pages/Register";
import RouteProtector from "./RouteProtector";
import MyProducts from "./pages/MyProducts";
import AddProduct from "./pages/AddProduct";
import EditProduct from './pages/EditProduct'

const router = createBrowserRouter([
    {
        path: '/',
        element: (
            <RouteProtector>
                <App/>
            </RouteProtector>
        ),
        children: [
            {
                index: true,
                element: <Navigate to='/products'/>
            },
            {
                path: 'products',
                element: <Products/>
            },
            {
                path: 'add',
                element: <AddProduct/>
            },
            {
                path: 'edit',
                element: <EditProduct/>
            },
            {
                path: 'my-products',
                element: <MyProducts/>
            }
        ]
    },
    {
        path: 'login',
        element: <Login/>
    },
    {
        path: 'register',
        element: <Register/>
    }
])

export default router