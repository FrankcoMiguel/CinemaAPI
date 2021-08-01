import React, { useState } from "react"
import Layout from "../components/Layout"
import "./signin.scss"

import ModernTextField from '../components/ModernTextField'
import { Button } from '@material-ui/core'
import {RiUserFill} from 'react-icons/ri'
import {BsShieldLockFill} from 'react-icons/bs'
import { HiEye, HiEyeOff } from 'react-icons/hi'
import { Link } from "gatsby"

import api from '../data/api'


export default function Signin() {

    const [auth, setAuth] = useState()
    const [credentials, setCredentials] = useState({username: '', password: ''})
    const [pass, showPass] = useState(false) 

    const login = async (user, password) => {
        
        const response = await api.post('/token', {
            "username": `${user}`,
            "password": `${password}`
        })
        
        alert(response.data)
        return response
    }

    return (
        <Layout title="Cinema API | Sign in" page={3}>
            <div className="Signin">
                <div className="SigninBox">
                    <h1>Sign in</h1>
                    <form className="SigninForm">

                        <div className="FieldContainer">
                            <ModernTextField label="Username" type="text" onChange={e => setCredentials({...credentials, username: e.target.value})}>
                                <RiUserFill />
                            </ModernTextField>
                        </div>

                        <div className="FieldContainer">
                            <ModernTextField 
                                label="Password" 
                                type={pass ? "text" : "password"}
                                secondElement={pass ? 
                                    <HiEye className="eye" onClick={() => {showPass(false)}} />
                                    :
                                    <HiEyeOff className="eye" onClick={() => {showPass(true)}} />
                                } 
                                onChange={e => setCredentials({...credentials, password: e.target.value})}>
                                <BsShieldLockFill />
                            </ModernTextField>
                        </div>


                        <Button variant="contained" color="primary" onClick={() => login(credentials.username, credentials.password)}>Sign in</Button>
                        <Link to="/signup">I don't have an account yet</Link>
                    </form>
                </div>
            </div>
        </Layout>
    )
}
