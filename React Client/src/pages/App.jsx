import { useState } from 'react'
import { Link, Outlet, useNavigate } from 'react-router-dom'
import { clearAuth, isAuthorized } from '../storage'
import './App.css'
import Cart from './Cart'

function App() {

    const [cart, showCart] = useState(false)

    const navigate = useNavigate()

    function logout() {
        clearAuth()
        navigate('/login')
    }

    return (
        <>
            <header>
                <h2> Dashboard </h2>
                <ul className="nav">
                    <li className="nav-item">
                        <Link to="/products">Products</Link>
                    </li>
                    {
                        isAuthorized() && (
                            <li className="nav-item">
                                <Link to="/my-products">My Products</Link>
                            </li>
                        )
                    }
                </ul>

                {
                    isAuthorized() ? (
                        <div className="flex">
                            <button onClick={() => showCart(true)}> Cart </button>
                            <button onClick={logout}> Logout </button>
                        </div>
                    ) : (
                        <div className="flex">
                            <Link to="/login" className='btn'> Login </Link>
                            <Link to="/register" className='btn'> Register </Link>
                        </div>
                    )
                }
            </header>

            <div className="container">
                <Outlet />
                
            </div>
            {
                cart && (
                    <Cart close={() => showCart(false)}/>
                )
            }
        </>
    )
}

export default App