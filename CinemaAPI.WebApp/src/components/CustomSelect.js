import React, { useState } from 'react'
import {InputLabel, Select, MenuItem} from '@material-ui/core'
import FormControl from '@material-ui/core/FormControl';

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