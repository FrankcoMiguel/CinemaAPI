import React, { useState } from "react"
import { navigate, Link } from "gatsby"
import Layout from "../components/Layout"
import "./signup.scss"

import { Button, TextField, InputAdornment, IconButton, Alert, CircularProgress } from '@mui/material'
import { FaUser, FaLock, FaEye, FaEyeSlash, FaFilm, FaEnvelope } from 'react-icons/fa'

import api from '../data/api'
import { useAuth } from '../context/AuthContext'


export default function Signup() {

    const { login } = useAuth()
    const [formData, setFormData] = useState({
        firstname: '',
        lastname: '',
        username: '',
        password: '',
        confirmPassword: '',
        role: 2
    })
    const [showPassword, setShowPassword] = useState(false)
    const [showConfirmPassword, setShowConfirmPassword] = useState(false)
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)
    const [success, setSuccess] = useState(false)

    const handleSubmit = async (e) => {
        e.preventDefault()
        setLoading(true)
        setError(null)
        
        if (formData.password !== formData.confirmPassword) {
            setError('Passwords do not match')
            setLoading(false)
            return
        }

        if (formData.password.length < 6) {
            setError('Password must be at least 6 characters')
            setLoading(false)
            return
        }

        try {
            // First, get a token to create the user (using admin credentials)
            const tokenResponse = await api.post('/token', {
                "username": "franco.js",
                "password": "123"
            })
            
            // Create the user account
            await api.post('/user', {
                "firstname": formData.firstname,
                "lastname": formData.lastname,
                "username": formData.username,
                "password": formData.password,
                "role": formData.role
            }, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${tokenResponse.data.token}`
                }
            })
            
            // Now login with the new user credentials
            const loginResponse = await api.post('/token', {
                "username": formData.username,
                "password": formData.password
            })
            
            setSuccess(true)
            
            // Auto-login and redirect to dashboard
            setTimeout(() => {
                login({ 
                    username: formData.username,
                    firstname: formData.firstname,
                    lastname: formData.lastname
                }, loginResponse.data.token)
                navigate('/dashboard')
            }, 1500)
            
        } catch (err) {
            setError(err.response?.data?.message || 'Registration failed. Please try again.')
        } finally {
            setLoading(false)
        }
    }

    return (
        <Layout title="Cinema API | Sign up" page={4} hideFooter={true}>
            <div className="AuthPage">
                <div className="AuthContainer signup">
                    <div className="AuthDecor">
                        <div className="DecorContent">
                            <h2>Join Cinema API</h2>
                            <p>Create your account and start building amazing entertainment applications today.</p>
                            <ul className="DecorFeatures">
                                <li>Free tier with 1000 requests/day</li>
                                <li>Full API documentation</li>
                                <li>Community support</li>
                                <li>No credit card required</li>
                            </ul>
                        </div>
                    </div>

                    <div className="AuthCard">
                        <div className="AuthHeader">
                            <div className="AuthLogo">
                                <FaFilm />
                            </div>
                            <h1>Create Account</h1>
                            <p>Get started with Cinema API for free</p>
                        </div>

                        {error && (
                            <Alert severity="error" className="AuthAlert">
                                {error}
                            </Alert>
                        )}

                        {success && (
                            <Alert severity="success" className="AuthAlert">
                                Account created successfully! Redirecting to dashboard...
                            </Alert>
                        )}

                        <form className="AuthForm" onSubmit={handleSubmit}>
                            <div className="FormRow">
                                <div className="FormField">
                                    <TextField
                                        fullWidth
                                        label="First Name"
                                        variant="outlined"
                                        value={formData.firstname}
                                        onChange={(e) => setFormData({...formData, firstname: e.target.value})}
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
                                        label="Last Name"
                                        variant="outlined"
                                        value={formData.lastname}
                                        onChange={(e) => setFormData({...formData, lastname: e.target.value})}
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
                            </div>

                            <div className="FormField">
                                <TextField
                                    fullWidth
                                    label="Username"
                                    variant="outlined"
                                    value={formData.username}
                                    onChange={(e) => setFormData({...formData, username: e.target.value})}
                                    required
                                    InputProps={{
                                        startAdornment: (
                                            <InputAdornment position="start">
                                                <FaEnvelope className="FieldIcon" />
                                            </InputAdornment>
                                        ),
                                    }}
                                />
                            </div>

                            <div className="FormRow">
                                <div className="FormField">
                                    <TextField
                                        fullWidth
                                        label="Password"
                                        variant="outlined"
                                        type={showPassword ? 'text' : 'password'}
                                        value={formData.password}
                                        onChange={(e) => setFormData({...formData, password: e.target.value})}
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

                                <div className="FormField">
                                    <TextField
                                        fullWidth
                                        label="Confirm Password"
                                        variant="outlined"
                                        type={showConfirmPassword ? 'text' : 'password'}
                                        value={formData.confirmPassword}
                                        onChange={(e) => setFormData({...formData, confirmPassword: e.target.value})}
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
                                                        onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                                                        edge="end"
                                                        className="TogglePassword"
                                                    >
                                                        {showConfirmPassword ? <FaEyeSlash /> : <FaEye />}
                                                    </IconButton>
                                                </InputAdornment>
                                            ),
                                        }}
                                    />
                                </div>
                            </div>

                            <Button 
                                type="submit"
                                variant="contained" 
                                fullWidth
                                disabled={loading || success}
                                className="AuthButton"
                            >
                                {loading ? (
                                    <>
                                        <CircularProgress size={20} color="inherit" />
                                        <span>Creating Account...</span>
                                    </>
                                ) : success ? (
                                    'Account Created!'
                                ) : (
                                    'Create Account'
                                )}
                            </Button>
                        </form>

                        <div className="AuthFooter">
                            <p>Already have an account? <Link to="/signin">Sign in</Link></p>
                        </div>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
