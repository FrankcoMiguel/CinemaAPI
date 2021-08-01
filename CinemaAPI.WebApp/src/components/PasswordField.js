import React from 'react'
import { TextField } from '@material-ui/core'

import "./PasswordField.scss"

const PasswordField = ({children, label, type, onChange}) => {

    

    return (
        <div className="PasswordField">
            <TextField 
                id="standard-basic" 
                label={label} 
                type={type ? "text" : "password"}
                required
                onChange={onChange}/>

            {children}
        </div>
    )
}

export default PasswordField