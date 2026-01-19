import React, { createContext, useContext, useState, useEffect } from 'react'

const AuthContext = createContext(null)

export const useAuth = () => {
    const context = useContext(AuthContext)
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider')
    }
    return context
}

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null)
    const [token, setToken] = useState(null)
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        // Check for stored auth on mount
        if (typeof window !== 'undefined') {
            const storedToken = localStorage.getItem('cinema_token')
            const storedUser = localStorage.getItem('cinema_user')
            
            if (storedToken && storedUser) {
                setToken(storedToken)
                setUser(JSON.parse(storedUser))
            }
            setLoading(false)
        }
    }, [])

    const login = (userData, authToken) => {
        setUser(userData)
        setToken(authToken)
        if (typeof window !== 'undefined') {
            localStorage.setItem('cinema_token', authToken)
            localStorage.setItem('cinema_user', JSON.stringify(userData))
        }
    }

    const logout = () => {
        setUser(null)
        setToken(null)
        if (typeof window !== 'undefined') {
            localStorage.removeItem('cinema_token')
            localStorage.removeItem('cinema_user')
        }
    }

    const isAuthenticated = () => {
        return !!token && !!user
    }

    const value = {
        user,
        token,
        loading,
        login,
        logout,
        isAuthenticated
    }

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthContext
