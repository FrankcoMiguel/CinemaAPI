import React, { useState } from "react"
import { navigate, Link } from "gatsby"
import Layout from "../components/Layout"
import "./signin.scss"

import { Button, TextField, InputAdornment, IconButton, Alert, CircularProgress } from '@mui/material'
import { FaUser, FaLock, FaEye, FaEyeSlash, FaFilm } from 'react-icons/fa'

import api from '../data/api'
import { useAuth } from '../context/AuthContext'


export default function Signin() {

    const { login } = useAuth()
    const [credentials, setCredentials] = useState({username: '', password: ''})
    const [showPassword, setShowPassword] = useState(false)
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)

    const handleLogin = async (e) => {
        e.preventDefault()
        setLoading(true)
        setError(null)
        
        try {
            const response = await api.post('/token', {
                "username": credentials.username,
                "password": credentials.password
            })
            
            login({ username: credentials.username }, response.data.token)
            navigate('/dashboard')
        } catch (err) {
            setError(err.response?.data?.message || 'Invalid credentials. Please try again.')
        } finally {
            setLoading(false)
        }
    }

    return (
        <Layout title="Cinema API | Sign in" page={3} hideFooter={true}>
            <div className="AuthPage">
                <div className="AuthContainer">
                    <div className="AuthCard">
                        <div className="AuthHeader">
                            <div className="AuthLogo">
                                <FaFilm />
                            </div>
                            <h1>Welcome Back</h1>
                            <p>Sign in to access your Cinema API dashboard</p>
                        </div>

                        {error && (
                            <Alert severity="error" className="AuthAlert">
                                {error}
                            </Alert>
                        )}

                        <form className="AuthForm" onSubmit={handleLogin}>
                            <div className="FormField">
                                <TextField
                                    fullWidth
                                    label="Username"
                                    variant="outlined"
                                    value={credentials.username}
                                    onChange={(e) => setCredentials({...credentials, username: e.target.value})}
                                    required
                                    InputProps={{
                                        startAdornment: (
                                            <InputAdornment position="start">
                                                <FaUser className="FieldIcon" />
                                            </InputAdornment>
                                        ),
                                    }}
                                />
                            </div>

                            <div className="FormField">
                                <TextField
                                    fullWidth
                                    label="Password"
                                    variant="outlined"
                                    type={showPassword ? 'text' : 'password'}
                                    value={credentials.password}
                                    onChange={(e) => setCredentials({...credentials, password: e.target.value})}
                                    required
                                    InputProps={{
                                        startAdornment: (
                                            <InputAdornment position="start">
                                                <FaLock className="FieldIcon" />
                                            </InputAdornment>
                                        ),
                                        endAdornment: (
                                            <InputAdornment position="end">
                                                <IconButton
                                                    onClick={() => setShowPassword(!showPassword)}
                                                    edge="end"
                                                    className="TogglePassword"
                                                >
                                                    {showPassword ? <FaEyeSlash /> : <FaEye />}
                                                </IconButton>
                                            </InputAdornment>
                                        ),
                                    }}
                                />
                            </div>

                            <Button 
                                type="submit"
                                variant="contained" 
                                fullWidth
                                disabled={loading}
                                className="AuthButton"
                            >
                                {loading ? (
                                    <>
                                        <CircularProgress size={20} color="inherit" />
                                        <span>Signing in...</span>
                                    </>
                                ) : (
                                    'Sign In'
                                )}
                            </Button>
                        </form>

                        <div className="AuthFooter">
                            <p>Don't have an account? <Link to="/signup">Create one</Link></p>
                        </div>
                    </div>

                    <div className="AuthDecor">
                        <div className="DecorContent">
                            <h2>Cinema API</h2>
                            <p>Access comprehensive movie data through our powerful REST API. Build amazing entertainment applications.</p>
                            <ul className="DecorFeatures">
                                <li>10,000+ Movies</li>
                                <li>50,000+ Actors</li>
                                <li>Real-time Updates</li>
                                <li>99.9% Uptime</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
