export function saveAuth (auth) {
    localStorage.setItem('auth', JSON.stringify(auth))
}

export function clearAuth () {
    localStorage.removeItem('auth')
}

export function getAuth() {
    return JSON.parse(localStorage.getItem('auth'))
}

export function getCart() {
    const items = JSON.parse(localStorage.getItem('cart'))
    return items ?? []
}

export function addCart(item) {
    const items = getCart()
    items.push(item)
    localStorage.setItem('cart', JSON.stringify(items))
}

export function removeFromCart(item) {
    const items = getCart().filter(i => i.id != item.id)
    localStorage.setItem('cart', JSON.stringify(items))
}

export function isAuthorized() {
    const auth = getAuth()

    if (auth != null) {
        return auth.token != undefined
    }

    return false
}