import React, { useState } from "react"
import Layout from "../components/Layout"
import "./signup.scss"

import { TextField, Button } from '@material-ui/core'
import { HiEye, HiEyeOff } from 'react-icons/hi'
import { Link } from "gatsby"

import api from '../data/api'
import values from '../data/values.json'
import CustomSelect from "../components/CustomSelect";
import PasswordField from "../components/PasswordField";


export default function Signup() {

    const [user, setUser] = useState({firstname: '', lastname: '', username: '', password: '', confirmPassword: '', role: 2})
    const [pass, showPass] = useState(false)

    const getToken = async (username, password) => {

        const response = await api.post('/token', {
            "username": `${username}`,
            "password": `${password}`
        })

        return response.data.token
    }

    const saveUser = async (user) => {
        
        if (user.password == user.confirmPassword) {
            const token = await getToken("franco.js", "123")
            const response = await api.post('/user', 
            {
                "firstname": `${user.firstname}`,
                "lastname": `${user.lastname}`,
                "username": `${user.username}`,
                "password": `${user.password}`,
                "role": user.role
            },
            {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization' : `Bearer ${token}`
                }
            })

            console.log(response.data)

        } else {
            alert('Password validation failed')
        }
    }

    return (
        <Layout title="Cinema API | Sign up" page={4}>
            <div className="Signup">
                <div className="SignupBox">
                    <h1>Sign up</h1>
                    <form className="SignupForm" onSubmit={() => {saveUser(user)}}>
                        <div className="SignupRow">
                            <TextField 
                                id="standard-basic" 
                                label="Firstname" 
                                type="text"
                                required 
                                onChange={e => setUser({...user, firstname: e.target.value})}/>

                            <TextField 
                                id="standard-basic" 
                                label="Lastname" 
                                type="text"
                                required 
                                onChange={e => setUser({...user, lastname: e.target.value})}/>
                        </div>

                        <div className="SignupRow">
                            <TextField 
                                id="standard-basic" 
                                label="Username" 
                                type="text"
                                required 
                                onChange={e => setUser({...user, username: e.target.value})}/>

                            <CustomSelect 
                                label="Role"
                                value={user.role}
                                onChange={e => setUser({...user, role: e.target.value})}
                                items={values[0].items} />
                        </div>

                        <div className="SignupRow">
                            <PasswordField 
                                label="Password"
                                type={pass}
                                onChange={e => setUser({...user, password: e.target.value})}>
                            </PasswordField>

                            <PasswordField 
                                label="Confirm Password"
                                type={pass}
                                onChange={e => setUser({...user, confirmPassword: e.target.value})}>

                                {pass ? 
                                    <HiEye onClick={() => {showPass(false)}} />
                                    :
                                    <HiEyeOff onClick={() => {showPass(true)}} />
                                }

                            </PasswordField>                               
                        </div>

                        <Button variant="contained" color="primary" type="submit">Sign up</Button>
                        <Link to="/signin">I already have an account</Link>
                    </form>
                </div>
            </div>
        </Layout>
    )
}
