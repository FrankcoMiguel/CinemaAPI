import React, { useState } from 'react'
import { InputLabel, Select, MenuItem, FormControl } from '@mui/material'

const CustomSelect = ({label, value, onChange, items = []}) => {

    return(
        <FormControl>
            <InputLabel id="demo-simple-select-label">{label}</InputLabel>
            <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={value}
                onChange={onChange}>
            
                {items.map(i => 
                    <MenuItem key={i.id} value={i.value}>
                        {i.display}
                    </MenuItem>
                )}
            </Select>
        </FormControl>

    )
}

export default CustomSelect