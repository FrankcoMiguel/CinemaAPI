import React from 'react'
import "./ModernTextField.scss"

import TextField from '@material-ui/core/TextField';
import InputAdornment from '@material-ui/core/InputAdornment';

const ModernTextField = ({children, label, type, secondElement, onChange}) => {
    return (
        <div className="ModernTextField">
            <TextField
                id="input-with-icon-textfield"
                onChange={onChange}
                label={label}
                required
                type={type}
                InputProps={{
                    startAdornment: (
                        <InputAdornment position="start">
                            {children}
                        </InputAdornment>
                    ),
                }}
            />
            {secondElement}
        </div>
    ) 
}

export default ModernTextField