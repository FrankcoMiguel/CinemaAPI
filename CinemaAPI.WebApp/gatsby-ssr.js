import React from 'react'
import { AuthProvider } from './src/context/AuthContext'

export const wrapRootElement = ({ element }) => {
    return <AuthProvider>{element}</AuthProvider>
}
